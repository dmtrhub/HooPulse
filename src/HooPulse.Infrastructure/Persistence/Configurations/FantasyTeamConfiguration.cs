using HooPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HooPulse.Infrastructure.Persistence.Configurations;

public sealed class FantasyTeamConfiguration
    : IEntityTypeConfiguration<FantasyTeam>
{
    public void Configure(
        EntityTypeBuilder<FantasyTeam> builder)
    {
        builder.ToTable("fantasy_teams");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.OwnerId)
            .IsRequired();

        builder.HasIndex(x => x.OwnerId);
    }
}