using System.Collections.Generic;
using Container.DTOs;

namespace Templates.view
{
    /// <summary>
    /// Interface for TeamView interactions. Abstracts all user interaction logic for TeamController.
    /// </summary>
    public interface ITeamView
    {
        /// <summary>
        /// Shows the main menu and returns the user's choice.
        /// </summary>
        /// <param name="saved">Indicates if the data is saved.</param>
        /// <returns>User's menu choice as an integer.</returns>
        int MainMenu(bool saved);

        /// <summary>
        /// Shows a goodbye message and returns whether to continue running.
        /// </summary>
        /// <param name="saved">Indicates if the data is saved.</param>
        /// <returns>False if the user wants to exit, true otherwise.</returns>
        bool Bye(bool saved);

        /// <summary>
        /// Prompts the user to confirm saving to the database.
        /// </summary>
        /// <returns>True if the user confirms, false otherwise.</returns>
        bool ConfirmSaveToDB();

        /// <summary>
        /// Prompts the user for input to create a new team.
        /// </summary>
        /// <param name="players">List of available players as DTOs.</param>
        /// <returns>A TeamDto with the user's input.</returns>
        TeamDto GetTeamInput(List<PlayerDto> players);

        /// <summary>
        /// Displays a list of teams.
        /// </summary>
        /// <param name="teams">List of TeamDto to display.</param>
        void ShowTeams(List<TeamDto> teams);

        /// <summary>
        /// Prompts the user to select a team by ID.
        /// </summary>
        /// <param name="teams">List of TeamDto to choose from.</param>
        /// <returns>The selected team's ID, or -1 if cancelled.</returns>
        int GetTeamId(List<TeamDto> teams);

        /// <summary>
        /// Prompts the user for input to edit a team.
        /// </summary>
        /// <param name="players">List of available players as DTOs.</param>
        /// <param name="team">The team to edit as a DTO.</param>
        /// <returns>A TeamDto with the edited data.</returns>
        TeamDto GetTeamEdit(List<PlayerDto> players, TeamDto team);

        /// <summary>
        /// Prompts the user to confirm deletion of a team.
        /// </summary>
        /// <param name="team">The team to delete as a DTO.</param>
        /// <returns>True if the user confirms, false otherwise.</returns>
        bool ConfirmDeleteTeam(TeamDto team);
    }
}
