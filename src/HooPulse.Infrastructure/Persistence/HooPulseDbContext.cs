using HooPulse.Application.Abstractions.Persistence;
using HooPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HooPulse.Infrastructure.Persistence;

public sealed class HooPulseDbContext : DbContext, IApplicationDbContext
{
    public HooPulseDbContext(
        DbContextOptions<HooPulseDbContext> options)
        : base(options)
    {
    }

    public DbSet<Player> Players => Set<Player>();

    public DbSet<Club> Clubs => Set<Club>();

    public DbSet<Match> Matches => Set<Match>();

    public DbSet<FantasyTeam> FantasyTeams => Set<FantasyTeam>();

    public DbSet<TeamSlot> TeamSlots => Set<TeamSlot>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(HooPulseDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}