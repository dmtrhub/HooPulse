using HooPulse.Application.Features.Players.DTOs;
using MediatR;

namespace HooPulse.Application.Features.Players.Queries;

public sealed record GetPlayersQuery
    : IRequest<IReadOnlyList<PlayerDto>>;
