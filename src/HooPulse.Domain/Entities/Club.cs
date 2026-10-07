using HooPulse.Domain.Common;

namespace HooPulse.Domain.Entities;

public sealed class Club : Entity
{
    private readonly List<Player> _players = [];

    public string Name { get; private set; }
    public string ShortName { get; private set; }
    public string Country { get; private set; }

    public IReadOnlyCollection<Player> Players => _players.AsReadOnly();

    private Club()
    {
        Name = null!;
        ShortName = null!;
        Country = null!;
    }

    public Club(
        string name,
        string shortName,
        string country)
    {
        ValidateName(name);
        ValidateShortName(shortName);
        ValidateCountry(country);

        Name = name.Trim();
        ShortName = shortName.Trim().ToUpperInvariant();
        Country = country.Trim();
    }

    public void Rename(string name)
    {
        ValidateName(name);

        Name = name.Trim();
    }

    public void ChangeShortName(string shortName)
    {
        ValidateShortName(shortName);

        ShortName = shortName.Trim().ToUpperInvariant();
    }

    public void ChangeCountry(string country)
    {
        ValidateCountry(country);

        Country = country.Trim();
    }

    internal void AddPlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        if (_players.Any(x => x.Id == player.Id))
            return;

        _players.Add(player);
    }

    internal void RemovePlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        _players.Remove(player);
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Club name cannot be empty.",
                nameof(name));
    }

    private static void ValidateShortName(string shortName)
    {
        if (string.IsNullOrWhiteSpace(shortName))
            throw new ArgumentException(
                "Club short name cannot be empty.",
                nameof(shortName));
    }

    private static void ValidateCountry(string country)
    {
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException(
                "Club country cannot be empty.",
                nameof(country));
    }
}