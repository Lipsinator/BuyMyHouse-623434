﻿using Domain.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EFContext
{
    public class MortgageApplicationContext : DbContext
    {
        public DbSet<MortgageApplication> MortgageApplications { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
                "https://localhost:8081",
                "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                databaseName: "BuildMyHouseDB");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MortgageApplication>()
                .ToContainer("MortgageApplications");

            modelBuilder.Entity<MortgageApplication>()
                .HasNoDiscriminator();

            modelBuilder.Entity<MortgageApplication>()
                .HasPartitionKey(o => o.id);

            modelBuilder.Entity<MortgageApplication>()
                .UseETagConcurrency();
        }
    }
}