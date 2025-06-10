using Context;
using Models;
using Templates;
using View;
using Container.DTOs;
using System.ComponentModel.Design;

namespace Controller;

public class PlayerController
{
    // Fields for data context and view
    private DataContext _data;
    private IPlayerView _view;

    private bool _saved {get{ return _data.PlayerRepo.Saved; }}

    private bool isRunning;

    // Constructor
    public PlayerController(IPlayerView view, DataContext data)
    {
        _view = view;
        _data = data;
    }

    // Main interaction loop
    public void BeginInteraction()
    {
        isRunning = true;
        while (isRunning)
        {
            int input = _view.MainMenu(_saved);
            HandleUserChoice(input);
        }
    }

    // Handles user menu choices
    private void HandleUserChoice(int choice)
    {
        switch (choice)
        {
            case 0:
                isRunning = _view.Bye(_saved);
                break;
            case 1:
                CreatePlayer();
                break;
            case 2:
                EditPlayer();
                break;
            case 3:
                DeletePlayer();
                break;
            case 4:
                ListPlayers();
                break;
            case 5:
                SaveChanges();
                break;
            default:
                throw new ArgumentOutOfRangeException("Invalid choice. Please select a valid option.");
        }
    }

    // Creates a new player and adds to repository
    private void CreatePlayer()
    {
        PlayerDto? playerDto = _view.GetPlayerInput();
        if (playerDto == null) { return; }
        bool confirmation = _view.ConfirmPlayerAdd(playerDto);
        if (!confirmation) { return; }
        var player = new Player(playerDto);
        _data.PlayerRepo.Add(player);
    }

    // Edits an existing player
    private void EditPlayer()
    {
        var players = _data.PlayerRepo.GetAll();

        var playersDto = players.Select(p => new PlayerDto
        {
            Id = p.Id,
            Name = p.Name,
            Age = p.Age,
            Position = p.Position
        }).ToList();

        var playerId = _view.GetPlayerId(playersDto);

        if (playerId < 0) { return; }

        Player oldPlayer = _data.PlayerRepo.GetById(playerId) ?? throw new NullReferenceException("PlayerInfo Can't Be Null");

        var oldPlayerDto = new PlayerDto(oldPlayer);

        PlayerDto? newPlayerDto = _view.GetPlayerEdit(oldPlayerDto);

        if (newPlayerDto == null) { return; }

        // playerDto.Id = playerId;

        var confirmation = _view.ConfirmPlayerEdit(oldPlayerDto, newPlayerDto);

        if (confirmation)
        {
            var player = new Player(newPlayerDto);
            _data.PlayerRepo.UpdateById(playerId, player);
        }
        else
        {
            return;
        }
    }

    // Deletes a player from the repository
    private void DeletePlayer()
    {
        var players = _data.PlayerRepo.GetAll();

        var playersDto = players.Select(p => new PlayerDto
        {
            Id = p.Id,
            Name = p.Name,
            Age = p.Age,
            Position = p.Position
        }).ToList();

        var playerId = _view.GetPlayerId(playersDto);

        Player playerInfo = _data.PlayerRepo.GetById(playerId) ?? throw new NullReferenceException("PlayerInfo Can't Be Null");

        bool confirmation = _view.ConfirmPlayerDelete(new PlayerDto(playerInfo));

        if (confirmation) _data.PlayerRepo.RemoveAt(playerId);
    }

    // Lists all players using the view
    private void ListPlayers()
    {
        var players = _data.PlayerRepo.GetAll();
        var playersDto = players.Select(p => new PlayerDto
        {
            Id = p.Id,
            Name = p.Name,
            Age = p.Age,
            Position = p.Position
        }).ToList();

        _view.ShowPlayers(playersDto);

    }

    // Saves changes to the database
    private void SaveChanges()
    {
        bool confirmation = _view.ConfirmSaveToDatabase(_saved);
        if (confirmation) _data.PlayerRepo.WriteToDataBase();
        else return;
    }

}