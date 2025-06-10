using Container.DTOs;

public interface IPlayerView
{
    int MainMenu(bool saved);
    bool Bye(bool saved);
    PlayerDto? GetPlayerInput();
    PlayerDto? GetPlayerEdit(PlayerDto oldPlayer);
    int GetPlayerId(List<PlayerDto> players);
    bool ConfirmPlayerEdit(PlayerDto oldPlayer, PlayerDto newPlayer);
    bool ConfirmPlayerDelete(PlayerDto player);
    bool ConfirmSaveToDatabase(bool saved);
    void ShowPlayers(List<PlayerDto> players);
}