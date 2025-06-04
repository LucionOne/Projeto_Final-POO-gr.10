namespace Lib.TeamFormation;

public class TeamFormation
{
    public readonly int MaxPlayers;
    public readonly int NumberGoalkeepers = 1;
    public readonly int NumberDefenders;
    public readonly int NumberAttackers;
    public readonly int NumberAny;

    public bool IsValid => MaxPlayers == (NumberGoalkeepers + NumberDefenders + NumberAttackers + NumberAny);

    public TeamFormation()
    {
        MaxPlayers = 11; // Default value
        NumberDefenders = 3; // Default value
        NumberAttackers = 3; // Default value
        NumberAny = 4; // Default value
    }


    public TeamFormation(int maxPlayers, int numberGoalkeepers, int numberDefenders, int numberAttackers, int numberAny = 0)
    {
        MaxPlayers = maxPlayers;
        NumberGoalkeepers = numberGoalkeepers;
        NumberDefenders = numberDefenders;
        NumberAttackers = numberAttackers;
        NumberAny = numberAny;
    }
}