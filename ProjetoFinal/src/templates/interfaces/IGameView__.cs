using System.Collections.Generic;
using Container.DTOs;

namespace Templates.view
{
    /// <summary>
    /// Interface for GameView interactions. Abstracts all user interaction logic for GameController.
    /// </summary>
    public interface IGameView__
    {
        /// <summary>
        /// Shows the main menu and returns the user's choice.
        /// </summary>
        /// <param name="saved">Indicates if the data is saved.</param>
        /// <returns>User's menu choice as an integer.</returns>
        int MainMenu(bool saved, GameDto game);

        /// <summary>
        /// Shows a goodbye message and returns whether to continue running.
        /// </summary>
        /// <param name="saved">Indicates if the data is saved.</param>
        /// <returns>False if the user wants to exit, true otherwise.</returns>
        bool Bye(bool saved);

        // /// <summary>
        // /// Prompts the user to confirm saving to the database.
        // /// </summary>
        // /// <returns>True if the user confirms, false otherwise.</returns>
        // bool ConfirmSaveToDB();

        // /// <summary>
        // /// Prompts the user for input to create a new game.
        // /// </summary>
        // /// <param name="teams">List of available teams as DTOs.</param>
        // /// <returns>A GameDto with the user's input.</returns>
        // GameDto? GetGameInput(List<TeamDto> teams);

        // /// <summary>
        // /// Displays a list of games.
        // /// </summary>
        // /// <param name="games">List of GameDto to display.</param>
        // void ShowGames(List<GameDto> games);

        // /// <summary>
        // /// Prompts the user to select a game by ID.
        // /// </summary>
        // /// <param name="games">List of GameDto to choose from.</param>
        // /// <returns>The selected game's ID, or -1 if cancelled.</returns>
        // int GetGameId(List<GameDto> games);

        // /// <summary>
        // /// Prompts the user for input to edit a game.
        // /// </summary>
        // /// <param name="teams">List of available teams as DTOs.</param>
        // /// <param name="game">The game to edit as a DTO.</param>
        // /// <returns>A GameDto with the edited data.</returns>
        // GameDto? GetGameEdit(List<TeamDto> teams, GameDto game);

        // /// <summary>
        // /// Prompts the user to confirm deletion of a game.
        // /// </summary>
        // /// <param name="game">The game to delete as a DTO.</param>
        // /// <returns>True if the user confirms, false otherwise.</returns>
        // bool ConfirmDeleteGame(GameDto game);
        public int GetPlayerId(List<PlayerDto> players);
        public int GetTeamId(List<TeamDto> teams);
        public int ManageLineup(List<PlayerDto> players, List<TeamDto> teams, GameDto game);
    }
}
