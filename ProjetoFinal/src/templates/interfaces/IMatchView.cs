using Container.DTOs;
using Context;
using Controller;
using Lib.TeamFormation;

namespace Templates;

public interface IMatchView : IView
{
    GameDto AcquireGameInfo();
    int GetIdForGame(List<GameDto> games);
    (bool team, int score) AcquireGoalInfo(string homeName = "Home", string AdvName = "Visitant");

    int MainMenu(GameDto game);
    bool Bye(bool saved);
    PlayerDto GetPlayerInput();
    StartContext CreateOrLoadTeam();
    TeamDto CreateTeam(List<PlayerDto> playerDtos, TeamFormation formation);
    bool ConfirmationMakeTeamOfficial(TeamDto team);
    int GetIdOfTeam(List<TeamDto> teams, TeamFormation formation);
    TeamEnumRL EndMatch(GameDto game);
    void NotEnoughTeams(GameDto game);
}