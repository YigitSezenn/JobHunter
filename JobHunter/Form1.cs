using JobHunter.Data;
using JobHunter.Models;
using JobHunter.Scraper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace JobHunter
{
    public partial class Form1 : Form
    {
        private bool isPdfUploaded = false;
        private string? _pdfPath = "";

        public Form1()
        {
            InitializeComponent();
            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();
            }
        }

        private async void button_IlanEkle_Click(object sender, EventArgs e)
        {
            if (!isPdfUploaded || string.IsNullOrEmpty(_pdfPath))
            {
                MessageBox.Show("Lütfen önce bir PDF (CV) yükleyiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Lütfen hem pozisyon (keyword) hem de şehir giriniz!", "Uyarı");
                return;
            }
            try
            {
                // 2. Arayüzü Hazırla
                button_IlanEkle.Enabled = false;
                button_IlanEkle.Text = "Aranıyor...";

                // 3. Scraper'ı Çalıştır
                var scraper = new JobScraper();
                await scraper.SearchJobs(textBox1.Text, textBox2.Text);

                // Form2'yi oluştur ve verileri yüklemesini bekle
                Form2 listeformu = new Form2(_pdfPath);
                listeformu.LoadData(); // Veritabanından verileri çekmesi için zorla
                listeformu.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arama sırasında bir hata oluştu: " + ex.Message);
            }
            finally
            {
                button_IlanEkle.Enabled = true;
                button_IlanEkle.Text = "İlan Bul";
            }

        }

        public void txtKeyword(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_Pdf_Upload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PDF Dosyaları (*.pdf)|*.pdf";
                openFileDialog.Title = "Lütfen CV dosyanızı seçin";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Seçilen tam yolu değişkene atıyoruz (En önemli kısım burası!)
                    _pdfPath = openFileDialog.FileName;
                    isPdfUploaded = true;

                    // Görsel Geri Bildirim
                    btn_Pdf_Upload.BackColor = Color.LightGreen;
                    btn_Pdf_Upload.Text = "PDF Yüklendi ✅";
                    label1.Text = "Dosya: " + openFileDialog.SafeFileName;
                    label1.ForeColor = Color.DarkGreen;

                    MessageBox.Show("PDF başarıyla tanımlandı. Artık ilan arayabilirsiniz.", "Başarılı");
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}
