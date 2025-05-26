using jogo;
using DTOs;
using Templates;

namespace Controller;

public class GameController
{
    private readonly IGameView _view;

    public enum Context
    {
        CreateGame,
        LoadGame
    }

    public GameController(IGameView view)
    {
        _view = view;
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
        if ()
        _view.ShowDb();
    }
}