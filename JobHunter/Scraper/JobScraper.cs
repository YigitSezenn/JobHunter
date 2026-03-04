using JobHunter.Data;
using JobHunter.Models;
using Microsoft.Playwright;
using System.Text.RegularExpressions;
using System.Net;

public class JobScraper
{
    // Daha kesin mail yakalayan Regex
    private readonly string _emailPattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";

    public async Task SearchJobs(string keyword, string city)
    {
        Microsoft.Playwright.Program.Main(new[] { "install", "chromium" });
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            Args = new[] { "--disable-blink-features=AutomationControlled" }
        });

        var context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36"
        });

        var page = await context.NewPageAsync();

        // 1. ESKİ VERİLERİ SİL
        using (var db = new AppDbContext())
        {
            db.Database.EnsureCreated();
            db.Jobs.RemoveRange(db.Jobs);
            db.SaveChanges();
        }

        // 2. ÇOKLU ARAMA SORGULARI (Daha geniş kapsamlı)
        string[] queries = new[]
        {
            $"\"{keyword}\" \"{city}\" \"@gmail.com\" OR \"@outlook.com\"",
            $"site:linkedin.com/jobs \"{keyword}\" \"{city}\" \"@\"",
            $"site:kariyer.net \"{keyword}\" \"{city}\" \"@\"",
            $"\"{keyword}\" {city} iş ilanları iletişim \"@\""
        };

        foreach (var query in queries)
        {
            try
            {
                await page.GotoAsync($"https://www.google.com/search?q={Uri.EscapeDataString(query)}&num=20");

                // Bot kontrolünü aşmak ve yüklenmesini beklemek için bekleme
                await Task.Delay(4000);

                // İnsansı hareket: Sayfayı aşağı kaydır
                await page.Mouse.WheelAsync(0, 800);
                await Task.Delay(1000);

                // --- TEMİZLEME VE AYIKLAMA BAŞLANGICI ---

                // Sayfanın ham içeriğini al
                string rawContent = await page.ContentAsync();

                // %22, %40 gibi URL kodlarını gerçek karakterlere dönüştür
                string decodedContent = WebUtility.UrlDecode(rawContent);

                // Regex ile mailleri bul
                var matches = Regex.Matches(decodedContent, _emailPattern);

                using (var db = new AppDbContext())
                {
                    foreach (Match match in matches)
                    {
                        // Maili temizle: Tırnakları, URL artıklarını ve boşlukları sil
                        string email = match.Value.ToLower().Trim()
                                            .Replace("%22", "")
                                            .Replace("\"", "")
                                            .Replace("'", "");

                        if (IsSpam(email)) continue;

                        // Veritabanında mükerrer kayıt kontrolü
                        if (!db.Jobs.Any(x => x.Email == email))
                        {
                            db.Jobs.Add(new JobItem
                            {
                                Title = $"{keyword} İlanı ({city})",
                                Company = GetSourceFromUrl(query), // Hangi sorgudan geldiğini yazar
                                Email = email,
                                Url = "https://google.com",
                                Status = 0
                            });
                        }
                    }
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Arama sırasında hata oluştu: " + ex.Message);
                continue;
            }
        }
        await browser.CloseAsync();
    }

    private string GetSourceFromUrl(string query)
    {
        if (query.Contains("linkedin")) return "LinkedIn";
        if (query.Contains("kariyer.net")) return "Kariyer.net";
        return "Genel Web";
    }

    private bool IsSpam(string email)
    {
        // Uzantı kontrolü ve bilinen sistem mailleri
        string[] filters = { "google", "w3.org", "sentry", "example", "noreply", "support", "feedback", "domain.com" };
        return filters.Any(f => email.Contains(f)) || email.Length < 5;
    }
}