using LifeTrack.Core;
using LifeTrack.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace LifeTrack.Services.Data
{
    public class LifeTrackDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<AppUsage> AppUsages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Veritabanı dosyasının uzak bir sunucudaki dosyaya yönlendirildiğinden emin olun
                string dbPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "LifeTrackDb.db");

                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Kategori örnek verileri - ColorHex değerlerini ekleyin
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Yiyecek", IconName = "food", ColorHex = "#4CAF50" },
                new Category { Id = 2, Name = "Ulaşım", IconName = "transport", ColorHex = "#2196F3" },
                new Category { Id = 3, Name = "Kira", IconName = "home", ColorHex = "#9C27B0" },
                new Category { Id = 4, Name = "Eğlence", IconName = "entertainment", ColorHex = "#FF9800" },
                new Category { Id = 5, Name = "Diğer", IconName = "other", ColorHex = "#607D8B" }
            );
        }
    }
}