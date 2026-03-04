using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunter.Models
{
    public class UserSettings
    {
        public string GondericiEmail { get; set; }
        public string UygulamaSifresi { get; set; } // Gmail App Password
        public string AdSoyad { get; set; }
        public string VarsayilanOnYazi { get; set; }
        public string VarsayilanPdfYolu { get; set; }
    }
}
