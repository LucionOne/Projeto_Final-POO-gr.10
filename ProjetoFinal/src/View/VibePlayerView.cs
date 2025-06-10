using Templates.view;
using Container.DTOs;
using VS;


namespace View;

public class VibePlayerView : IPlayerView
{
    VibeShell _vibe;


    public VibePlayerView(VibeShell vibe)
    {
        _vibe = vibe;
        _vibe.BetterChangeHeader(" P L A Y E R   M A N A G E M E N T", render: false);
        _vibe.ChangeInfBar([], false);
    }

    public int MainMenu(bool saved)
    {
        _vibe.clearInfBar(render: false);
        _vibe.BetterChangePageInfo(" M A I N   M E N U", render: false);
        List<string> options = new List<string>
        {
            "Create Player",
            "Edit Player",
            "Delete Player",
            "List Players",
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
    public PlayerDto? GetPlayerInput()
    {
        _vibe.BetterChangePageInfo(" C R E A T E   P L A Y E R", render: false);
        return null;
    }
    public PlayerDto? GetPlayerEdit(PlayerDto oldPlayer)
    {
        _vibe.BetterChangePageInfo(" E D I T   P L A Y E R", render: false);
        return null;
    }

    private VibeShell.SelectableItem PLayerToSelectableItem(PlayerDto player)
    {

        return new VibeShell.SelectableItem(
        player.Id,
        $"{player.Id.ToString().PadRight(4)}| {player.Name.PadRight(15)}| {player.PositionStringMini}",
        [$"ID: {player.Id.ToString().PadRight(4)}| Name: {player.Name}", $"Position: {player.PositionString}", $"Age: {player.Age.ToString().PadRight(3)}"]);
    }
    public int GetPlayerId(List<PlayerDto> players)
    {
        _vibe.BetterChangePageInfo(" S E L E C T   P L A Y E R", render: false);
        if (players.Count == 0)
        {
            _vibe.ChangeMainView(new List<string> { "No players available." }, render: true);
            return -1;
        }

        var items = players.Select(PLayerToSelectableItem).ToList();

        return _vibe.HandleSelectById(items, new List<string> { "ID  | Name" });
    }
    public bool ConfirmPlayerEdit(PlayerDto oldPlayer, PlayerDto newPlayer)
    {
        _vibe.BetterChangePageInfo(" C O N F I R M   E D I T", render: false);
        _vibe.ChangeMainView(new List<string>
        {
            "",
            "Old Player",
            "",
            $"Name: {oldPlayer.Name.PadRight(15)} |ID: {oldPlayer.Id}",
            $"Age: {oldPlayer.Age.ToString().PadRight(3)}| {oldPlayer.PositionString}"
        }, render: false);
        _vibe.ChangeSecView(new List<string>
        {
            "",
            "New Player",
            "",
            $"Name: {newPlayer.Name.PadRight(15)} |ID: {newPlayer.Id}",
            $"Age: {newPlayer.Age.ToString().PadRight(3)}| {newPlayer.PositionString}"
        }, render: true);

        (var X, var Y) = _vibe.GetInfBarPosition();

        bool confirmation = _vibe.HandleInputAt<bool>("  Save Changes? y/n: ", X, Y, 5, c => "yesnoYESNO".Contains(c));

        return confirmation;
    }
    public bool ConfirmPlayerDelete(PlayerDto player)
    {
        _vibe.BetterChangePageInfo(" C O N F I R M   D E L E T E", render: false);
        _vibe.ChangeMainView(new List<string>
        {
            "",
            "Delete Player?",
            "",
            $"Name: {player.Name.PadRight(15)} |ID: {player.Id}",
            $"Age: {player.Age.ToString().PadRight(3)}| {player.PositionString}"
        }, render: true);

        (var X, var Y) = _vibe.GetInfBarPosition();

        bool confirmation = _vibe.HandleInputAt<bool>("  Delete Player? y/n: ", X, Y, 5, c => "yesnoYESNO".Contains(c));

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
        if (choice == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ShowPlayers(List<PlayerDto> players)
    {
        _vibe.BetterChangePageInfo(" P L A Y E R   L I S T", render: false);

        if (players.Count == 0)
        {
            _vibe.clearSecView(render: false);
            _vibe.ChangeMainView(new List<string> { "", " Wow, such Empty ᓚᘏᗢ" }, render: false);
            var pos = _vibe.GetInfBarPosition();
            string line = "Press Any to Continue... ";
            _vibe.ChangeInfBar(["line"]);
            _vibe.Render();
            Console.SetCursorPosition(pos.X + line.Length, pos.Y);
            Console.ReadKey();
            return;
        }

        var items = players.Select(PLayerToSelectableItem).ToList();
        _vibe.HandleListItems(items, new List<string> { "ID  | Name" });
        return;
    }
    





}
