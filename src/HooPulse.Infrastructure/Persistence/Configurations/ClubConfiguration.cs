using HooPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HooPulse.Infrastructure.Persistence.Configurations;

public sealed class ClubConfiguration
    : IEntityTypeConfiguration<Club>
{
    public void Configure(
        EntityTypeBuilder<Club> builder)
    {
        builder.ToTable("clubs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.ShortName)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Country)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(x => x.ShortName)
            .IsUnique();
    }
}