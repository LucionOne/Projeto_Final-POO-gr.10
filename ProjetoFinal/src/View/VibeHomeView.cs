using System.Data;
using Templates;
using VS;

namespace View;


public class VibeHomeView : IHomeView
{
    VibeShell _vibe = new();

    public VibeHomeView(VibeShell vibe)
    {
        _vibe = vibe;
        _vibe.SetSize(80);
        var fillSize = _vibe.GetFillSize();
        string header = "H O M E   M E N U";
        var fill = (fillSize - header.Length) / 2;
        _vibe.ChangeHeader([$"{new string(' ', fill)}H O M E   M E N U"]);
        _vibe.ChangePageInfo([$"{new string('+', fillSize)}"]);
        _vibe.ChangeInfBar(["  >> "]);
    }

    public int MainMenu()
    {
        _vibe.SetSize(102);
        var fillSize = _vibe.GetFillSize();
        string header = "H O M E   M E N U";
        var fill = (fillSize - header.Length) / 2;
        _vibe.ChangeHeader([$"{new string(' ', fill)}H O M E   M E N U"]);
        _vibe.ChangePageInfo([$"{new string('+', fillSize)}"]);
        _vibe.ChangeInfBar(["  >> "]);
        List<string> options = new()
        {
            "Start Game",
            "Load Game",
            "Player Management",
            "Team Management",
            "Exit"
        };
        List<List<string>> descriptions = new()
        {
            new List<string> { "Start a new game." },
            new List<string> { "Load an existing game." },
            new List<string> { "Manage players in the game." },
            new List<string> { "Manage teams in the game." },
            new List<string> { "Exit the application." }
        };
        int choice = _vibe.HandleMenu(options, descriptions, 0) + 1;

        if (choice == 5) choice = 0;

        return choice;
    }

    public bool Bye()
    {
        (int X, int Y) = _vibe.GetInfBarPosition();
        Console.SetCursorPosition(X, Y);
        var question = "Exit? y/n: ";
        Console.WriteLine(question);
        bool confirm = _vibe.GetParsedInput<bool>(X + question.Length, Y, 2, c => char.IsLetterOrDigit(c) || c == ' ' || c == 'y' || c == 'n');

        (var RX, var RY) = _vibe.GetReadLinePosition();
        Console.SetCursorPosition(RX,RY);
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