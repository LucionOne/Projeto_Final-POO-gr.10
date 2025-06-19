using System.Data;
using System.Runtime.InteropServices;
using Templates;
using VS;

namespace View;


public class VibeHomeView : IHomeView
{
    VibeShell _vibe = new();

    public VibeHomeView(VibeShell vibe)
    {
        _vibe = vibe;
    }

    public int MainMenu()
    {
        _vibe.BetterChangeHeader(" G A M E S   M A N A G E M E N T   A P P",render: false);
        _vibe.BetterChangePageInfo(" H O M E   M E N U",render:false);
        _vibe.ChangeInfBar([]);

        List<string> options = new()
        {
            " Start Game",
            " Load Game",
            " Player Management",
            " Team Management",
            " Exit"
        };
        List<List<string>> descriptions = new()
        {
            new List<string> { "Start a new game." },
            new List<string> { "Load an existing game." },
            new List<string> { "Manage players in the game." },
            new List<string> { "Manage teams in the game." },
            new List<string> { "Exit the application." }
        };
        int choice = _vibe.HandleMenu(options, descriptions) + 1;

        if (choice == 5) choice = 0;

        return choice;
    }

    public bool Bye()
    {
        _vibe.BetterChangeHeader(" E X I T  ?");
        _vibe.ChangeInfBar([""]);
        var pos = _vibe.GetInfBarPosition();
        var question = "Exit? y/n: ";
        bool confirm = _vibe.HandleInputAt<bool>(question, pos.X, pos.Y, 3, c => "YESyesNOno".Contains(c));
        return confirm;
    }

    public void InvalidChoice<T>(T error)
    {
        _vibe.ChangeInfBar(["Invalid choice, please try again."]);
    }
    
    public VibeShell GetVibe()
    {
        return _vibe;
    }
    
}