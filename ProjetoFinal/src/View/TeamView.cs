using Templates;
using Container.DTOs;
using System.Dynamic;
using System.ComponentModel.Design;

namespace View;

public class TeamView : ViewBasicFunctions
{
    public int MainMenu(bool saved)
    {
        string? strSaved;
        if (saved) { strSaved = ""; } else { strSaved = "*"; }
        var menulist = new List<string>
        {
            "=============================",
            "|         TEAM MENU         |",
            "+---------------------------+",
            "|01| Create Team            |",
            "|02| Edit Team              |",
            "|03| Delete Team            |",
            "|04| List Teams             |",
           $"|05| Save Changes{strSaved.PadRight(11)}|",
            "+---------------------------+",
            "|00| Exit                   |",
            "=============================",
        };
        var menu = string.Join("\n", menulist);

        var choice = GetChoice(
            prompt: ">> ",
            menu: menu,
            minimum: 0,
            maximum: 5,
            clear: true
        );
        return choice;
    }

    public TeamDto GetTeamInput()
    {

        string name = string.Empty;

        bool confirmation = false;

        while (!confirmation)
        {
            name = GetValidInput<string>(
                ">> ",
                "What's the name for the team?",
                true);

            confirmation = GetValidInput<bool>(
                prompt: ">> ",
                menu: $"Is ({name}) correct? y/n: ",
                clear: true);
        }

        var team = new TeamDto
        {
            Name = name
        };


        return team;
    }

    public int ShowAndGetTeamId(List<TeamDto> teams)
    {
        var menu = FormattedTeamsToString(teams);

        int input = -1;

        var valid = false;
        while (!valid)
        {
            input = GetValidInput<int>(
                "\nID: ",
                menu,
                true
            );

            valid = teams.Any(x => x.Id == input);

            if (!valid)
            {
                Console.WriteLine("ID doesn't exist, try again");
                Console.ReadLine();
            }
        }
        return input;
    }

    private string FormattedTeamsToString(List<TeamDto> teams)
    {
        var stringList = new List<string>
        {
            "========================================================",
            "|                  T E A M S   L I S T                 |",
            "+------------------------------------------------------+",
            "|ID   | Title               | Player Count  |   Date   |",
            "+-----+---------------------+---------------+----------+",
        };

        if (teams == null || teams.Count == 0)
        {
            stringList.Add("| No teams available.                                  |");
            return string.Join("\n", stringList);
        }

        foreach (var team in teams)
        {
            if (team == null)
            {
                stringList.Add($"| {"Null".PadRight(53)}|");
            }
            else
            {
                stringList.Add($"|{team.Id.ToString().PadRight(5)}| {team.Name.PadRight(20)}| {team.Players.Count.ToString().PadRight(14)}| {team.date.ToString("dd/MM/yy").PadRight(8)} |");
            }
        }

        stringList.AddRange([
            "+-----+---------------------+---------------+----------+",
            "|ID   | Title               | Player Count  |   Date   |",
            "+------------------------------------------------------+",
            "|                  T E A M S   L I S T                 |",
            "========================================================",
        ]);

        return string.Join("\n", stringList);
    }

    public void ShowTeams(List<TeamDto> teams)
    {
        Console.Clear();
        Console.WriteLine(FormattedTeamsToString(teams));
        Console.Write("\nPress any key to continue... ");
        Console.ReadKey();
    }

    public bool Bye(bool saved)
    {
        Console.Clear();

        if (!saved)
        {
            var confirmation = GetValidInput<bool>(
                ">> y/n: ",
                "Changes not Saved! Leave Anyway?",
                true
            );
            if (confirmation)
            { Console.Clear(); return false; }
            else { Console.Clear(); return true; }
        }
        else
        {
            return false;
        }
    }

    private string GetTeamString(TeamDto team)
    {
        if (team == null)
        {
            return "Team not found.";
        }


        var teamString = new List<string>
        {
            "========================================================",
            "|                  T E A M   D E T A I L S             |",
            "+------------------------------------------------------+",
            $"|ID   : {team.Id.ToString().PadRight(5)}|",
            $"|Name : {team.Name.PadRight(20)}|",
            $"|XP   : {team.XP.ToString().PadRight(14)}|",
            $"|Date : {team.date.ToString("dd/MM/yy").PadRight(8)} |",
            "+------------------------------------------------------+"
        };

        if (team.Players.Count > 0)
        {
            teamString.Add("| Players: ");
            foreach (var player in team.Players)
            {
                teamString.Add($"| - {player.Name}");
            }
        }
        else
        {
            teamString.Add("| No players in this team.");
        }

        teamString.Add("========================================================");

        return string.Join("\n", teamString);
    }
    public void ShowTeam(TeamDto team)
    {
        Console.Clear();
        Console.WriteLine(GetTeamString(team));
        Console.ReadKey();
    }


    public bool ConfirmDelete(TeamDto team)
    {
        Console.Clear();
        var confirmation = GetValidInput<bool>(
            ">> y/n: ",
            $"{GetTeamString(team)}\n\nAre you sure you want to delete the team ({team.Name})? This action cannot be undone.",
            true
        );
        return confirmation;
    }
}