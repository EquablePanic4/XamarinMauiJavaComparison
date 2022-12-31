using CenyWPolsce.Data.Tables;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace CenyWPolsce.Data
{
    public class ApplicationDbContext : DbContext
    {
        public static string DatabasePath { get; set; }

        public static string ConnectionString
        {
            get => Environment.OSVersion.Platform switch
            {
                PlatformID.Win32NT => $"Data Source={DatabasePath}",
                _ => $"Filename={DatabasePath}"
            };
        }

        static ApplicationDbContext()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                DatabasePath = "D:\\ceny.db3";
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(ConnectionString);

        public DbSet<Product> Products { get; set; }
    }
}
