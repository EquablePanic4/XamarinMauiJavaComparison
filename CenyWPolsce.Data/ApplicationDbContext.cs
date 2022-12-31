using CenyWPolsce.Data.Tables;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace CenyWPolsce.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _dbPath;
        public ApplicationDbContext()
        {
            _dbPath = Environment.OSVersion.Platform switch
            {
                PlatformID.Win32NT => "D:\\ceny.db3",
                _ => "/storage/emulated/0/MojFolder/ceny.db3"
            };
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={_dbPath}");

        public DbSet<Product> Products { get; set; }
    }
}
