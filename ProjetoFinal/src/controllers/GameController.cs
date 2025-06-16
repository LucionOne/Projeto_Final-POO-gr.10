using Models;
using Container.DTOs;
using Templates;
using System.ComponentModel;
using Context;
using System.Collections;

namespace Controller;


public class GameController
{
    public enum StartContext
    {
        Create,
        Load,
        Cancel
    }

    public enum TeamEnumRL
    {
        HomeR,
        VisitantL,
        Unset
    }

    private readonly IGameView _view;
    private DataContext _data;

    private bool _saved { get { return _data.GamesRepo.Saved; } }
    private bool isRunning = true;

    private Game game = new();


    public GameController(IGameView view, DataContext data)
    {
        _view = view;
        _data = data;
    }

    public void BeginInteraction(StartContext actionContext)
    {
        isRunning = true;
        game = HandleContext(actionContext);
        while (isRunning)
        {
            GameDto gameDto = new(game);
            int choice = _view.MainMenu(_saved, gameDto);
            HandleChoice(choice);
        }
    }

    private void HandleChoice(int choice)
    {

        switch (choice)
        {
            case 0:
                isRunning = _view.Bye(_saved);
                break;

            case 5:
                WriteToDatabase();
                break;
            default:
                Console.WriteLine("invalid choice");
                break;
        }
    }

    private Game HandleContext(StartContext actionContext)
    {
        Game game = new();
        switch (actionContext)
        {
            case StartContext.Create:
                game = CreateGame();
                break;
            case StartContext.Load:
                game = GetGameFromDb();
                break;
            case StartContext.Cancel:
                isRunning = false;
                break;
        }
        return game;
    }

    public Game CreateGame()
    {
        GameDto? gamePackage = _view.GetGameInput(); // Get basic info

        if (gamePackage == null) { isRunning = false; return new(); }

        Game game = new Game(gamePackage);
        _data.GamesRepo.Add(game); // Adds the game to the repo
        Game game1 = _data.GamesRepo.Last() // Return the ref of the game in its repo
            ?? throw new InvalidOperationException("Failed to retrieve from the repository.");

        return game1;
    }

    public Game GetGameFromDb()
    {
        List<GameDto> dtoList = RepoToDto();
        int id = _view.GetGameId(dtoList);

        if (id < 0) { isRunning = false; return new(); }

        Game game = _data.GamesRepo.GetById(id) ?? throw new ArgumentNullException("The id returned a null game");

        return game;
    }

    public void WriteToDatabase()
    {
        if (_saved)
        {
            bool confirm = _view.ConfirmSaveToDatabase(_saved);
            if (confirm)
            {
                _data.GamesRepo.WriteToDataBase();
            }
        }
        else
        {
            _data.GamesRepo.WriteToDataBase();
        }
    }

    private List<GameDto> RepoToDto()
    {
        return _data.GamesRepo.GetAll().Select(game => new GameDto(game)).ToList();
    }

}