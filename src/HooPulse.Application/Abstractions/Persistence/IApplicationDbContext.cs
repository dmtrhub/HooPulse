using HooPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HooPulse.Application.Abstractions.Persistence;

public interface IApplicationDbContext
{
    DbSet<Player> Players { get; }

    DbSet<Club> Clubs { get; }

    DbSet<Match> Matches { get; }

    DbSet<FantasyTeam> FantasyTeams { get; }

    DbSet<TeamSlot> TeamSlots { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}