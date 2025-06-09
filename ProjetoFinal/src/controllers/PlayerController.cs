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
    private PlayerView _view;

    private bool _saved {get{ return _data.PlayerRepo.Saved; }}

    private bool isRunning;

    // Constructor
    public PlayerController(PlayerView view, DataContext data)
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
        var playerDto = _view.GetPlayerInput() ?? throw new ArgumentNullException("Player input cannot be null.");
        var player = new Player(playerDto);
        _data.PlayerRepo.Add(player);
    }

    // Edits an existing player
    private void EditPlayer()
    {
        var players = _data.PlayerRepo.GetAll();

        if (players == null || !players.Any())
        {
            return;
        }

        // Convert players to DTOs for the view
        var playersDto = players.Select(p => new PlayerDto
        {
            Id = p.Id,
            Name = p.Name,
            Age = p.Age,
            Position = p.Position
        }).ToList();

        var playerId = _view.GetPlayerId(playersDto);

        Player playerInfo = _data.PlayerRepo.GetById(playerId) ?? throw new NullReferenceException("PlayerInfo Can't Be Null");

        var confirmation = _view.GetValidInput<bool>(
            "\nEdit? y/n: ",
            playerInfo.ToStringAlt(),
            true);

        if (confirmation)
        {
            var oldPlayerDto = new PlayerDto(playerInfo);
            var playerDto = _view.GetPlayerInput(oldPlayerDto) ?? throw new ArgumentNullException("Player input cannot be null.");
            playerDto.Id = playerId;
            var player = new Player(playerDto);

            var menu = _view.ComparerPlayers(oldPlayerDto, playerDto);

            bool commit = _view.GetValidInput<bool>(
                "Overwrite? y/n: ",
                menu,
                true
            );

            _data.PlayerRepo.UpdateById(playerId, player);
        }
    }

    // Deletes a player from the repository
    private void DeletePlayer()
    {
        var players = _data.PlayerRepo.GetAll();

        if (players == null || !players.Any())
        {
            return;
        }

        // Convert players to DTOs for the view
        var playersDto = players.Select(p => new PlayerDto
        {
            Id = p.Id,
            Name = p.Name,
            Age = p.Age,
            Position = p.Position
        }).ToList();

        var playerId = _view.GetPlayerId(playersDto);

        Player playerInfo = _data.PlayerRepo.GetById(playerId) ?? throw new NullReferenceException("PlayerInfo Can't Be Null");

        var confirmation = _view.GetValidInput<bool>(
            "\nDelete? y/n: ",
            playerInfo.ToStringAlt(),
            true);

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
        Console.Clear(); // Yeah I know ⚠️
        string _;
        var success = _view.DisplayPlayerList(playersDto, out _);
        Console.ReadLine(); // Yeah I know ⚠️
    }

    // Saves changes to the database
    private void SaveChanges()
    {
        _data.PlayerRepo.WriteToDataBase();
    }

}