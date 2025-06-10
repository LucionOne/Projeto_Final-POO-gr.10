using Templates;
using Templates.view;
using Container.DTOs;
using System.Dynamic;
using System.ComponentModel.Design;
using VS;
using System.Xml.Serialization;
using System.Reflection.Metadata.Ecma335;
using Models;
using System.Data.Common;



namespace View;


public class VibeTeamView : ITeamView
{
    public VibeShell _vibe;

    public VibeTeamView(VibeShell vibe)
    {
        _vibe = vibe;

        List<string> header = new()
        {
            "Team Management",
        };

        List<string> pageInfo = new()
        {
            "Manage your teams efficiently",
        };

        List<string> mainView = new()
        {
            "01. Create Team",
            "02. Edit Team",
            "03. Delete Team",
            "04. List Teams",
            "05. Save Changes",
            "00. Exit",
        };
        List<string> infBar = new()
        { };

        _vibe.ChangeHeader(header, false);
        _vibe.ChangePageInfo(pageInfo, false);
        _vibe.ChangeMainView(mainView, false);
        _vibe.ChangeInfBar(infBar, false);

    }

    public int MainMenu(bool saved)
    {
        string savedStr;
        string savedStrDescription;
        if (saved)
        {
            savedStrDescription = "Saved";
            savedStr = "";
        }
        else
        {
            savedStrDescription = "Unsaved Changes";
            savedStr = "*";
        }

        List<string> options = new()
        {
            "01. Create Team",
            "02. Edit Team",
            "03. Delete Team",
            "04. List Teams",
           $"05. Save Changes{savedStr}",
            "00. Exit",
        };

        List<string>? createTeamDescription = new()
        {
            "Create a new team with a unique name.",
        };
        List<string>? editTeamDescription = new()
        {
            "Edit an existing team by ",
            "selecting it from the list."
        };
        List<string>? deleteTeamDescription = new()
        {
            "Delete an existing team ",
            "by selecting it from the list."
        };
        List<string>? listTeamsDescription = new()
        {
            "List all existing teams.",
        };
        List<string>? saveChangesDescription = new()
        {
            $"Save changes to the teams.",
            $"Current status: {savedStrDescription}"
        };
        List<string>? exitDescription = new()
        {
            "Exit the team management menu.",
        };

        List<List<string>>? descriptionsList = new()
        {
            createTeamDescription,
            editTeamDescription,
            deleteTeamDescription,
            listTeamsDescription,
            saveChangesDescription,
            exitDescription,
        };

        var choice = _vibe.HandleMenu(
            options: options,
            descriptions: descriptionsList,
            defaultIndex: 0,
            menuScale: _vibe.Scale,
            renderEachChange: true
        ) + 1 ;

        if (choice == 6) // If the user selected "Exit"
        {
            choice = 0; // Default to exit option
        }
        if (choice == -1)
        {
            choice = 0; // Default to first option if invalid
        }

        return choice;
    }

    public bool Bye(bool saved)
    {
        if (saved)
        {
            _vibe.Clear(true, true);
            return false; // Exit the menu
        }
        else
        {
            List<string> options = new()
            {
                "Yes, exit without saving",
                "No, go back to the menu",
            };

            _vibe.clearSecView(render: false);

            int choice = _vibe.HandleMenu(
                options: options,
                defaultIndex: 0,
                menuScale: _vibe.Scale,
                renderEachChange: true
            );

            if (choice == 0)
            {
                _vibe.Clear(true, true);
                return false; // Exit the menu
            }
            else
            {
                _vibe.Clear(false, true);
                return true; // Go back to the menu
            }
        }
    }



public TeamDto GetTeamInput(List<PlayerDto> players)
{
    // … your existing name/date logic …

    // 1) Clear out the views
    _vibe.Clear(render: false);
    _vibe.ChangePageInfo(new List<string> { "Making a Team" }, false);
    _vibe.ChangeSecView(new List<string> { "" }, false);

    // 2) Ask for team name & date (unchanged)
    var questionName  = "  Team name: ";
    var questionDate  = "  Date of Creation (yyyy-MM-dd): ";
    var questionDate2 = $"  Use Actual Date({DateOnly.FromDateTime(DateTime.Now)})? y/n: ";

    _vibe.ChangeMainView(new List<string> { "", questionName, "", questionDate });

    var (X, Y) = _vibe.GetMainViewPosition();
    string  name    = _vibe.HandleInputAt<string>(questionName, X, Y + 1, 20);
    bool    confirm = _vibe.HandleInputAt<bool>(questionDate2, X, Y + 3, 2, c => "yesnoYESNO".Contains(c), clear: true);

    DateOnly date = confirm
        ? DateOnly.FromDateTime(DateTime.Now)
        : _vibe.HandleInputAt<DateOnly>(questionDate, X, Y + 3, 10, c => "0123456789-".Contains(c));

    // 3) Now build your selectable items from the PlayerDto list
    var selectablePlayers = players.Select(p =>
        new VibeShell.SelectableItem(
            id: p.Id,
            label: $"{p.Id.ToString().PadRight(5)}| {p.Name.PadRight(20)}| {p.Age.ToString().PadRight(3)}| {p.PositionStringMini}",
            description: new List<string>
            {
                $"Name:     {p.Name}",
                $"Age:      {p.Age}",
                $"Position: {p.PositionStringMini}"
            }
        )
    ).ToList();

    // 4) Clear the main view and let the user pick multiple player IDs
    _vibe.clearMainView(true);

    // Optional header lines for the list
    var header = new List<string>
    {
        "==================================================",
        "|             P L A Y E R   L I S T              |",
        "+-----+---------------------+----+---------------+",
        "| ID  | Name                | yo | Position      |",
        "+-----+---------------------+----+---------------+"
    };

    List<int> ids = _vibe.HandleMultiIds(
        choices:    selectablePlayers,
        headerLines: header,
        exitCode:   "XX",
        prompt:     "Enter Player ID (XX to finish):"
    );

    return new TeamDto(name, date, ids);
}


    public bool ConfirmSaveToDB()
    {
        _vibe.clearMainView(false);
        _vibe.clearSecView(false);
        _vibe.ChangePageInfo(["S A V E   T O   D A T A B A S E"], false);
        _vibe.ChangeInfBar(["  >>"], false);
        var options = new List<string>
        {
            "Save to the DataBase",
            "Don't Save to the DataBase"
        };
        int choice = _vibe.HandleMenu(options);
        if (choice == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private List<List<string>> MakeDescriptions(List<TeamDto> teams)
    {
        var descriptions = new List<List<string>>();

        foreach (var team in teams)
        {
            var playerDescr = new List<string> { "PLayers" };
            var eventDescr = new List<string> { "Events" };

            int id = team.Id;
            string name = team.Name;
            List<PlayerDto> players = team.Players;
            List<Event> events = team.EventsHistory;
            int XP = team.XP;
            DateOnly date = team.Date;

            foreach (var Event in events)
            {
                eventDescr.Add($"{Event.ToString()}");
            }
            foreach (var player in players)
            {
                playerDescr.Add($"{player.Id.ToString().PadRight(3)}| {player.Name.PadRight(15)}| {player.PositionStringMini}");
            }
            var descr = new List<string>
            {
                $"Id: {id.ToString().PadRight(5)}| Name: {name}",
                $"XP: {XP.ToString().PadRight(5)}| Date: {date.ToString("dd/MM/yy")}",
                $""
            };

            descr.AddRange(playerDescr);
            descr.Add("");
            descr.AddRange(eventDescr);

            descriptions.Add(descr);
        }
        return descriptions;
    }

    private List<string> MakeTeamHeads(List<TeamDto> teams)
    {
        var heads = new List<string>();

        foreach (var team in teams)
        {
            heads.Add($"| {team.Name.PadRight(20)}| {team.Id.ToString().PadRight(5)}");
        }
        return heads;
    }


    public void ShowTeams(List<TeamDto> teams)
    {
        var descriptions = MakeDescriptions(teams);
        var heads = MakeTeamHeads(teams);

        var header = new List<string>
        {
            "ID   | Names"
        };


        var items = TeamsIntoSelectableItems(teams);


        _vibe.HandleListItems(items, header);

        return;
    }

    private List<VibeShell.SelectableItem> TeamsIntoSelectableItems(List<TeamDto> teams)
    {
        var items = new List<VibeShell.SelectableItem>();

        foreach (var team in teams)
        {

            var playerDescr = new List<string> { "PLayers" };
            var eventDescr = new List<string> { "Events" };

            int id = team.Id;
            string name = team.Name;
            List<PlayerDto> players = team.Players;
            List<Event> events = team.EventsHistory;
            int XP = team.XP;
            DateOnly date = team.Date;

            foreach (var Event in events)
            {
                eventDescr.Add($"{Event.ToString()}");
            }
            foreach (var player in players)
            {
                playerDescr.Add($"{player.Id.ToString().PadRight(3)}| {player.Name.PadRight(15)}| {player.PositionStringMini}");
            }
            var description = new List<string>
            {
                $"Id: {id.ToString().PadRight(5)}| Name: {name}",
                $"XP: {XP.ToString().PadRight(5)}| Date: {date.ToString("dd/MM/yy")}",
                $""
            };

            description.AddRange(playerDescr);
            description.Add("");
            description.AddRange(eventDescr);

            string Label = $"{team.Id.ToString().PadRight(5)}| {team.Name.PadRight(20)}";


            var item = new VibeShell.SelectableItem(id, Label, description);
            items.Add(item);
        }


        return items;

    }


    public int GetTeamId(List<TeamDto> teams)
    {
        var items = TeamsIntoSelectableItems(teams);

        var header = new List<string>
        {
            "ID   | Names"
        };

        int id = _vibe.HandleSelectById(items,header);

        return id;
    }

    public bool ConfirmDeleteTeam(TeamDto team)
    {
        _vibe.clearMainView(false);
        _vibe.clearSecView(false);
        _vibe.ChangePageInfo(["D E L E T E   T E A M"], false);
        _vibe.ChangeInfBar(["  >>"], false);

        var options = new List<string>
        {
            $"Delete Team: {team.Name}",
            "Don't Delete Team"
        };

        int choice = _vibe.HandleMenu(options);

        if (choice == 0)
        {
            return true; // Confirm deletion
        }
        else
        {
            return false; // Cancel deletion
        }
    }
public TeamDto GetTeamEdit(List<PlayerDto> players, TeamDto team)
{
    _vibe.Clear(render: false);
    _vibe.ChangePageInfo(["E D I T   T E A M"], false);
    _vibe.ChangeSecView([""], false);

    var questionName  = "  Team name: ";
    var questionDate  = "  Date of Creation (yyyy-MM-dd): ";
    var questionDate2 = $"  Use Actual Date({DateOnly.FromDateTime(DateTime.Now)})? y/n: ";

    _vibe.ChangeMainView(["", questionName, "", questionDate]);

    var (X, Y) = _vibe.GetMainViewPosition();

    string name = team.Name;
    DateOnly date = team.Date;

    if (team.IdList.Count > 0)
    {
        _vibe.ChangeMainView(
            new List<string>
            {
                questionName + name,
                "",
                questionDate + date.ToString("yyyy-MM-dd")
            },
            render: false
        );
    }

    name = _vibe.HandleInputAt<string>(questionName, X, Y + 1, 20);

    Console.SetCursorPosition(X, Y + 3);
    Console.Write(new string(' ', questionDate.Length));

    bool confirm = _vibe.HandleInputAt<bool>(
        questionDate2, X, Y + 3, 2, x => "yesnoYESNO".Contains(x), clear: true
    );

    if (confirm)
        date = DateOnly.FromDateTime(DateTime.Now);
    else
        date = _vibe.HandleInputAt<DateOnly>(
            questionDate, X, Y + 3, 10, c => "0123456789-".Contains(c)
        );

    _vibe.clearMainView(true);

    // 🛠 Create SelectableItems from players
    var selectablePlayers = players.Select(p =>
        new VibeShell.SelectableItem(
            id: p.Id,
            label: $"{p.Id.ToString().PadRight(5)}| {p.Name.PadRight(20)}| {p.Age.ToString().PadRight(3)}| {p.PositionStringMini}",
            description: new List<string>
            {
                $"Name:     {p.Name}",
                $"Age:      {p.Age}",
                $"Position: {p.PositionStringMini}"
            }
        )
    ).ToList();

    var header = new List<string>
    {
        "===================================================",
        "|              P L A Y E R   L I S T              |",
        "+----+---------------------+----+-----------------+",
        "| ID | Name                | yo | Position        |",
        "+----+---------------------+----+-----------------+"
    };

    List<int> ids = _vibe.HandleMultiIds(
        choices: selectablePlayers,
        headerLines: header,
        exitCode: "XX",
        prompt: "Enter Player ID (XX to finish):"
    );

    return new TeamDto(name, date, ids);
}


}