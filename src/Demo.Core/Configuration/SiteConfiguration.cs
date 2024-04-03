using Demo.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Core.Configuration;

public class SiteConfiguration : IEntityTypeConfiguration<Site>
{
    public void Configure(EntityTypeBuilder<Site> builder)
    {
        builder.ToTable("sites");
        builder.Property(x => x.Id)
            .HasColumnName("id");
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(256);
    }
}