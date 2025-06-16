using Templates.view;
using Container.DTOs;
using VS;
using System.Runtime.InteropServices;
using Templates;
using Models;
using Lib.TeamFormation;

namespace View;

public class VibeGameView : IGameView
{
    VibeShell _vibe;

    public VibeGameView(VibeShell vibe)
    {
        _vibe = vibe;
        _vibe.BetterChangeHeader(" G A M E   M A N A G E M E N T", render: false);
    }

    public int MainMenu(bool saved, GameDto game)
    {
        _vibe.clearInfBar(render: false);
        _vibe.BetterChangePageInfo(" M A I N   M E N U", render: false);
        _vibe.ChangeInfBar([], false);
        List<string> options = new List<string>
        {
            "Create Game",
            "Edit Game",
            "Delete Game",
            "List Games",
            saved ? "Save Changes" : "Save Changes *",
            "Exit"
        };

        int choice = _vibe.HandleMenu(options, renderEachChange: true) + 1;

        if (choice == 6) choice = 0;

        return choice;
    }

    public bool Bye(bool saved)
    {
        _vibe.BetterChangePageInfo(" G O O D B Y E", render: false);

        if (saved)
        { return false; }
        else
        {
            var options = new List<string>
            {
                "Yes, exit without saving",
                "No, go back to the menu",
            };

            bool confirmExit = _vibe.HandleMenu(options, renderEachChange: true) == 0;

            if (confirmExit)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

    }

    public GameDto? GetGameInput()
    {
        _vibe.Clear(render: false);
        _vibe.BetterChangePageInfo(" G A M E   I N P U T", render: false);
        _vibe.ChangeInfBar([], false);
        var form = new List<string>
        {
            "Title: ",
            "Data: ",
            "Horário: ",
            "Local: ",
            "Tipo de Campo: ",
            "Players: ",
            "Formation: "
        };

        _vibe.ChangeMainView(form, render: true);

        var pos = _vibe.GetMainViewPosition();

        // _vibe.ChangeSecView(["What's the Title?"]);
        string title = _vibe.HandleInputAt<string>(form[0], pos.X, pos.Y, 15);
        // _vibe.ChangeSecView(["What's the Date?","Write as (yyyy-MM-dd)"]);
        DateOnly date = _vibe.HandleInputAt<DateOnly>("Data (yyyy-MM-dd): ", pos.X, pos.Y + 1, 10, c => "1234567890-".Contains(c));
        // _vibe.ChangeSecView(["What's the Time?","Write as (HH:mm)"]);
        TimeOnly time = _vibe.HandleInputAt<TimeOnly>("Horário (HH:mm): ", pos.X, pos.Y + 2, 5, c => "1234567890:".Contains(c));
        // _vibe.ChangeSecView(["Where's the Game?"]);
        string local = _vibe.HandleInputAt<string>(form[3], pos.X, pos.Y + 3, 20);
        // _vibe.ChangeSecView(["How it the Camp?"]);
        string tipoDeCampo = _vibe.HandleInputAt<string>(form[4], pos.X, pos.Y + 4, 20);
        // _vibe.ChangeSecView(["How Many Players Per Team?"]);
        int maxPlayers = _vibe.HandleInputAt<int>(form[5], pos.X, pos.Y + 5, 2, c => char.IsDigit(c));

        // _vibe.ChangeSecView(["Do you want to use a formation?"]);
        bool useFormation = _vibe.HandleInputAt<bool>("Use a Formation? y/n: ", pos.X, pos.Y + 6, 3, c => "YESnoNOyes".Contains(c));

        TeamFormation formation = new();

        if (useFormation)
        {
            bool isValid = false;
            while (!isValid)
            {
                // _vibe.ChangeSecView(["How Many GoalKeepers Per Team?"]);
                int numberGoalkeepers = _vibe.HandleInputAt<int>("GoalKeepers: ", pos.X, pos.Y + 7, 2, c => char.IsDigit(c), clear: true);
                // _vibe.ChangeSecView(["How Many Attackers Per Team?"]);
                int numberAttackers = _vibe.HandleInputAt<int>("Attackers: ", pos.X, pos.Y + 7, 2, c => char.IsDigit(c), clear: true);
                // _vibe.ChangeSecView(["How Many Defenders Per Team?"]);
                int numberDefenders = _vibe.HandleInputAt<int>("Defenders: ", pos.X, pos.Y + 7, 2, c => char.IsDigit(c), clear: true);
                formation = new(maxPlayers: maxPlayers, numberGoalkeepers: numberGoalkeepers, numberDefenders: numberDefenders, numberAttackers: numberAttackers);
                formation.UsingFormation = useFormation;
                isValid = TeamFormation.isValid(formation);
                if (!isValid)
                {
                    _vibe.ChangeSecView([
                        "The sum of the players",
                        "need to be equal to the",
                        "amount of players in",
                        $"the team - {maxPlayers}"]);
                    _vibe.ChangeInfBar(["Press any key to Continue... "]);
                    _vibe.WaitForClick();
                    _vibe.clearSecView();
                    _vibe.clearInfBar();
                }
            }
        }
        else
        {
            formation = new(maxPlayers, useFormation);
        }

        return new GameDto
        {
            Title = title,
            Date = date,
            HoraInicio = time,
            Local = local,
            TipoDeCampo = tipoDeCampo,
            TeamFormation = formation
        };
    }

    public GameDto? GetGameEdit(GameDto oldPlayer)
    {
        return null;
    }

    private VibeShell.SelectableItem GameToSelectableItem(GameDto game)
    {
        return null;
    }

    public int GetGameId(List<GameDto> games)
    {
        return -1;
    }

    public bool ConfirmGameEdit(GameDto oldGame, GameDto newGame)
    {
        return false;
    }

    public bool ConfirmGameDelete(GameDto player)
    {
        return false;
    }

    public bool ConfirmGameAdd(GameDto player)
    {
        return false;
    }

    public bool ConfirmSaveToDatabase(bool saved)
    {
        _vibe.BetterChangePageInfo(" C O N F I R M   S A V E", render: false);
        var options = new List<string>
        {
            "Save to the DataBase",
            "Don't Save to the DataBase",
        };

        int choice = _vibe.HandleMenu(options);

        return choice == 0;
    }

    public void ShowGames(List<GameDto> players) { }

    public int GetTeamMaker()
    {
        return -1;
    }

}
