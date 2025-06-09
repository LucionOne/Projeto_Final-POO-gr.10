using View;
using Templates;
using MyRepository;
using Models;
using Context;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

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
            { 1, StartGame },
            { 2, LoadGame },
            { 3, ManagePlayers },
            { 4, ManageTeams }
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


    public void StartGame()
    {

        // var gameView = new GameView();
        // var gameController = new MatchController(gameView, _data);
        // gameController.BeginInteraction(StartContext.Create);
    }


    public void LoadGame()
    {
        // var gameView = new GameView();
        // var gameController = new MatchController(gameView, _data);
        // gameController.BeginInteraction(StartContext.Load);
    }

    public void ManagePlayers()
    {
        var playerView = new PlayerView();
        var playerController = new PlayerController(playerView, _data);
        playerController.BeginInteraction();
    }

    public void ManageTeams()
    {
        var teamView = new TeamView();
        var teamController = new TeamController(teamView, _data);
        teamController.BeginInteraction();
    }
}
