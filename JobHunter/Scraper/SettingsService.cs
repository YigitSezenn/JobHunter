using JobHunter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JobHunter.Scraper
{
    public class SettingsService
    {

        private static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");

        public static void Save(UserSettings settings)
        {
            try
            {
                string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Ayarlar kaydedilemedi: " + ex.Message);
            }
        }

        public static UserSettings Load()
        {
            if (!File.Exists(filePath)) return new UserSettings();
            try
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<UserSettings>(json) ?? new UserSettings();
            }
            catch { return new UserSettings(); }
        }
    }
        
}
