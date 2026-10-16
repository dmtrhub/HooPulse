using HooPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HooPulse.Infrastructure.Persistence.Configurations;

public sealed class MatchConfiguration
    : IEntityTypeConfiguration<Match>
{
    public void Configure(
        EntityTypeBuilder<Match> builder)
    {
        builder.ToTable("matches");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ScheduledAt)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.HasOne(x => x.HomeClub)
            .WithMany()
            .HasForeignKey(x => x.HomeClubId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.AwayClub)
            .WithMany()
            .HasForeignKey(x => x.AwayClubId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.ScheduledAt);
        builder.HasIndex(x => x.Status);
    }
}