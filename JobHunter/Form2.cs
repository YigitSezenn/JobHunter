using JobHunter.Data;
using JobHunter.Models;
using JobHunter.Scraper;
using JobHunter.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace JobHunter
{
    public partial class Form2 : Form
    {
        private string _pdfPath; // Form1'den gelen yolu tutacak değişken
        public Form2(string pdfPath)
        {
            InitializeComponent();
            LoadData();
            _pdfPath = pdfPath;

        }

        public void LoadData()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    // DB dosyasının oluştuğundan emin ol
                    db.Database.EnsureCreated();

                    // Önce filtrelemeden tüm sayıya bakalım (Debug için)
                    var tumKayitlar = db.Jobs.Count();

                    // Sadece Status=0 olanları çek
                    var liste = db.Jobs.Where(x => x.Status == 0).ToList();

                    dgvJobs.DataSource = null;

                    if (liste.Count > 0)
                    {
                        dgvJobs.DataSource = liste;
                        this.Text = $"Toplam {liste.Count} yeni ilan bulundu.";
                    }
                    else
                    {
                        this.Text = $"DB'de toplam {tumKayitlar} kayıt var, ama Status=0 olan yok.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yükleme hatası: " + ex.Message);
            }
        }

        // Bu metodu boş bırakabilirsin veya silebilirsin (Tasarım tarafında bağlı değilse)
        private void dgvJobs_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private async void btn_SendMail_Click(object sender, EventArgs e)
        {
            // 1. Seçim Kontrolü
            var settings = SettingsService.Load();
            if (string.IsNullOrEmpty(settings.GondericiEmail) || string.IsNullOrEmpty(settings.UygulamaSifresi))
            {
                MessageBox.Show("Lütfen önce ayarlar kısmından mail ve uygulama şifrenizi kaydedin!");
                return;
            }

            // 2. Seçim ve TextBox Kontrolleri
            if (dgvJobs.CurrentRow == null)
            {
                MessageBox.Show("Lütfen önce listeden bir ilan seçin!");
                return;
            }

            string adSoyad = textBox1.Text.Trim(); // Form üzerindeki Ad-Soyad
            string onYazi = textBox2.Text.Trim();  // Form üzerindeki Ön Yazı

            if (string.IsNullOrEmpty(adSoyad))
            {
                MessageBox.Show("Lütfen adınızı ve soyadınızı giriniz.");
                return;
            }

            var selectedJob = (JobItem)dgvJobs.CurrentRow.DataBoundItem;

            if (string.IsNullOrEmpty(selectedJob.Email) || selectedJob.Email.Contains("sirket.com"))
            {
                MessageBox.Show("Bu ilanın e-posta adresi geçerli görünmüyor.");
                return;
            }

            // 3. Gönderim İşlemi
            btn_SendMail.Enabled = false;
            var emailService = new EmailService();

            try
            {
                bool sonuc = await emailService.SendApplicationAsync(
                    selectedJob,
                    settings.GondericiEmail, // Ayarlardan gelen mail
                    settings.UygulamaSifresi, // Ayarlardan gelen şifre
                    adSoyad,
                    onYazi,
                    _pdfPath
                );

                if (sonuc)
                {
                    MessageBox.Show("Başvuru başarıyla gönderildi!");
                    using (var db = new AppDbContext())
                    {
                        var job = db.Jobs.Find(selectedJob.Id);
                        if (job != null) { job.Status = 1; db.SaveChanges(); }
                    }
                    LoadData(); // Listeyi yenile
                }
                else
                {
                    MessageBox.Show("Mail gönderilirken bir hata oluştu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                btn_SendMail.Enabled = true;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var settings = SettingsService.Load();
            textBox3.Text = settings.GondericiEmail;
            textBox4.Text = settings.UygulamaSifresi;
            textBox1.Text = settings.AdSoyad;
            textBox2.Text = settings.VarsayilanOnYazi;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            var settings = SettingsService.Load();
            if (string.IsNullOrEmpty(settings.GondericiEmail))
            {
                MessageBox.Show("Lütfen önce ayarlar kısmından mail bilgilerinizi kaydedin!");
                return;
            }

            if (dgvJobs.Rows.Count == 0)
            {
                MessageBox.Show("Başvurulacak ilan bulunamadı!");
                return;
            }

            var confirm = MessageBox.Show($"Listeye eklenen {dgvJobs.Rows.Count} ilanın tamamına mail gönderilecek. Onaylıyor musunuz?",
                                          "Toplu Başvuru", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            // 2. Hazırlık
            btn_ApplyAll.Enabled = false;
            btn_SendMail.Enabled = false;

            string adSoyad = textBox1.Text.Trim();
            string onYazi = textBox2.Text.Trim();
            int basarili = 0;
            int hatali = 0;

            var emailService = new EmailService();

            // 3. Döngü
            foreach (DataGridViewRow row in dgvJobs.Rows)
            {
                if (row.DataBoundItem is JobItem selectedJob)
                {
                    try
                    {
                        btn_ApplyAll.Text = $"Gönderiliyor: {selectedJob.Email}...";

                        bool sonuc = await emailService.SendApplicationAsync(
                            selectedJob,
                            settings.GondericiEmail,  // Dinamik mail
                            settings.UygulamaSifresi, // Dinamik şifre
                            adSoyad,
                            onYazi,
                            _pdfPath
                        );

                        if (sonuc)
                        {
                            basarili++;
                            using (var db = new AppDbContext())
                            {
                                var job = db.Jobs.Find(selectedJob.Id);
                                if (job != null) { job.Status = 1; db.SaveChanges(); }
                            }
                        }
                        else { hatali++; }

                        // Spam engellemek için her mail arası 3 saniye bekle
                        await Task.Delay(3000);
                    }
                    catch { hatali++; }
                }
            }

            // 4. Bitiş
            MessageBox.Show($"İşlem tamamlandı!\nBaşarılı: {basarili}\nHatalı: {hatali}");

            btn_ApplyAll.Enabled = true;
            btn_ApplyAll.Text = "Hepsine Başvur";
            btn_SendMail.Enabled = true;

            LoadData(); // Listeyi güncelle (Status=1 olanlar listeden düşer)
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            var settings = new UserSettings
            {
                AdSoyad = textBox1.Text,
                GondericiEmail = textBox3.Text,
                UygulamaSifresi = textBox4.Text,
                VarsayilanOnYazi = textBox2.Text



            };
            SettingsService.Save(settings);
            MessageBox.Show("Ayarlar kaydedildi!");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            var settings = new UserSettings
            {
                GondericiEmail = textBox3.Text,
                UygulamaSifresi = textBox4.Text,
                AdSoyad = textBox1.Text,
                VarsayilanOnYazi = textBox2.Text
            };
            SettingsService.Save(settings);
        }
    }
}