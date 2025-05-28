using jogo;
using Container.DTOs;
using Templates;

namespace Controller;

public class GameController
{
    private readonly IGameView _view;
    private readonly IRepo<Game> _repo;

    public enum Context
    {
        CreateGame,
        LoadGame
    }

    public GameController(IGameView view, IRepo<Game> repo)
    {
        _view = view;
        _repo = repo;
    }

    public void BeginInteraction(Context actionContext)
    {
        Game game = HandleContext(actionContext);
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
        return game;
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