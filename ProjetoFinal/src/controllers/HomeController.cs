using View;
using Templates;
using MyRepository;
using jogo;
using Context;

namespace Controller;

public class HomeController
{
    private HomeView _view;
    private Dictionary<int, Action> _userActions;
    private DataContext _data;


    public HomeController(DataContext data, HomeView view)
    {

        _data = data;

        _view = view;

        _userActions = new Dictionary<int, Action>
        {
            { 1, CreateGame },
            { 2, LoadGame },
            { 3, ManagePlayers },
            { 4, Option4 }
        };
    }

    public void BeginInteraction() //needs to be MVC like ⚠️
    {
        bool isRunning = true;
        while (isRunning)
        {
            _view.Menu();
            int choice = _view.GetChoice(">> ");
            isRunning = HandleUserChoice(choice);
        }
    }


    private bool HandleUserChoice(int input)
    {
        if (input == 0)
        {
            _view.Bye();
            return false;
        }

        if (_userActions.TryGetValue(input, out var action))
        {
            action();
        }
        else
        {
            _view.InvalidChoice(input);
        }

        return true;
    }


    public void CreateGame()
    {
        // Console.WriteLine("Option 1");
        var gameView = new GameView();
        var gameController = new GameController(gameView, _data.GamesRepo);
        gameController.BeginInteraction(GameController.Context.CreateGame);
    }


    public void LoadGame()
    {
        // Console.WriteLine("Option 2");
        var gameView = new GameView();
        var gameController = new GameController(gameView, _data.GamesRepo);
        gameController.BeginInteraction(GameController.Context.LoadGame);
    }

    public void ManagePlayers()
    {
        // Console.WriteLine("Option 3");
        var playerView = new PlayerView();
        var playerController = new PlayerController(_data, playerView);
        playerController.BeginInteraction();
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