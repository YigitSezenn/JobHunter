using JobHunter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunter.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<JobItem> Jobs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Veritabanı dosyasının tam yolunu belirle
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jobs.db");
            optionsBuilder.UseSqlite($"Data Source={path}");
        }
    }
}
