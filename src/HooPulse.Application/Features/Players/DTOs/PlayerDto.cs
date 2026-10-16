using HooPulse.Domain.Enums;

namespace HooPulse.Application.Features.Players.DTOs;

public sealed record PlayerDto(
    Guid Id,
    string FirstName,
    string LastName,
    string FullName,
    PlayerPosition Position,
    Guid ClubId);
