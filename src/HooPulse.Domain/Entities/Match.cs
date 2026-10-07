using HooPulse.Domain.Common;
using HooPulse.Domain.Enums;

namespace HooPulse.Domain.Entities;

public sealed class Match : Entity
{
    public Guid HomeClubId { get; private set; }
    public Club HomeClub { get; private set; }

    public Guid AwayClubId { get; private set; }
    public Club AwayClub { get; private set; }

    public DateTimeOffset ScheduledAt { get; private set; }

    public MatchStatus Status { get; private set; }

    public int? HomeScore { get; private set; }
    public int? AwayScore { get; private set; }

    private Match()
    {
        HomeClub = null!;
        AwayClub = null!;
    }

    public Match(
        Club homeClub,
        Club awayClub,
        DateTimeOffset scheduledAt)
    {
        ArgumentNullException.ThrowIfNull(homeClub);
        ArgumentNullException.ThrowIfNull(awayClub);

        if (homeClub.Id == awayClub.Id)
        {
            throw new ArgumentException(
                "A club cannot play against itself.");
        }

        HomeClub = homeClub;
        HomeClubId = homeClub.Id;

        AwayClub = awayClub;
        AwayClubId = awayClub.Id;

        ScheduledAt = scheduledAt;
        Status = MatchStatus.Scheduled;
    }

    public void Start()
    {
        if (Status != MatchStatus.Scheduled)
        {
            throw new InvalidOperationException(
                "Only scheduled matches can be started.");
        }

        Status = MatchStatus.Live;

        HomeScore = 0;
        AwayScore = 0;
    }

    public void UpdateScore(
        int homeScore,
        int awayScore)
    {
        EnsureLive();

        ValidateScore(homeScore, awayScore);

        HomeScore = homeScore;
        AwayScore = awayScore;
    }

    public void Finish(
        int homeScore,
        int awayScore)
    {
        EnsureLive();

        ValidateScore(homeScore, awayScore);

        HomeScore = homeScore;
        AwayScore = awayScore;
        Status = MatchStatus.Finished;
    }

    public void Postpone()
    {
        if (Status is MatchStatus.Finished or MatchStatus.Cancelled)
        {
            throw new InvalidOperationException(
                "Finished or cancelled matches cannot be postponed.");
        }

        Status = MatchStatus.Postponed;
    }

    public void Reschedule(DateTimeOffset scheduledAt)
    {
        if (Status is MatchStatus.Live or MatchStatus.Finished)
        {
            throw new InvalidOperationException(
                "Live or finished matches cannot be rescheduled.");
        }

        ScheduledAt = scheduledAt;
        Status = MatchStatus.Scheduled;
    }

    public void Cancel()
    {
        if (Status == MatchStatus.Finished)
        {
            throw new InvalidOperationException(
                "Finished matches cannot be cancelled.");
        }

        Status = MatchStatus.Cancelled;
    }

    private void EnsureLive()
    {
        if (Status != MatchStatus.Live)
        {
            throw new InvalidOperationException(
                "Match must be live.");
        }
    }

    private static void ValidateScore(
        int homeScore,
        int awayScore)
    {
        if (homeScore < 0 || awayScore < 0)
        {
            throw new ArgumentException(
                "Scores cannot be negative.");
        }
    }
}