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


    public static List<List<Player>> AllPossibleTeamsAny(
        List<Player> players,
        TeamFormation formation,
        bool shuffle = false)
    {

        List<List<Player>> teams = new();

        bool CanMake = true;
        while (CanMake)
        {
            var team = AllAnyPlayersSelection(players, formation, shuffle);
            if (team != null)
            {
                teams.Add(team);
                players.RemoveAll(x => team.Contains(x));
            }
            else
            {
                CanMake = false;
            }
        }

        return teams;
    }


    public static List<List<Player>> AllPossibleTeamsBasic(
        List<Player> players,
        TeamFormation formation,
        bool shuffle = false)
    {
        List<List<Player>> teams = new();

        bool CanMake = true;
        while (CanMake)
        {
            var team = BasicPlayersSelection(players, formation, shuffle);
            if (team != null)
            {
                teams.Add(team);
                players.RemoveAll(x => team.Contains(x));
            }
            else
            {
                CanMake = false;
            }
        }

        return teams;
    }

    public static List<Player>? AllAnyPlayersSelection(
        List<Player> players,
        TeamFormation formation,
        bool shuffle = false)
    {
        // Copy so we don’t mutate the original
        var copy = players.ToList();
        if (shuffle) copy.Shuffle();

        // Must have *at least* MaxPlayers
        if (copy.Count < formation.MaxPlayers)
            return null;

        // Safe to grab exactly MaxPlayers
        return copy.GetRange(0, formation.MaxPlayers);
    }

    public static List<Player>? BasicPlayersSelection(
        List<Player> players,
        TeamFormation formation,
        bool shuffle = false)
    {
        var copy = players.ToList();
        if (shuffle) copy.Shuffle();

        var selected = new List<Player>();
        int gk = 0, def = 0, atk = 0;

        foreach (var p in copy)
        {
            if (p.Position == 1 && gk  < formation.NumberGoalkeepers)
            {
                selected.Add(p);
                gk++;
            }
            else if (p.Position == 2 && def < formation.NumberDefenders)
            {
                selected.Add(p);
                def++;
            }
            else if (p.Position == 3 && atk < formation.NumberAttackers)
            {
                selected.Add(p);
                atk++;
            }
            else if (p.Position == 0) // “Any” slot
            {
                if      (atk < formation.NumberAttackers)
                {
                    selected.Add(p);
                    atk++;
                }
                else if (def < formation.NumberDefenders)
                {
                    selected.Add(p);
                    def++;
                }
                else if (gk < formation.NumberGoalkeepers)
                {
                    selected.Add(p);
                    gk++;
                }
                
            }

            if (gk == formation.NumberGoalkeepers
             && def == formation.NumberDefenders
             && atk == formation.NumberAttackers)
            {
                return selected;
            }
        }

        // If can't fill the selected
        return null;
    }
}

