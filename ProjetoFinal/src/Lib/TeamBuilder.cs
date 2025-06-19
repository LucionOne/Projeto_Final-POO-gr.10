using Models;
using Lib.TeamFormation;
using System.Xml.Schema;


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

        // Returns only the valid, post‑checked teams
    public static List<List<Player>> AllPossibleTeamsAny(
        List<Player> players,
        TeamFormation formation,
        bool shuffle = false)
    {

        












        if (players.Count < formation.MaxPlayers)
            return new List<List<Player>>();

        return players
            .Combinations(formation.MaxPlayers)
            .Select(combo => combo.ToList())
            .Select(team => AllAnyPlayersSelection(team, formation, shuffle))
            .Where(valid => valid is not null)
            .ToList()!;
    }

    public static List<List<Player>> AllPossibleTeamsBasic(
        List<Player> players,
        TeamFormation formation,
        bool shuffle = false)
    {
        if (players.Count < formation.MaxPlayers)
            return new List<List<Player>>();

        var result = new List<List<Player>>();

        foreach (var combo in players.Combinations(formation.MaxPlayers))
        {
            PlayerPosition error;
            var team = BasicPlayersSelection(combo.ToList(), formation, out error, shuffle);
            if (team != null)
                result.Add(team);
        }
        return result;
    }

    public static List<Player>? AllAnyPlayersSelection(List<Player> players, TeamFormation formation, bool Shuffle = false)
    {
        List<Player> playersList = players.ToList();

        if (Shuffle) playersList.Shuffle();

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

    public static List<Player>? BasicPlayersSelection(List<Player> players, TeamFormation formation, out PlayerPosition error, bool Shuffle = false)
    {
        List<Player> playersList = players.ToList();

        if (Shuffle) playersList.Shuffle();

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

public static class EnumerableExtensions
{
    // Generic n‑choose‑k combinations
    public static IEnumerable<IEnumerable<T>> Combinations<T>(
        this IEnumerable<T> source, int k)
    {
        if (k == 0)
        {
            yield return Enumerable.Empty<T>();
            yield break;
        }

        var index = 0;
        foreach (var item in source)
        {
            // take this item, then all (k-1)-combinations of the tail
            var tail = source.Skip(index + 1);
            foreach (var combo in tail.Combinations(k - 1))
                yield return new[] { item }.Concat(combo);
            index++;
        }
    }
}
