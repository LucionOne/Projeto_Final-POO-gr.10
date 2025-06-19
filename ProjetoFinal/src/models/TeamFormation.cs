using System.Text.Json.Serialization;

namespace Lib.TeamFormation;

public class TeamFormation
{
    public int MaxPlayers { get; set; }
    public int NumberGoalkeepers { get; set; } = 1;
    public int NumberDefenders { get; set; }
    public int NumberAttackers { get; set; }
    public int NumberAny { get; set; } = 0;
    public bool UsingFormation { get; set; } = false;

    [JsonIgnore]
    public bool IsValid => MaxPlayers == (NumberGoalkeepers + NumberDefenders + NumberAttackers + NumberAny);

    public TeamFormation() { }

    public TeamFormation(bool _using)
    {
        UsingFormation = _using;
    }

    public TeamFormation(int MaxPlayers, bool UsingFormation)
    {
        this.UsingFormation = UsingFormation;
        this.MaxPlayers = MaxPlayers;
    }

    public TeamFormation(int maxPlayers, int numberGoalkeepers, int numberDefenders, int numberAttackers, int numberAny = 0)
    {
        MaxPlayers = maxPlayers;
        NumberGoalkeepers = numberGoalkeepers;
        NumberDefenders = numberDefenders;
        NumberAttackers = numberAttackers;
        NumberAny = numberAny;
    }

    public TeamFormation(TeamFormation package)
    {
        if (package == null)
        {
            throw new ArgumentNullException(nameof(package), "TeamFormation cannot be null");
        }

        MaxPlayers = package.MaxPlayers;
        NumberGoalkeepers = package.NumberGoalkeepers;
        NumberDefenders = package.NumberDefenders;
        NumberAttackers = package.NumberAttackers;
        NumberAny = package.NumberAny;
    }

    public static bool isValid(TeamFormation formation)
    {
        return formation != null &&
        (formation.MaxPlayers == (formation.NumberAttackers + formation.NumberDefenders + formation.NumberGoalkeepers));
    }
}
