using Context;
using jogador;
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

    // Constructor
    public PlayerController(DataContext data, PlayerView view)
    {
        _data = data;
        _view = view;
    }

    // Main interaction loop
    public void BeginInteraction()
    {
        bool isRunning = true;
        while (isRunning)
        {
            int input = _view.MainMenu();
            isRunning = HandleUserChoice(input);
        }
    }

    // Handles user menu choices
    private bool HandleUserChoice(int choice)
    {
        switch (choice)
        {
            case 0:
                return false; // Exit
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
        }
        return true;
    }

    // Creates a new player and adds to repository
    private void CreatePlayer()
    {
        var playerDto = _view.GetPlayerInput() ?? throw new ArgumentNullException("Player input cannot be null.");
        var player = new Player(playerDto);
        _data.JogadorRepo.Add(player);
    }

    // Edits an existing player
    private void EditPlayer()
    {
        var players = _data.JogadorRepo.GetAll();

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

        Player playerInfo = _data.JogadorRepo.GetById(playerId) ?? throw new NullReferenceException("PlayerInfo Can't Be Null");

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

            _data.JogadorRepo.UpdateById(playerId, player);
        }
    }

    // Deletes a player from the repository
    private void DeletePlayer()
    {
        var players = _data.JogadorRepo.GetAll();

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

        Player playerInfo = _data.JogadorRepo.GetById(playerId) ?? throw new NullReferenceException("PlayerInfo Can't Be Null");

        var confirmation = _view.GetValidInput<bool>(
            "\nDelete? y/n: ",
            playerInfo.ToStringAlt(),
            true);

        if (confirmation) _data.JogadorRepo.RemoveAt(playerId);
    }

    // Lists all players using the view
    private void ListPlayers()
    {
        var players = _data.JogadorRepo.GetAll();
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
        _data.JogadorRepo.WriteToDataBase();
    }

}