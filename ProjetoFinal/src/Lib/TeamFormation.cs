namespace Lib.TeamFormation;

public class TeamFormation
{
    public readonly int MaxPlayers;
    public readonly int NumberGoalkeepers = 1;
    public readonly int NumberDefenders;
    public readonly int NumberAttackers;
    public readonly int NumberAny = 0;
    public bool UsingFormation = false;

    public bool IsValid => MaxPlayers == (NumberGoalkeepers + NumberDefenders + NumberAttackers + NumberAny);



    public TeamFormation()
    {
        UsingFormation = false;
        MaxPlayers = 11; // Default value
        // NumberDefenders = 7; // Default value
        // NumberAttackers = 3; // Default value
        // NumberGoalkeepers = 1; // Default value
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