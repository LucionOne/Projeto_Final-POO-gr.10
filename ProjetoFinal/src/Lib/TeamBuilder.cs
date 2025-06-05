using team;
using Lib.TeamFormation;
using jogador;
using System.Xml.Schema;

namespace Lit.TeamBuilder;

public class TeamBuilder
{

    public enum PlayerPosition
    {
        Any,
        Goalkeepers,
        Defender,
        Attackers,
        Corrupted
    }

    public static Team? BuildAllAny(List<Player> players, TeamFormation formation)
    {
        bool valid = players.Count <= formation.MaxPlayers;
        if (valid)
        {
            var team = new Team();

            for (int i = 0; i < formation.MaxPlayers; i++)
            {
                var player = players[i];
                team.AddJogador(player);
                players.RemoveAt(i);
            }
            return team;
        }
        return null;
    }

    public static Team? BuildBasic(List<Player> players, TeamFormation formation, out PlayerPosition error)
    {

        (var valid, error) = Validate(players, formation);

        if (valid)
        {
            int totalGoalkeepers = 0, totalDefenders = 0, totalAttackers = 0;
            var team = new Team();

            for (int i = 0; i < players.Count;)
            {

                var player = players[i];
                bool added = false;

                switch (player.Position)
                {
                    case 1:
                        if (totalGoalkeepers < formation.NumberGoalkeepers)
                        {
                            team.AddJogador(player);
                            totalGoalkeepers++;
                            players.RemoveAt(i);
                            added = true;
                        }
                        break;

                    case 2:
                        if (totalDefenders < formation.NumberDefenders)
                        {
                            team.AddJogador(player);
                            totalDefenders++;
                            players.RemoveAt(i);
                            added = true;
                        }
                        break;

                    case 3:
                        if (totalAttackers < formation.NumberAttackers)
                        {
                            team.AddJogador(player);
                            totalAttackers++;
                            players.RemoveAt(i);
                            added = true;
                        }
                        break;

                    case 0:
                        if (totalAttackers < formation.NumberAttackers)
                        {
                            player.Position = 3;
                            team.AddJogador(player);
                            totalAttackers++;
                            players.RemoveAt(i);
                            added = true;
                        }
                        else if (totalDefenders < formation.NumberDefenders)
                        {
                            player.Position = 2;
                            team.AddJogador(player);
                            totalDefenders++;
                            players.RemoveAt(i);
                            added = true;
                        }
                        else if (totalGoalkeepers < formation.NumberGoalkeepers)
                        {
                            player.Position = 1;
                            team.AddJogador(player);
                            totalGoalkeepers++;
                            players.RemoveAt(i);
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
                    return team;
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