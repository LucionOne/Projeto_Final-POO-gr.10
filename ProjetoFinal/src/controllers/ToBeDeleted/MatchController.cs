// using Models;
// using Container.DTOs;
// using Templates;
// using System.ComponentModel;
// using Context;
// using System.Collections;

// namespace Controller;

// public enum StartContext
// {
//     Create,
//     Load,
//     Cancel
// }

// public enum TeamEnumRL
// {
//     HomeR,
//     VisitantL,
//     Unset
// }

// public class MatchController
// {
//     private readonly IMatchView _view;
//     private DataContext _data;

//     private bool _saved { get { return _data.GamesRepo.Saved; } }
//     private bool isRunning = true;

//     private Game game = new();


//     public MatchController(IMatchView view, DataContext data)
//     {
//         _view = view;
//         _data = data;
//     }

//     public void BeginInteraction(StartContext actionContext)
//     {
//         isRunning = true;
//         game = HandleContext(actionContext);
//         RunGame(game);
//     }

//     private void RunGame(Game game)
//     {
//         while (isRunning)
//         {
//             GameDto gameDto = new(game);
//             int choice = _view.MainMenu(gameDto);
//             HandleChoice(choice);
//         }
//     }

//     private void HandleChoice(int choice)
//     {

//         switch (choice)
//         {
//             case 0://exit W
//                 isRunning = _view.Bye(_saved);
//                 break;

//             case 1://goal WI
//                 (var team, var score) = _view.AcquireGoalInfo();
//                 if (team)
//                     game.HomeScore += score;
//                 else if (!team)
//                     game.AdversaryScore += score;
//                 break;

//             case 3://Add Player to line
//                 PlayerDto playerDto = _view.GetPlayerInput() ?? new();
//                 Player player = new(playerDto);
//                 game.AddPlayerToLineUp(player);
//                 break;

//             case 4:
//                 AddTeam();
//                 break;
//             case 7:
//                 NextMatch();
//                 break;
//             case 8:
            
//                 break;
//             default:
//                 Console.WriteLine("invalid choice");
//                 break;
//         }
//     }

//     private void NextMatch()
//     {
//         if (game.TeamsLineUp.Count == 0)
//         {
//             _view.NotEnoughTeams(new GameDto(game));
//             return;
//         }

//         TeamEnumRL winner = _view.EndMatch(new GameDto(game));
//         if (game.HomeTeam.Side == winner)
//         {
//             game.AdversaryTeam = game.TeamsLineUp[0];
//             game.TeamsLineUp.RemoveAt(0);
//             game.AdversaryTeam.Side = TeamEnumRL.VisitantL;
//         }
//         else if (game.AdversaryTeam.Side == winner)
//         {
//             game.HomeTeam = game.TeamsLineUp[0];
//             game.TeamsLineUp.RemoveAt(0);
//             game.HomeTeam.Side = TeamEnumRL.HomeR;
//         }
//     }

// // 
//     private void AddTeam()
//     {
//         StartContext context = _view.CreateOrLoadTeam();

//         switch (context)
//         {
//             case StartContext.Create:
//                 List<PlayerDto> playerDtos = _data.PlayerRepo.GetAll().Select(p => new PlayerDto(p)).ToList();
//                 TeamDto teamDto = _view.CreateTeam(playerDtos, game.TeamFormation);
//                 Team teamCreate = new(teamDto);
//                 bool confirmation = _view.ConfirmationMakeTeamOfficial(teamDto);
//                 if (confirmation)
//                 {
//                     _data.TeamRepo.Add(teamCreate);
//                     teamCreate = _data.TeamRepo.Last()!;
//                 }
//                 game.AddTeamInLine(teamCreate);
//                 break;
//             case StartContext.Load:
//                 List<TeamDto> teamDtos = _data.TeamRepo.GetAll().Select(t => new TeamDto(t)).ToList();
//                 int Id = _view.GetIdOfTeam(teamDtos, game.TeamFormation);
//                 if (Id == -1) { return; }
//                 Team teamLoad = _data.TeamRepo.GetById(Id)!;
//                 game.AddTeamInLine(teamLoad);
//                 break;

//             case StartContext.Cancel:
//                 break;
//         }
//     }


//     private Game HandleContext(StartContext actionContext)
//     {
//         Game game;
//         switch (actionContext)
//         {
//             case StartContext.Create:
//                 game = CreateGame();
//                 break;
//             case StartContext.Load:
//                 game = GetGameFromDb();
//                 break;
//             default:
//                 throw new Exception("Invalid actionContext");
//         }
//         return game;
//     }

//     public Game CreateGame()
//     {
//         GameDto gamePackage = _view.AcquireGameInfo();
//         Game game = new Game(gamePackage);
//         _data.GamesRepo.Add(game);
//         var allGames = _data.GamesRepo.GetAll();

//         if (allGames.Count == 0)
//             throw new InvalidOperationException("No games found in the repository.");

//         return allGames.LastOrDefault() ?? throw new InvalidOperationException("Failed to retrieve the last game from the repository.");
//     }

//     public Game GetGameFromDb()
//     {
//         List<Game> games = _data.GamesRepo.GetAll();
//         List<GameDto> dtoList = BuildDtoListFromRepo(games);

//         int id = _view.GetIdForGame(dtoList);
//         Game game = _data.GamesRepo.GetById(id) ?? throw new ArgumentNullException("The id returned a null game");
//         return game;
//     }

//     private List<GameDto> BuildDtoListFromRepo(List<Game> games)
//     {
//         return games.Select(game => new GameDto(game)).ToList();
//     }



// }