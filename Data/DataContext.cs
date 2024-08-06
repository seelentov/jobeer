
using Microsoft.EntityFrameworkCore;
using Jobeer.Models;
using Jobeer.Models.Base;

namespace Jobber.Data
{
    public class DataContext : DbContext
    {
        public DbSet<SearchModel> SearchModels { get; set; }

        public string DbPath { get; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Debug);
        }
    }
}
