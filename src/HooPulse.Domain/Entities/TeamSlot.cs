using HooPulse.Domain.Common;
using HooPulse.Domain.Enums;

namespace HooPulse.Domain.Entities;

public sealed class TeamSlot : Entity
{
    public Guid FantasyTeamId { get; private set; }

    public Guid PlayerId { get; private set; }
    public Player Player { get; private set; }

    public SlotPosition Position { get; private set; }

    private TeamSlot()
    {
        Player = null!;
    }

    internal TeamSlot(
        Guid fantasyTeamId,
        Player player,
        SlotPosition position)
    {
        ArgumentNullException.ThrowIfNull(player);

        FantasyTeamId = fantasyTeamId;
        PlayerId = player.Id;
        Player = player;
        Position = position;
    }

    internal void ChangePosition(SlotPosition position)
    {
        Position = position;
    }
}