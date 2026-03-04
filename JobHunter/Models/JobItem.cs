using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunter.Models
{
    
        public class JobItem
        {
            public int Id { get; set; }
            public string Title { get; set; } = string.Empty;   
            public string Company { get; set; } = string.Empty; 
            public string? Email { get; set; }                  
            public string Url { get; set; } = string.Empty;      
            public DateTime FoundDate { get; set; } = DateTime.Now;

            // Durum: 0=Beklemede, 1=Başvuruldu, 2=Reddedildi
            public int Status { get; set; } = 0;
        }
    }
