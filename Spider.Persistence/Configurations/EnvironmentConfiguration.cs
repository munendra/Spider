using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spider.Domain.Entities;

namespace Spider.Persistence.Configurations
{
    public class EnvironmentConfiguration : IEntityTypeConfiguration<Environment>
    {
        public void Configure(EntityTypeBuilder<Environment> builder)
        {
            builder.Property(a => a.ApplicationId).IsRequired();

            builder.Property(e => e.ApplicationId)
                .HasColumnName("ApplicationId");

            builder.HasOne(x=>x.Application)
                .WithMany(app => app.Environments)
                .HasForeignKey(e => e.ApplicationId);

            //builder.HasOne(x => x.Application)
            //    .WithMany(x => x.Environments)
            //    .HasForeignKey(x => x.ApplicationId);



            builder.Property(a => a.Name)
                .IsRequired();

            builder.Property(a => a.Rowversion).IsRowVersion();

            builder.ToTable("Environments");
        }
    }
}