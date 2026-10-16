using HooPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HooPulse.Infrastructure.Persistence.Configurations;

public sealed class PlayerConfiguration
    : IEntityTypeConfiguration<Player>
{
    public void Configure(
        EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("players");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Position)
            .IsRequired();

        builder.HasOne(x => x.Club)
            .WithMany(x => x.Players)
            .HasForeignKey(x => x.ClubId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.ClubId);
        builder.HasIndex(x => new
        {
            x.FirstName,
            x.LastName
        });
    }
}