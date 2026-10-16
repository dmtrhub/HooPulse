using HooPulse.Application.Abstractions.Persistence;
using HooPulse.Application.Features.Players.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HooPulse.Application.Features.Players.Queries;

public sealed class GetPlayersQueryHandler(
    IApplicationDbContext context)
    : IRequestHandler<GetPlayersQuery, IReadOnlyList<PlayerDto>>
{
    public async Task<IReadOnlyList<PlayerDto>> Handle(
        GetPlayersQuery request,
        CancellationToken cancellationToken)
    {
        return await context.Players
            .AsNoTracking()
            .OrderBy(player => player.LastName)
            .ThenBy(player => player.FirstName)
            .Select(player => new PlayerDto(
                player.Id,
                player.FirstName,
                player.LastName,
                player.FirstName + " " + player.LastName,
                player.Position,
                player.ClubId))
            .ToListAsync(cancellationToken);
    }
}
