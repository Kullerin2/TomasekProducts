using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using TomasekRestApi.Model.Cryptography;

namespace TomasekRestApi.Model.Models
{
    public partial class AlzaTestDBContext : DbContext
    {
        public AlzaTestDBContext()
        {
        }

        public AlzaTestDBContext(DbContextOptions<AlzaTestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

                var connectionStringEncrypted = configuration.GetConnectionString("WebApiDatabase");
                var connectionStringDecrypted = CryptoHelper.DecryptString(connectionStringEncrypted);                
                optionsBuilder.UseSqlServer(connectionStringDecrypted);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
