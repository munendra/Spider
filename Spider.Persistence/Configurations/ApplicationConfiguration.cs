using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spider.Domain.Entities;

namespace Spider.Persistence.Configurations
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.HasMany(c => c.Environments)
                .WithOne(e => e.Application);

            builder.Property(a => a.Name)
                .IsRequired();
            builder.Property(a => a.Rowversion).IsRowVersion();

            builder.ToTable("Application");
        }
    }
}