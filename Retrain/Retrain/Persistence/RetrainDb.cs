using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Retrain.Model;

namespace Retrain.Persistence
{
	public class RetrainDb : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Word> Words { get; set; }
        public RetrainDb(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("RetrainDb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Define many to many relation
            modelBuilder.Entity<Word>().HasKey(w => w.Id);
            modelBuilder.Entity<Word>().Property(w => w.WordStr).IsRequired();
        }
    }
}

