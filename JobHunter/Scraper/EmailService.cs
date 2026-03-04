using MailKit.Net.Smtp;
using MimeKit;
using JobHunter.Models;
using System.IO;

namespace JobHunter.Services
{
    // Sınıf ismini 's' yerine 'EmailService' yaptık
    public class EmailService
    {
        public async Task<bool> SendApplicationAsync(
            JobItem job,
            string senderEmail,
            string appPassword,
            string fullName,
            string coverLetter,
            string pdfPath)
        {
            try
            {
                var message = new MimeMessage();

                // 1. Gönderen (Senin Adın) ve Alıcı Bilgileri
                message.From.Add(new MailboxAddress(fullName, senderEmail));
                message.To.Add(new MailboxAddress("İşe Alım Sorumlusu ", job.Email));

                // 2. Konu Başlığı
                message.Subject = $"{job.Title} Pozisyonu Hakkında İş Başvurusu - {fullName}";

                // 3. Önyazı Kontrolü (Boşsa varsayılan metni kullan)
                string finalBody = string.IsNullOrWhiteSpace(coverLetter)
                    ? $"Sayın İlgili,\n\n{job.Company} bünyesinde yayınlanan {job.Title} ilanınızla ilgileniyorum. " +
                      $"Yetkinliklerimin pozisyon için uygun olduğunu düşünüyorum. Özgeçmişim ektedir.\n\n" +
                      $"Değerlendirmeniz için teşekkürler.\n\nİyi çalışmalar,\n{fullName}"
                    : coverLetter;

                var body = new TextPart("plain") { Text = finalBody };

                // 4. PDF Dosyası Kontrolü (Dışarıdan gelen yol)
                if (!File.Exists(pdfPath))
                {
                    throw new FileNotFoundException("Seçilen PDF dosyası bulunamadı: " + pdfPath);
                }

                var attachment = new MimePart("application", "pdf")
                {
                    Content = new MimeContent(File.OpenRead(pdfPath)),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = Path.GetFileName(pdfPath) // Dosyanın orijinal ismini gönderir
                };

                var multipart = new Multipart("mixed") { body, attachment };
                message.Body = multipart;

                // 5. SMTP Gönderimi
                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(senderEmail, appPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Mail Hatası: " + ex.Message);
                return false;
            }
        }
    }
}