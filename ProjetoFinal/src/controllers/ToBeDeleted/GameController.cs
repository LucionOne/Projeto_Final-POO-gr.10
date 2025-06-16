// using Context;
// using Models;
// using Templates.view;
// using Container.DTOs;

// namespace Controller;

// public class GameController
// {
//     private DataContext _data;
//     private IGameView _view;

//     private bool _saved { get { return _data.GamesRepo.Saved; } }
//     private bool isRunning = true;

//     private List<GameDto> RepoDto { get { return RepoToDto(); } }

//     public GameController(DataContext data, IGameView view)
//     {
//         _data = data;
//         _view = view;
//     }

//     public void BeginInteraction()
//     {
//         isRunning = true;
//         while (isRunning)
//         {
//             int input = _view.MainMenu(_saved);
//             HandleUserChoice(input);
//         }
//     }

//     private void HandleUserChoice(int choice)
//     {
//         switch (choice)
//         {
//             case 0:
//                 isRunning = _view.Bye(_saved);
//                 break;
//             case 1:
//                 CreateGame();
//                 break;
//             case 2:
//                 EditGame();
//                 break;
//             case 3:
//                 DeleteGame();
//                 break;
//             case 4:
//                 ListGames();
//                 break;
//             case 5:
//                 SaveToDataBase();
//                 break;
//             default:
//                 throw new ArgumentOutOfRangeException("Invalid choice. Please select a valid option.");
//         }
//     }

//     private void SaveToDataBase()
//     {
//         bool confirmation = _view.ConfirmSaveToDB();
//         if (confirmation) { _data.GamesRepo.WriteToDataBase(); }
//     }

//     private void CreateGame()
//     {
//         var gameDto = _view.GetGameInput(_data.TeamRepo.GetAll().Select(t => new TeamDto(t)).ToList());
//         var game = new Game(gameDto);
//         _data.GamesRepo.Add(game);
//     }

//     private void ListGames()
//     {
//         _view.ShowGames(RepoDto);
//     }

//     private void EditGame()
//     {
//         var id = _view.GetGameId(RepoDto);
//         if (id < 0) { return; }
//         var gameToEdit = _data.GamesRepo.GetById(id) ?? new Game(id);
//         var gameDto = _view.GetGameEdit(_data.TeamRepo.GetAll().Select(t => new TeamDto(t)).ToList(), Map(gameToEdit));
//         _data.GamesRepo.UpdateById(id, new Game(gameDto));
//     }

//     private void DeleteGame()
//     {
//         var id = _view.GetGameId(RepoDto);
//         if (id < 0) { return; }
//         var gameToDelete = _data.GamesRepo.GetById(id);
//         if (gameToDelete == null) { return; }
//         var confirmation = _view.ConfirmDeleteGame(Map(gameToDelete));
//         if (confirmation)
//         {
//             _data.GamesRepo.Remove(gameToDelete);
//         }
//     }

//     // Map methods
//     public List<GameDto> RepoToDto()
//     {
//         return _data.GamesRepo.GetAll().Select(g => Map(g)).ToList();
//     }

//     public GameDto Map(Game game)
//     {
//         // You may want to expand this mapping if GameDto needs more info
//         return new GameDto(game);
//     }
// }
