using Container.DTOs;
using Context;
using Controller;
using Lib.TeamFormation;
using Models;

namespace Templates;

public interface IGameView
{
    GameController.GameChoices MainMenu(bool saved, GameDto game);
    bool Bye(bool saved);

    List<TeamDto> FastTeamBuilder(List<List<PlayerDto>> playersFormation);

    List<int> GetPlayers(List<PlayerDto> players);
    List<int> GetTeams(List<TeamDto> teams, GameDto? game = null);

    GameDto? GetGameInput();
    GameDto? GetGameEdit(GameDto game);

    GameController.Sides GetWhoWon(GameDto game);

    Event? GetEventInput(List<PlayerDto> players);

    int GetGameId(List<GameDto> games);
    int GetTeamMakingMethod();

    bool ConfirmGameEdit(GameDto oldGame, GameDto newGame);
    bool ConfirmGameDelete(GameDto game);
    bool ConfirmGameAdd(GameDto game);
    bool ConfirmSaveToDatabase(bool saved);

    void ShowGames(List<GameDto> games);
    void ShowTeams(TeamDto Home, TeamDto Guest, List<TeamDto> TeamLineUp);
}