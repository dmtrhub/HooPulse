using HooPulse.Domain.Common;
using HooPulse.Domain.Enums;

namespace HooPulse.Domain.Entities;

public sealed class Player : Entity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public string FullName => $"{FirstName} {LastName}";

    public PlayerPosition Position { get; private set; }

    public Guid ClubId { get; private set; }
    public Club Club { get; private set; }

    private Player()
    {
        FirstName = null!;
        LastName = null!;
        Club = null!;
    }

    public Player(
        string firstName,
        string lastName,
        PlayerPosition position,
        Club club)
    {
        ArgumentNullException.ThrowIfNull(club);

        ValidateName(firstName, nameof(firstName));
        ValidateName(lastName, nameof(lastName));

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Position = position;

        Club = club;
        ClubId = club.Id;

        club.AddPlayer(this);
    }

    public void Rename(
        string firstName,
        string lastName)
    {
        ValidateName(firstName, nameof(firstName));
        ValidateName(lastName, nameof(lastName));

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
    }

    public void ChangePosition(PlayerPosition position)
    {
        Position = position;
    }

    public void TransferTo(Club newClub)
    {
        ArgumentNullException.ThrowIfNull(newClub);

        if (ClubId == newClub.Id)
            return;

        Club.RemovePlayer(this);

        Club = newClub;
        ClubId = newClub.Id;

        newClub.AddPlayer(this);
    }

    private static void ValidateName(
        string value,
        string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                "Player name cannot be empty.",
                parameterName);
        }
    }
}