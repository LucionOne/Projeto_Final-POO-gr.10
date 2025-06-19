using Models;
using Container.DTOs;
using Templates;
using System.ComponentModel;
using Context;
using System.Collections;

namespace Controller;


public class GameController
{
    public enum StartContext
    {
        Create,
        Load,
        Cancel
    }

    public enum Sides
    {
        Home,
        Guest,
        Cancel
    }


    private readonly IGameView _view;
    private DataContext _data;

    private bool _saved { get { return _data.GamesRepo.Saved; } }
    private bool isRunning = true;

    private Game gameRunning = new(); //Needs to be a ref from _data.GameRepo


    public GameController(IGameView view, DataContext data)
    {
        _view = view;
        _data = data;
    }

    public void BeginInteraction(StartContext actionContext)
    {
        isRunning = true;
        gameRunning = HandleContext(actionContext);
        while (isRunning)
        {
            GameDto gameDto = new(gameRunning);
            GameChoices choice = _view.MainMenu(_saved, gameDto);
            HandleChoice(choice);
        }
    }

    #region Flow Methods

    public enum GameChoices
    {
        CreateGame,//out
        AddEvent,
        EditGame,
        AddPlayers,
        AddTeam,
        EndMatch,
        DeleteGame,
        ListGames,//out
        Save,
        Exit,
        Peek,//implement
        FallBack,
    }

    private void HandleChoice(GameChoices choice)
    {
        switch (choice)
        {
            case GameChoices.Exit:
                isRunning = _view.Bye(_saved);
                break;

            case GameChoices.EditGame:
                EditGameFlow();
                break;

            case GameChoices.AddPlayers:
                AddPlayersFlow();
                break;

            case GameChoices.AddTeam:
                AddTeamFlow();
                break;

            case GameChoices.EndMatch:
                EndMatchFlow();
                break;

            case GameChoices.ListGames:
                ListGamesFlow();
                break;

            case GameChoices.DeleteGame:
                DeleteGameFlow();
                break;

            case GameChoices.Save:
                WriteToDatabaseFlow();
                break;
            case GameChoices.AddEvent:
                AddEventFlow();
                break;
            case GameChoices.Peek:
                PeekFlow();
                break;
            case GameChoices.FallBack:
            default:
                break;
        }
    }

    private void PeekFlow()
    {
        TeamDto Home = gameRunning.HomeTeam.ToDto(_data.PlayerRepo);
        TeamDto Guest = gameRunning.GuestTeam.ToDto(_data.PlayerRepo);
        List<TeamDto> LineUp = gameRunning.TeamsLineUp
            .Select(t => t.ToDto(_data.PlayerRepo))
            .ToList();
        _view.ShowTeams(Home, Guest, LineUp);
    }

    private void AddEventFlow()
    {
        Event? _event = _view.GetEventInput(
            MapperTools.MapPlayersByIds(
                gameRunning.GetAllUniquePlayerIds(), _data)
                .Select(p => p.ToDto())
                .ToList()
            );
        if (_event == null) { return; }

        Player? player = _data.PlayerRepo.GetById(_event.PlayerId);
        
        if (player == null) { return; }

        player.AddEvent(_event);
    }

    private void EditGameFlow()
    {
        GameDto oldGamePackage = new(gameRunning);
        GameDto? newGamePackage = _view.GetGameEdit(oldGamePackage);

        if (newGamePackage == null) { return; }

        bool confirmation = _view.ConfirmGameEdit(oldGamePackage, newGamePackage);
        if (confirmation)
        {
            newGamePackage.Id = gameRunning.Id;
            _data.GamesRepo.UpdateById(gameRunning.Id, new Game(newGamePackage));
            gameRunning = new Game(newGamePackage);
            return;
        }
        else
        {
            return;
        }
    }

    private void DeleteGameFlow()
    {
        int id = _view.GetGameId(RepoToDto());

        if (id < 0) { return; }

        Game game = _data.GamesRepo.GetById(id) ?? throw new NullReferenceException("Game cannot be null");
        GameDto toDelete = game.ToDto();

        bool confirmation = _view.ConfirmGameDelete(toDelete);

        if (confirmation)
        {
            _data.GamesRepo.RemoveAt(id);
        }
        else
        {
            return;
        }
    }

    private void ListGamesFlow()
    {
        _view.ShowGames(RepoToDto());
    }

    private List<List<PlayerDto>> ConvertToPlayerDtoLists(List<List<Player>> playerLists)
    {
        return playerLists
            .Select(innerList => innerList.Select(player => player.ToDto()).ToList())
            .ToList();
    }

    private void AddTeamFlow()
    {
        int choice = _view.TeamMakingMethod();

        if (choice <= 0 || choice > 3) return;
        var teamsToAdd = new List<Team>();
        switch (choice)
        {
            case 1:
                List<List<Player>> possibleTeamsBasic = TeamBuilder.AllPossibleTeamsBasic(gameRunning.PlayersLineUp, gameRunning.TeamFormation);
                List<TeamDto> teamsPackageBasic = _view.FastTeamBuilder(ConvertToPlayerDtoLists(possibleTeamsBasic));
                teamsToAdd = teamsPackageBasic.Select(t => new Team(t)).ToList();
                break;

            case 2:
                List<List<Player>> possibleTeamsAny = TeamBuilder.AllPossibleTeamsAny(gameRunning.PlayersLineUp, gameRunning.TeamFormation);
                List<TeamDto> teamsPackageAny = _view.FastTeamBuilder(ConvertToPlayerDtoLists(possibleTeamsAny));
                teamsToAdd = teamsPackageAny.Select(t => new Team(t)).ToList();
                break;

            case 3:
                List<TeamDto> teams = _data.TeamRepo.ToDto(_data.PlayerRepo);
                List<int> TeamsIds = _view.GetTeams(teams);
                teamsToAdd = /*Mapper.*/MapperTools.MapTeamsByIds(TeamsIds, _data);
                break;
        }
        gameRunning.AddTeamsToLineUp(teamsToAdd);
    }

    private void AddPlayersFlow()
    {
        List<int> playersId = _view.GetPlayers(_data.PlayerRepo.ToDtoList());

        if (playersId == null || playersId.Count == 0) return;

        List<Player> players = /*Mapper.*/MapperTools.MapPlayersByIds(playersId, _data);

        gameRunning.AddPlayersToLineUp(players);
    }

    private void WriteToDatabaseFlow()
    {
        if (_saved)
        {
            bool confirm = _view.ConfirmSaveToDatabase(_saved);
            if (confirm)
            {
                _data.GamesRepo.WriteToDataBase();
            }
        }
        else
        {
            _data.GamesRepo.WriteToDataBase();
        }
    }

    private void EndMatchFlow()
    {
        Sides winner = _view.GetWhoWon(gameRunning.ToDto());

        switch (winner)
        {
            case Sides.Home://Home Won
                Team? teamV = gameRunning.PopNextTeam();
                if (teamV == null) return;
                gameRunning.GuestTeam = teamV;
                break;

            case Sides.Guest://Guest Won
                Team? teamH = gameRunning.PopNextTeam();
                if (teamH == null) return;
                gameRunning.HomeTeam = teamH;
                break;

            case Sides.Cancel:
                return;
        }
    }
    #endregion








    #region Context Methods

    private Game HandleContext(StartContext actionContext)
    {
        Game game = new();
        switch (actionContext)
        {
            case StartContext.Create:
                game = CreateGame();
                break;
            case StartContext.Load:
                game = GetGameFromDb();
                break;
            case StartContext.Cancel:
                isRunning = false;
                break;
        }
        return game;
    }

    public Game CreateGame()
    {
        GameDto? gamePackage = _view.GetGameInput(); // Get basic info

        if (gamePackage == null) { isRunning = false; return new(); }

        Game game = new Game(gamePackage);
        _data.GamesRepo.Add(game); // Adds the game to the repo
        Game game1 = _data.GamesRepo.Last() // Return the ref of the game in its repo
            ?? throw new InvalidOperationException("Failed to retrieve from the repository.");

        return game1;
    }

    public Game GetGameFromDb()
    {
        List<GameDto> dtoList = RepoToDto();
        int id = _view.GetGameId(dtoList);

        if (id < 0) { isRunning = false; return new(); }

        Game game = _data.GamesRepo.GetById(id) ?? throw new ArgumentNullException("The id returned a null game");

        return game;
    }
    #endregion








    #region Others


    private List<GameDto> RepoToDto()
    {
        return _data.GamesRepo.GetAll().Select(game => new GameDto(game)).ToList();
    }
    #endregion
}