using Models;
using Container.DTOs;
using Templates;
using System.ComponentModel;

namespace Controller;

public class MatchController
{
    private readonly IMatchView _view;
    private readonly IRepo<Game> _repo;

    public enum Context
    {
        CreateGame,
        LoadGame
    }

    public MatchController(IMatchView view, IRepo<Game> repo)
    {
        _view = view;
        _repo = repo;
    }

    public void BeginInteraction(Context actionContext)
    {
        Game game = HandleContext(actionContext);
        RunGame(game);
    }

    private void RunGame(Game game)
    {
        bool isRunning = true;
        while (isRunning)
        {
            GameDto gameDto = new(game);
            _view.ShowGameAndOptions(gameDto);
            int choice = _view.GetChoice(">> ");
            isRunning = HandleChoice(choice, game);
        }
    }

    private bool HandleChoice(int choice, Game game)
    {

        switch (choice)
        {
            case 0://exit W
                return false;
            case 1://goal WI
                (var team, var score) = _view.AcquireGoalInfo();
                if (team)
                    game.HomeScore += score;
                else if (!team)
                    game.AdversaryScore += score;
                break;
            case 3://Add Player to line

                break;
            case 4:// create team

                break;
            case 7://Next Match

                break;
            case 9://edit Game

                break;
            default:
                Console.WriteLine("invalid choice");
                break;
        }
        return true;
    }


    private Game HandleContext(Context actionContext)
    {
        Game game;
        switch (actionContext)
        {
            case Context.CreateGame:
                game = CreateGame();
                break;
            case Context.LoadGame:
                game = GetGameFromDb();
                break;
            default:
                throw new Exception("Invalid actionContext");
        }
        return game;
    }

    public Game CreateGame()
    {
        GameDto gamePackage = _view.AcquireGameInfo();
        Game game = new Game(gamePackage);
        _repo.Add(game);
        var allGames = _repo.GetAll();
        if (allGames.Count == 0)
            throw new InvalidOperationException("No games found in the repository.");

        return allGames[allGames.Count - 1];
}

    public Game GetGameFromDb()
    {
        List<Game> games = _repo.GetAll();
        List<GameDto> dtoList = BuildDtoListFromRepo(games);

        int id = _view.GetIdForGame(dtoList);
        Game game = _repo.GetById(id) ?? throw new ArgumentNullException("The id returned a null game");
        return game;
    }

    private List<GameDto> BuildDtoListFromRepo(List<Game> games)
    {
        return games.Select(game => new GameDto(game)).ToList();
    }

}