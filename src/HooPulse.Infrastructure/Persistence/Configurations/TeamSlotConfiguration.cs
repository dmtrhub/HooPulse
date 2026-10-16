using HooPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HooPulse.Infrastructure.Persistence.Configurations;

public sealed class TeamSlotConfiguration
    : IEntityTypeConfiguration<TeamSlot>
{
    public void Configure(
        EntityTypeBuilder<TeamSlot> builder)
    {
        builder.ToTable("team_slots");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Position)
            .IsRequired();

        builder.HasOne(x => x.Player)
            .WithMany()
            .HasForeignKey(x => x.PlayerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.PlayerId);

        builder.HasIndex(x => new
            {
                x.FantasyTeamId,
                x.Position
            })
            .IsUnique();

        builder.HasIndex(x => new
            {
                x.FantasyTeamId,
                x.PlayerId
            })
            .IsUnique();
    }
}