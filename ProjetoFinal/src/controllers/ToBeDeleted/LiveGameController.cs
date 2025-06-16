// using Container.DTOs;
// using Context;
// using Templates;
// using Templates.view;
// using Models;



// using System.Security.Cryptography.X509Certificates;
// using System.Reflection.Metadata;
// using Lit.TeamBuilder;

// public class LiveGameController
// {
//     private readonly IGameView _view;            // your console UI
//     private readonly DataContext _data;  // business logic + persistence
//     private bool saved => _data.GamesRepo.Saved; // check if data is saved
//     bool isRunning = true;

//     private Game _currentGame;

//     public LiveGameController(IGameView view, DataContext data, Game game)
//     {
//         _view = view;
//         _data = data;
//         _currentGame = game; // initialize with the game to be played
//     }


//     public void RunLiveSession()
//     {

//         while (isRunning)
//         {


//             while (isRunning)
//             {
//                 var choice = _view.MainMenu(saved, new GameDto(_currentGame));
//                 HandleChoice(choice);
//                 // 1) Show live menu
//                 //     var options = new List<string>
//                 // {
//                 //     "Add Goal",
//                 //     "Add Card",
//                 //     "Add Event",
//                 //     "Add New Player",
//                 //     "Add New Team",
//                 //     "Manage Line",
//                 //     "End Match",
//                 //     "Time Out / Switch Side",
//                 //     "End Game"
//                 // };

//                 //     int choice = _view.MainMenu(/* pass saved flag? */ false);

//                 //     switch (choice)
//                 //     {
//                 //         case 0: AddGoal(); break;
//                 //         case 1: AddCard(); break;
//                 //         case 2: AddCustomEvent(); break;
//                 //         case 3: AddNewPlayer(); break;
//                 //         case 4: AddNewTeam(); break;
//                 //         case 5: ManageLineup(); break;
//                 //         case 6: EndMatch(); break;
//                 //         case 7: TimeOutAndSwitch(); break;
//                 //         case 8: isRunning = ConfirmEndGame(); break;
//                 //         default: break;
//                 //     }

//                 //     // After each action, save progress if you like:
//                 //     _data.SaveLive(_currentGame);
//             }
//         }
//     }
//     public void HandleChoice(int choice)
//     {
//         switch (choice)
//         {
//             case 0: // Exit
//                 isRunning = _view.Bye(saved);
//                 break;
//             case 1: // Add Goal
//                 AddGoal();
//                 break;
//             case 2: // Add Card
//                 AddCard();
//                 break;
//             case 3: // Add Custom Event
//                 AddCustomEvent();
//                 break;
//             case 4: // Add New Player
//                 AddNewPlayer();
//                 break;
//             case 5: // Add New Team
//                 AddNewTeam();
//                 break;
//             case 6: // Manage Lineup
//                 ManageLineup();
//                 break;
//             case 7: // End Match
//                 EndMatch();
//                 break;
//             case 8: // Time Out and Switch
//                 TimeOutAndSwitch();
//                 break;
//             default:
//                 throw new ArgumentOutOfRangeException("Invalid choice. Please select a valid option.");
//         }
//     }

//     private void AddGoal()
//     {
//         // 1) pick scoring team (Home/Adversary)
//         // 2) pick player from that team or casuals
//         // 3) pick minute (auto or manual)
//         // 4) _currentGame.Events.Add(new GoalEvent(...));
//     }

//     private void AddCard()
//     {
//         // similar: pick type (yellow/red), team, player, minute
//     }

//     private void AddCustomEvent()
//     {
//         // free text + minute
//     }

//     private void AddNewPlayer()
//     {
//         int id = _view.GetPlayerId(_data.PlayerRepo.GetAll().Select(p => new PlayerDto(p)).ToList());
//         if (id < 0) return; // user cancelled
//         Player player = _data.PlayerRepo.GetById(id) ?? new Player(id);
//         _currentGame.AddPlayerToLineUp(player);
//     }

//     private void AddNewTeam()
//     {
//         int id = _view.GetTeamId(_data.TeamRepo.GetAll().Select(t => new TeamDto(t)).ToList());
//         if (id < 0) return; // user cancelled
//         Team team = _data.TeamRepo.GetById(id) ?? new Team(id);
//         _currentGame.AddTeamToLine(team);
//     }

//     private void ManageLineup()
//     {
//         int choice = _view.ManageLineup();
//         switch (choice)
//         {
//             case 0:
//                 TeamBuilder.BuildAllAny(_currentGame.PlayersLineUp, _currentGame.TeamFormation);
//                 break;
//             case 1:
//                 TeamBuilder.BuildBasic(_currentGame.PlayersLineUp, _currentGame.TeamFormation, out TeamBuilder.PlayerPosition error);
//                 break;
//             default:
//                 break;
//         }
//     }

//     private void EndMatch()
//     {
//         _currentGame.MatchEnded = true;
//         _view.ShowGames(new List<GameDto> { _currentGame }); // summary
//     }

//     private void TimeOutAndSwitch()
//     {
//         // insert a “timeout” event, then swap home/adversary roles
//     }

//     private bool ConfirmEndGame()
//     {
//         return _view.ConfirmDeleteGame(_currentGame); // repurpose for “exit?”
//     }
    
// }
