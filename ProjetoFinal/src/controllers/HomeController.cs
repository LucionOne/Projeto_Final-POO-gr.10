using View;
using Templates;
using MyRepository;
using Models;
using Context;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using VS;

namespace Controller;

public class HomeController
{
    private IHomeView _view;
    private Dictionary<int, Action> _userActions;
    private DataContext _data;
    bool isRunning = true;


    public HomeController(DataContext data, IHomeView view)
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
        while (isRunning)
        {
            int choice = _view.MainMenu();
            HandleUserChoice(choice);
        }
    }


    private void HandleUserChoice(int input)
    {
        if (input == 0)
        {
            isRunning = !_view.Bye();
        }
        else if (_userActions.TryGetValue(input, out var action))
        {
            action();
        }
        else
        {
            _view.InvalidChoice(input);
        }
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
        var vibePlayerView = new VibePlayerView(_view.GetVibe());
        var playerController = new PlayerController(vibePlayerView, _data);
        playerController.BeginInteraction();
    }

    public void ManageTeams()
    {
        var vibeTeamView = new VibeTeamView(_view.GetVibe());
        var teamController = new TeamController(vibeTeamView, _data);
        teamController.BeginInteraction();
    }
}
