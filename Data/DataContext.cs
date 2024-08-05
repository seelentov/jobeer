
using Microsoft.EntityFrameworkCore;
using Jobeer.Models;
using Jobeer.Models.Base;

namespace Jobber.Data
{
    public class DataContext : DbContext
    {
        public DbSet<SearchModel> SearchModels { get; set; }
        public DbSet<NotifCache> NotifCache { get; set; }

        public string DbPath { get; }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                }
                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }
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
