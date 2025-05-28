using Container.DTOs;

namespace Templates;

public interface IGameView
{
    GameDto AcquireGameInfo();
    int GetIdForGame(List<GameDto> games);
}