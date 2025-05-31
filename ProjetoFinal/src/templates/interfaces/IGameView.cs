using Container.DTOs;

namespace Templates;

public interface IGameView : IView
{
    GameDto AcquireGameInfo();
    int GetIdForGame(List<GameDto> games);
    void ShowGameAndOptions(GameDto game);
}