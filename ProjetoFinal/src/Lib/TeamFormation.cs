namespace Lib.TeamFormation;

public class TeamFormation
{
    public readonly int MaxPlayers;
    public readonly int NumberGoalkeepers = 1;
    public readonly int NumberDefenders;
    public readonly int NumberAttackers;
    public readonly int NumberAny = 0;

    public bool IsValid => MaxPlayers == (NumberGoalkeepers + NumberDefenders + NumberAttackers + NumberAny);

    public TeamFormation()
    {
        MaxPlayers = 11; // Default value
        NumberDefenders = 7; // Default value
        NumberAttackers = 3; // Default value
        NumberGoalkeepers = 1; // Default value
    }


    public TeamFormation(int maxPlayers, int numberGoalkeepers, int numberDefenders, int numberAttackers, int numberAny = 0)
    {
        MaxPlayers = maxPlayers;
        NumberGoalkeepers = numberGoalkeepers;
        NumberDefenders = numberDefenders;
        NumberAttackers = numberAttackers;
        NumberAny = numberAny;
    }

    public static bool validFormation(TeamFormation formation)
    {
        return formation != null &&
        (formation.MaxPlayers == (formation.NumberAttackers+formation.NumberDefenders+formation.NumberGoalkeepers));
               
    }

}