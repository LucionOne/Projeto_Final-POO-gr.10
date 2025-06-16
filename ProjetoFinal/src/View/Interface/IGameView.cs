using Container.DTOs;
using Context;
using Controller;
using Lib.TeamFormation;

namespace Templates;

public interface IGameView
{
    int MainMenu(bool saved, GameDto game);
    bool Bye(bool saved);

    GameDto? GetGameInput();
    GameDto? GetGameEdit(GameDto game);
    int GetGameId(List<GameDto> games);

    bool ConfirmGameEdit(GameDto oldGame, GameDto newGame);
    bool ConfirmGameDelete(GameDto game);
    bool ConfirmGameAdd(GameDto game);

    bool ConfirmSaveToDatabase(bool saved);

    void ShowGames(List<GameDto> games);

    int GetTeamMaker();
}