using View;
using Templates;
using MyRepository;
using jogo;

namespace Controller;

public class HomeController
{
    private HomeView _console;
    private Dictionary<int, Action> _userActions;

    public HomeController()
    {
        _console = new HomeView();
        _userActions = new Dictionary<int, Action>
        {
            { 1, CreateGame },
            { 2, Option2 },
            { 3, Option3 },
            { 4, Option4 }
        };
    }

    public void BeginInteraction()
    {
        bool isRunning = true;
        while (isRunning)
        {
            _console.Menu();
            int choice = _console.GetChoice(">> ");
            isRunning = HandleUserChoice(choice);
        }
    }

    private bool HandleUserChoice(int input)
    {
        if (input == 0)
        {
            _console.Bye();
            return false;
        }

        if (_userActions.TryGetValue(input, out var action))
        {
            action();
        }
        else
        {
            _console.InvalidChoice(input);
        }

        return true;
    }

    public void CreateGame()
    {
        Console.WriteLine("Option 1");
        var gameView = new GameView();
        var gamesRepo = new GamesRepo("Games.json");
        var gameController = new GameController(gameView, gamesRepo);
        gameController.BeginInteraction(GameController.Context.CreateGame);
    }

    public void Option2()
    {
        Console.WriteLine("Option 2");
        var gameView = new GameView();
        var gamesRepo = new GamesRepo("Games.json");
        var gameController = new GameController(gameView, gamesRepo);
        gameController.BeginInteraction(GameController.Context.LoadGame);
    }

    public void Option3()
    {
        Console.WriteLine("Option 3");
        Console.ReadLine();
    }

    public void Option4()
    {
        Console.WriteLine("Option 4");
        Console.ReadLine();
    }
}

    // private bool HandleUserChoice(int input)
    // {
    //     switch (input)
    //     {
    //         case 0:
    //             console.Bye();
    //             return false;
    //         case 1:
    //             Console.WriteLine("You chose option 1");
    //             break;
    //         case 2:
    //             Console.WriteLine("You chose option 2");
    //             // Add logic for option 2
    //             break;
    //         case 3:
    //             Console.WriteLine("You chose option 3");
    //             // Add logic for option 3
    //             break;
    //         case 4:
    //             Console.WriteLine("You chose option 4");
    //             // Add logic for option 4
    //             break;
    //         default:
    //             Console.WriteLine("Invalid choice, please try again.");
    //             break;
    //     }
    // }