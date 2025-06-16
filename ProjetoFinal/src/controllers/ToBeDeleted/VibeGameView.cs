// using System;
// using System.Collections.Generic;
// using Container.DTOs;
// using Templates.view;
// using VS;

// public class VibeGameView : IGameView
// {
//     private readonly VibeShell _vibe;

//     public VibeGameView(VibeShell vibe)
//     {
//         _vibe = vibe;
//     }

//     public int MainMenu(bool saved)
//     {
//         _vibe.Clear(extraClean: true, render: false);
//         _vibe.ChangeHeader(["⚽  G A M E   M A N A G E M E N T"], render: false);

//         string status = saved ? "✔ All data is saved." : "⚠ Unsaved changes detected.";
//         _vibe.ChangePageInfo(new List<string> { status }, render: false);

//         var options = new List<string>
//         {
//             "Create New Game",
//             "Edit Game",
//             "Delete Game",
//             "Show All Games",
//             "Save to Database",
//             "Return to Main Menu"
//         };

//         var descriptions = new List<List<string>>
//         {
//             new() { "Start a new game and assign teams." },
//             new() { "Edit existing game data." },
//             new() { "Remove a game permanently." },
//             new() { "View all recorded games." },
//             new() { "Persist data to external storage." },
//             new() { "Return to the main application menu." }
//         };

//         return _vibe.HandleMenu(
//             options: options,
//             descriptions: descriptions,
//             defaultIndex: 0,
//             menuScale: 0.65f
//         );
//     }

//     public bool Bye(bool saved)
//     {
//         _vibe.Clear(extraClean: true, render: false);
//         _vibe.ChangeHeader(["👋  E X I T   G A M E   M O D U L E"], render: false);

//         string msg = saved
//             ? "All changes are saved. Do you want to exit?"
//             : "You have unsaved changes. Exit anyway?";

//         var options = new List<string> { "Yes", "No" };
//         var descriptions = new List<List<string>>
//         {
//             new() { "Exit the game management module." },
//             new() { "Return to the menu to continue working." }
//         };

//         int choice = _vibe.HandleMenu(
//             options: options,
//             descriptions: descriptions,
//             defaultIndex: 1,
//             menuScale: 0.5f
//         );

//         return choice == 1 ? true : false; // false means exit
//     }

//     public bool ConfirmSaveToDB()
//     {
//         _vibe.Clear(extraClean: true, render: false);
//         _vibe.ChangeHeader(["💾  S A V E   D A T A B A S E"], render: false);
//         _vibe.ChangePageInfo(new List<string> { "Would you like to save the current data to the database?" }, render: false);

//         var options = new List<string> { "Yes", "No" };
//         var descriptions = new List<List<string>>
//         {
//             new() { "Store all current game data into the database." },
//             new() { "Skip saving and return to the previous menu." }
//         };

//         int choice = _vibe.HandleMenu(
//             options: options,
//             descriptions: descriptions,
//             defaultIndex: 0,
//             menuScale: 0.45f
//         );

//         return choice == 0;
//     }
//     // public void ShowAllGames(List<GameDto> games)
//     // {
//     //     // Empty implementation for testing
//     // }

//     // public void ShowGameDetails(GameDto game)
//     // {
//     //     // Empty implementation for testing
//     // }

//     // public GameDto? CreateGame(List<TeamDto> teams)
//     // {
//     //     // Empty implementation for testing
//     //     return null;
//     // }

//     // public GameDto? EditGame(GameDto game, List<TeamDto> teams)
//     // {
//     //     // Empty implementation for testing
//     //     return null;
//     // }

//     public bool ConfirmDeleteGame(GameDto game)
//     {
//         // Empty implementation for testing
//         return false;
//     }
//     public GameDto? GetGameInput(List<TeamDto> teams)
//     {
//         return null;
//     }
//     public void ShowGames(List<GameDto> games)
//     {

//     }
//     public int GetGameId(List<GameDto> games)
//     {
//         return -1;
//     }
//     public GameDto? GetGameEdit(List<TeamDto> teams, GameDto game)
//     {
//         return null;
//     }
// }
