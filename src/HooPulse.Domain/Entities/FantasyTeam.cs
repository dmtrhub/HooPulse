using HooPulse.Domain.Common;
using HooPulse.Domain.Enums;

namespace HooPulse.Domain.Entities;

public sealed class FantasyTeam : Entity
{
    private readonly List<TeamSlot> _slots = [];

    public string Name { get; private set; }

    public Guid OwnerId { get; private set; }

    public IReadOnlyCollection<TeamSlot> Slots =>
        _slots.AsReadOnly();

    private FantasyTeam()
    {
        Name = null!;
    }

    public FantasyTeam(
        string name,
        Guid ownerId)
    {
        ValidateName(name);

        if (ownerId == Guid.Empty)
        {
            throw new ArgumentException(
                "Owner ID cannot be empty.",
                nameof(ownerId));
        }

        Name = name.Trim();
        OwnerId = ownerId;
    }

    public void Rename(string name)
    {
        ValidateName(name);

        Name = name.Trim();
    }

    public void AddPlayer(
        Player player,
        SlotPosition position)
    {
        ArgumentNullException.ThrowIfNull(player);

        if (_slots.Any(x => x.PlayerId == player.Id))
        {
            throw new InvalidOperationException(
                "Player is already part of this fantasy team.");
        }

        if (_slots.Any(x => x.Position == position))
        {
            throw new InvalidOperationException(
                $"The {position} slot is already occupied.");
        }

        _slots.Add(
            new TeamSlot(
                Id,
                player,
                position));
    }

    public void RemovePlayer(Guid playerId)
    {
        var slot = GetSlot(playerId);

        _slots.Remove(slot);
    }

    public void MovePlayer(
        Guid playerId,
        SlotPosition newPosition)
    {
        var slot = GetSlot(playerId);

        if (slot.Position == newPosition)
            return;

        if (_slots.Any(x =>
                x.Position == newPosition &&
                x.PlayerId != playerId))
        {
            throw new InvalidOperationException(
                $"The {newPosition} slot is already occupied.");
        }

        slot.ChangePosition(newPosition);
    }

    private TeamSlot GetSlot(Guid playerId)
    {
        var slot = _slots.FirstOrDefault(
            x => x.PlayerId == playerId);

        return slot
            ?? throw new InvalidOperationException(
                "Player is not part of this fantasy team.");
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Fantasy team name cannot be empty.",
                nameof(name));
        }
    }
}