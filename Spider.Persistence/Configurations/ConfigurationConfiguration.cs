using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spider.Domain.Entities;

namespace Spider.Persistence.Configurations
{
    public class ConfigurationConfiguration : IEntityTypeConfiguration<Configuration>
    {
        public void Configure(EntityTypeBuilder<Configuration> builder)
        {
            builder.Property(a => a.Key)
                .IsRequired();

            builder.Property(a => a.Value)
                .IsRequired();

            builder.Property(a => a.IsActive)
                .IsRequired();

            builder.Property(a => a.EnvironmentId)
                .IsRequired();

            builder.HasOne(x => x.Environment)
                .WithMany(app => app.Configurations)
                .HasForeignKey(e => e.EnvironmentId);

            builder.Property(a => a.RowVersion).IsRowVersion();

            builder.ToTable("Configuration");
        }
    }
}