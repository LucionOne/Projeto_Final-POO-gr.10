using Models;
using Lib.TeamFormation;
using System.Xml.Schema;

namespace Lit.TeamBuilder;

public static class TeamBuilder
{

    public enum PlayerPosition
    {
        Any,
        Goalkeepers,
        Defender,
        Attackers,
        Corrupted
    }

    public static List<Player>? AllAnyPlayersSelection(this List<Player> players, TeamFormation formation)
    {
        List<Player> playersList = players.ToList();

        playersList.Shuffle();

        bool valid = playersList.Count <= formation.MaxPlayers;

        if (valid)
        {
            List<Player> PlayerSelection = new();

            for (int i = 0; i < formation.MaxPlayers; i++)
            {
                var player = playersList[i];
                PlayerSelection.Add(player);
                playersList.RemoveAt(i);
            }
            return PlayerSelection;
        }
        return null;
    }

    public static List<Player>? BasicPlayersSelection(this List<Player> players, TeamFormation formation, out PlayerPosition error)
    {
        List<Player> playersList = players.ToList();

        playersList.Shuffle();

        (var valid, error) = Validate(playersList, formation);

        if (valid)
        {
            int totalGoalkeepers = 0, totalDefenders = 0, totalAttackers = 0;
            List<Player> playersSelection = new();

            for (int i = 0; i < playersList.Count;)
            {

                var player = playersList[i];
                bool added = false;

                switch (player.Position)
                {
                    case 1:
                        if (totalGoalkeepers < formation.NumberGoalkeepers)
                        {
                            playersSelection.Add(player);
                            totalGoalkeepers++;
                            playersList.RemoveAt(i);
                            added = true;
                        }
                        break;

                    case 2:
                        if (totalDefenders < formation.NumberDefenders)
                        {
                            playersSelection.Add(player);
                            totalDefenders++;
                            playersList.RemoveAt(i);
                            added = true;
                        }
                        break;

                    case 3:
                        if (totalAttackers < formation.NumberAttackers)
                        {
                            playersSelection.Add(player);
                            totalAttackers++;
                            playersList.RemoveAt(i);
                            added = true;
                        }
                        break;

                    case 0:
                        if (totalAttackers < formation.NumberAttackers)
                        {
                            player.Position = 3;
                            playersSelection.Add(player);
                            totalAttackers++;
                            playersList.RemoveAt(i);
                            added = true;
                        }
                        else if (totalDefenders < formation.NumberDefenders)
                        {
                            player.Position = 2;
                            playersSelection.Add(player);
                            totalDefenders++;
                            playersList.RemoveAt(i);
                            added = true;
                        }
                        else if (totalGoalkeepers < formation.NumberGoalkeepers)
                        {
                            player.Position = 1;
                            playersSelection.Add(player);
                            totalGoalkeepers++;
                            playersList.RemoveAt(i);
                            added = true;
                        }
                        break;

                    default:
                        throw new Exception("Invalid player position");
                }

                if (!added) { i++; }

                if (totalGoalkeepers == formation.NumberGoalkeepers &&
                totalDefenders == formation.NumberDefenders &&
                totalAttackers == formation.NumberAttackers)
                {
                    return playersSelection;
                }

            }

            throw new InvalidOperationException("Not enough valid players to form a team.");

        }
        else
        {
            return null;
        }
    }

    public static (bool valid, PlayerPosition errorIndex) Validate(List<Player> players, TeamFormation formation)
    {
        int totalPlayers = players.Count(x => x.Position == 0 || x.Position == 1 || x.Position == 2 || x.Position == 3);
        int totalAny = players.Count(x => x.Position == 0);
        int totalGoalkeepers = players.Count(x => x.Position == 1);
        int totalDefenders = players.Count(x => x.Position == 2);
        int totalAttackers = players.Count(x => x.Position == 3);

        if (totalPlayers < formation.MaxPlayers)
        { return (false, PlayerPosition.Any); }


        if (totalAttackers < formation.NumberAttackers)
        {
            var neededAny = formation.NumberAttackers - totalAttackers;

            totalAny -= neededAny;

            if (totalAny < 0) { return (false, PlayerPosition.Attackers); }
        }

        if (totalDefenders < formation.NumberDefenders)
        {
            var neededAny = formation.NumberDefenders - totalDefenders;

            totalAny -= neededAny;

            if (totalAny < 0) { return (false, PlayerPosition.Defender); }
        }

        if (totalGoalkeepers < formation.NumberGoalkeepers)
        {
            var neededAny = formation.NumberGoalkeepers - totalGoalkeepers;

            totalAny -= neededAny;

            if (totalAny < 0) { return (false, PlayerPosition.Goalkeepers); }
        }

        return (true, PlayerPosition.Any);
    }

}