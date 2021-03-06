using Domain.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EFContext
{
    public class BuyerInfoContext : DbContext
    {
        public DbSet<BuyerInfo> BuyerInfos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
                "https://localhost:8081",
                "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                databaseName: "BuildMyHouseDB");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuyerInfo>()
                .ToContainer("BuyerInfos");

            modelBuilder.Entity<BuyerInfo>()
                .HasKey(b => b.id);

            modelBuilder.Entity<BuyerInfo>()
                .HasNoDiscriminator();

            modelBuilder.Entity<BuyerInfo>()
                .HasPartitionKey(o => o.ZipCode);

            modelBuilder.Entity<BuyerInfo>()
                .UseETagConcurrency();
        }
    }
}
