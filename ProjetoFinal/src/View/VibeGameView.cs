using Templates.view;
using Container.DTOs;
using VS;
using System.Runtime.InteropServices;
using Templates;
using Models;
using Lib.TeamFormation;
using Controller;

namespace View;

public class VibeGameView : IGameView
{
    VibeShell _vibe;

    public VibeGameView(VibeShell vibe)
    {
        _vibe = vibe;
        _vibe.BetterChangeHeader(" G A M E   M A N A G E M E N T", render: false);
    }

    public GameController.GameChoices MainMenu(bool saved, GameDto game)
    {
        _vibe.clearInfBar(render: false);
        _vibe.BetterChangePageInfo(" M A I N   M E N U", render: false);
        _vibe.ChangeInfBar([], false);


        string nextTeamName = game.PeekNextTeam()?.Name ?? "No more teams left";

        var gameStatus = new List<string>
        {
            $"Title: {game.Title}",
            $"Date: {game.Date.ToString("dd/MM/yy").PadRight(9)}| Time: {game.HoraInicio.ToString("HH:mm")}",
            $"Location: {game.Local}",
            $"Field Type: {game.TipoDeCampo}",
            "",
            $"Home: {game.HomeTeam.Name}",
            $"Guest: {game.GuestTeam.Name}",
            "",
            $"Next Team: {nextTeamName}",
            $"Teams LineUp: {game.TeamsToPlay.Count}"
        };

        List<string> options = new List<string>
        {
            "Edit Game",
            "Add Players",
            "Teams LineUp",
            "End Match",
            "Delete Game",
            "List Games",
            saved ? "Save Changes" : "Save Changes *",
            "Exit"
        };

        var description = Enumerable.Repeat(gameStatus, options.Count).ToList();


        int choice = _vibe.HandleMenu(options, description) + 1;

        if (choice == options.Count) choice = 0;

        switch (choice)
        {
            case 0:
                return GameController.GameChoices.Exit;
            case 1:
                return GameController.GameChoices.EditGame;
            case 2:
                return GameController.GameChoices.AddPlayers;
            case 3:
                return GameController.GameChoices.AddTeam;
            case 4:
                return GameController.GameChoices.EndMatch;
            case 5:
                return GameController.GameChoices.DeleteGame;
            case 6:
                return GameController.GameChoices.ListGames;
            case 7:
                return GameController.GameChoices.Save;
            default:
                return GameController.GameChoices.FallBack;
        }
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

    public GameDto? GetGameEdit(GameDto oldGame)
    {
        _vibe.Clear(render: false);
        _vibe.BetterChangePageInfo(" E D I T   G A M E", render: false);
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

        // Show old game info in secondary view
        _vibe.ChangeSecView(new List<string>
        {
            "Old Game:",
            $"Title({oldGame.Title}): ",
            $"Date({oldGame.Date:yyyy-MM-dd}): ",
            $"Time({oldGame.HoraInicio:HH:mm}): ",
            $"Local({oldGame.Local}): ",
            $"Tipo de Campo({oldGame.TipoDeCampo}): ",
            $"Players({oldGame.TeamFormation.MaxPlayers}): ",
            $"Formation({(oldGame.TeamFormation.UsingFormation ? "Yes" : "No")}): "
        });

        _vibe.ChangeMainView(form, render: true);

        var pos = _vibe.GetMainViewPosition();

        string title = _vibe.HandleInputAt<string>(form[0], pos.X, pos.Y, 15);
        DateOnly date = _vibe.HandleInputAt<DateOnly>("Data (yyyy-MM-dd): ", pos.X, pos.Y + 1, 10, c => "1234567890-".Contains(c));
        TimeOnly time = _vibe.HandleInputAt<TimeOnly>("Horário (HH:mm): ", pos.X, pos.Y + 2, 5, c => "1234567890:".Contains(c));
        string local = _vibe.HandleInputAt<string>(form[3], pos.X, pos.Y + 3, 20);
        string tipoDeCampo = _vibe.HandleInputAt<string>(form[4], pos.X, pos.Y + 4, 20);
        int maxPlayers = _vibe.HandleInputAt<int>(form[5], pos.X, pos.Y + 5, 2, c => char.IsDigit(c));

        bool useFormation = _vibe.HandleInputAt<bool>("Use a Formation? y/n: ", pos.X, pos.Y + 6, 3, c => "YESnoNOyes".Contains(c));

        TeamFormation formation = new();

        if (useFormation)
        {
            bool isValid = false;
            while (!isValid)
            {
            int numberGoalkeepers = _vibe.HandleInputAt<int>("GoalKeepers: ", pos.X, pos.Y + 7, 2, c => char.IsDigit(c), clear: true);
            int numberAttackers = _vibe.HandleInputAt<int>("Attackers: ", pos.X, pos.Y + 7, 2, c => char.IsDigit(c), clear: true);
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
            Id = oldGame.Id,
            Title = title,
            Date = date,
            HoraInicio = time,
            Local = local,
            TipoDeCampo = tipoDeCampo,
            TeamFormation = formation,
            TeamsToPlay = oldGame.TeamsToPlay // preserve teams
        };
    }

    public int GetGameId(List<GameDto> games)
    {
        _vibe.Clear(render: false);
        _vibe.BetterChangePageInfo(" G A M E   S E L E C T I O N", render: false);

        List<VibeShell.SelectableItem> gamesAsItems = games.Select(GameToSelectableItem).ToList();

        int id = _vibe.HandleSelectById(gamesAsItems, ["ID  | Name      | Date"]);

        return id;
    }

    public bool ConfirmGameEdit(GameDto oldGame, GameDto newGame)
    {
        _vibe.Clear();
        _vibe.BetterChangePageInfo(" C O N F I R M   E D I T", render: false);
        _vibe.ChangeMainView(new List<string>
        {
            "",
            "Old Game",
            "",
            $"ID: {oldGame.Id.ToString().PadRight(4)}| Title: {oldGame.Title}",
            $"Teams: {oldGame.TeamsToPlay.Count.ToString().PadRight(3)}| Local: {oldGame.Local}",
            $"Age: {oldGame.TipoDeCampo.PadRight(10)}| {oldGame.Date.ToString("dd/MM/yy")}",
        }, render: false);
        _vibe.ChangeSecView(new List<string>
        {
            "",
            "New Game",
            "",
            $"ID: {newGame.Id.ToString().PadRight(4)}| Title: {newGame.Title}",
            $"Teams: {newGame.TeamsToPlay.Count.ToString().PadRight(3)}| Local: {newGame.Local}",
            $"Age: {newGame.TipoDeCampo.PadRight(10)}| {newGame.Date.ToString("dd/MM/yy")}",
        }, render: false);

        _vibe.ChangeInfBar([""], render: true);

        var pos = _vibe.GetInfBarPosition();

        bool confirmation = _vibe.HandleInputAt<bool>(" Save Changes? y/n: ", pos.X, pos.Y, 3, c => "yesnoYESNO".Contains(c));

        return confirmation;
    }

    public bool ConfirmGameDelete(GameDto player)
    {
        _vibe.Clear(render:false);
        _vibe.BetterChangePageInfo(" C O N F I R M   D E L E T E", render: false);
        _vibe.ChangeMainView(new List<string>
        {
            "",
            "Game to Delete",
            "",
            $"ID: {player.Id.ToString().PadRight(4)}| Title: {player.Title}",
            $"Teams: {player.TeamsToPlay.Count.ToString().PadRight(3)}| Local: {player.Local}",
            $"Tipo de Campo: {player.TipoDeCampo.PadRight(10)}| {player.Date.ToString("dd/MM/yy")}",
        }, render: false);

        _vibe.ChangeInfBar([""], render: true);

        var pos = _vibe.GetInfBarPosition();

        bool confirmation = _vibe.HandleInputAt<bool>(" Confirm Deletion? y/n: ", pos.X, pos.Y, 3, c => "yesnoYESNO".Contains(c));

        return confirmation;
        
    }

    public bool ConfirmGameAdd(GameDto player)
    {
        _vibe.Clear(render: false);
        _vibe.BetterChangePageInfo(" C O N F I R M   A D D", render: false);
        _vibe.ChangeMainView(new List<string>
        {
            "",
            "  Add Game?",
            "",
            $" ID: {player.Id.ToString().PadRight(4)}| Title: {player.Title}",
            $" Teams: {player.TeamsToPlay.Count.ToString().PadRight(3)}| Local: {player.Local}",
            $" Age: {player.TipoDeCampo.PadRight(10)}| {player.Date.ToString("dd/MM/yy")}",
        }, render: false);

        _vibe.ChangeInfBar([""], render: true);

        var pos = _vibe.GetInfBarPosition();

        bool confirmation = _vibe.HandleInputAt<bool>("  Add Game? y/n: ", pos.X, pos.Y, 5, c => "yesnoYESNO".Contains(c));

        return confirmation;
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

    public void ShowGames(List<GameDto> games)
    {
        _vibe.Clear(render: false);
        _vibe.BetterChangePageInfo(" L I S T I N G   G A M E S", render: false);

        List<VibeShell.SelectableItem> gamesAsItems = games.Select(GameToSelectableItem).ToList();

        _vibe.HandleListItems(gamesAsItems, ["ID  | Name      | Date"]);
    }

    public int TeamMakingMethod()
    {
        _vibe.Clear(render: false);
        _vibe.BetterChangePageInfo(" How do you want to distribute the players?");
        List<string> options = new List<string>
        {
            "Basic",
            "Any",
            "Pick Teams",
            "Cancel"
        };

        int choice = _vibe.HandleMenu(options) +1;

        if (choice == 4) choice = -1;

        return choice;
    }

    public List<int> GetPlayers(List<PlayerDto> players)
    {
        _vibe.Clear(render: false);
        _vibe.BetterChangePageInfo(" S E L E C T   P L A Y E R S", render: false);

        List<VibeShell.SelectableItem> playerItems = players.Select(p =>
            new VibeShell.SelectableItem(
                p.Id,
                $"{p.Id.ToString().PadRight(4)}| {p.Name.PadRight(15)}| {p.Position}",
                new List<string>
                {
                    $"Name: {p.Name}",
                    $"Position: {p.Position}",
                    $"Age: {p.Age}"
                }
            )
        ).ToList();

        List<int> selectedIds = _vibe.HandleMultiSelectIds(playerItems, ["ID  | Name           | Position"]);

        return selectedIds;
    }

    public GameController.Sides GetWhoWon(GameDto game)
    {
        _vibe.Clear(render: false);
        _vibe.BetterChangePageInfo(" Who Won?", render: false);

        List<string> options = new List<string>
        {
            $"Home Team: {game.HomeTeam.Name}",
            $"Guest Team: {game.GuestTeam.Name}",
            "Cancel"
        };

        int choice = _vibe.HandleMenu(options);

        switch (choice)
        {
            case 0:
                return GameController.Sides.Home;
            case 1:
                return GameController.Sides.Guest;
            case 2:
                return GameController.Sides.Cancel;
            default:
                return GameController.Sides.Cancel;
        }
    }

    public List<int> GetTeams(List<TeamDto>teams, GameDto? game)
    {
        _vibe.Clear(render: false);
        _vibe.BetterChangePageInfo(" S E L E C T   T E A M S", render: false);

        List<VibeShell.SelectableItem> teamItems = teams.Select(t =>
            new VibeShell.SelectableItem(
                t.Id,
                $"{t.Id.ToString().PadRight(4)}| {t.Name.PadRight(15)}| {t.Players.Count} Players",
                new List<string>
                {
                    $"Name: {t.Name}",
                    $"Players: {t.Players.Count}",
                    $"Date: {t.Date:yyyy-MM-dd}",
                }
            )
        ).ToList();

        if (game != null)
        {
            teamItems.Add(new VibeShell.SelectableItem(
                -1,
                "Cancel",
                ["Cancel Team Selection"]
            ));
        }

        List<int> selectedIds = _vibe.HandleMultiSelectIds(teamItems, ["ID  | Name           | Players"]);

        return selectedIds;
    }

    public List<TeamDto> FastTeamBuilder(List<List<PlayerDto>> teamsSelections)
    {
        _vibe.Clear(render: false);
        _vibe.BetterChangePageInfo(" F A S T   T E A M   B U I L D E R", render: false);

        List<TeamDto> teams = new List<TeamDto>();

        for (int i = 0; i < teamsSelections.Count; i++)
        {
            var teamPlayers = teamsSelections[i];
            if (teamPlayers.Count == 0) continue;

            List<string> form = new List<string>
            {
                $"Team Name: ",
            };


            _vibe.ChangeMainView(form, render: true);

            var pos = _vibe.GetMainViewPosition();

            string teamName = _vibe.HandleInputAt<string>(form[0], pos.X, pos.Y, 20);


            TeamDto team = new TeamDto
            {
                Id = -2,
                Name = teamName,
                Players = teamPlayers,
                Date = DateOnly.FromDateTime(DateTime.Now),
            };

            teams.Add(team);
        }

        return teams;
    }

    private VibeShell.SelectableItem GameToSelectableItem(GameDto game)
    {
        return new VibeShell.SelectableItem
        (
            game.Id,
            $"{game.Id.ToString().PadRight(4)}| {game.Title.PadRight(10)}| {game.Date.ToString("dd/MM/yy")}",
            new List<string>
            {
                $"Date: {game.Date:yyyy-MM-dd}",
                $"Time: {game.HoraInicio:HH:mm}",
                $"Location: {game.Local}",
                $"Field Type: {game.TipoDeCampo}",
                $"Teams: {game.TeamsToPlay.Count}"
            }
        );
    }
    
}
