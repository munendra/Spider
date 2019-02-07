using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Spider.Domain.Entities;
using Spider.Persistence.Extensions;
using Environment = Spider.Domain.Entities.Environment;

namespace Spider.Persistence
{
    public class SpiderDbContext : DbContext
    {
        public SpiderDbContext(DbContextOptions<SpiderDbContext> options) : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Configuration> Configurations { get; set; }

        public DbSet<Environment> Environments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations();
        }

        public override int SaveChanges()
        {
            SetAuditColumnValue();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            SetAuditColumnValue();
            return this.SaveChangesAsync(true, cancellationToken);
        }

        private void SetAuditColumnValue()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (EntityEntry entry in modifiedEntries)
            {
                var entityType = entry.Context.Model.FindEntityType(entry.Entity.GetType());

                var modifiedProperty = entityType.FindProperty("SysEditedDateTime");
                var createdProperty = entityType.FindProperty("SysCreateDateTime");

                if (entry.State == EntityState.Modified && modifiedProperty != null)
                {
                    entry.Property("SysEditedDateTime").CurrentValue = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added && createdProperty != null)
                {
                    entry.Property("SysCreateDateTime").CurrentValue = DateTime.UtcNow;
                }
            }
        }
    }
}