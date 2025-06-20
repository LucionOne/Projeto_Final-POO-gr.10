using Models;

namespace Container.DTOs;

public class TeamDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<PlayerDto> Players { get; set; } = new();
    public List<Event> EventsHistory { get; set; } = new();
    public int XP { get; set; }
    public DateOnly Date = new();
    public Controller.GameController.Sides Side = Controller.GameController.Sides.Cancel;
    public List<int> IdList = new();

    public TeamDto() { }

    public TeamDto(Team team)
    {
        Id = team.Id;
        Name = team.Name;
        Players = team.Jogadores.Select(p => p.ToDto()).ToList();
        EventsHistory = team.EventsHistory;
        XP = team.XP;
        Date = team.CreationDate;
        Side = team.Side;
        IdList = team.PlayersId;
    }
    public TeamDto(Team team, List<PlayerDto> players)
    {
        Id = team.Id;
        Name = team.Name;
        Players = players;
        EventsHistory = team.EventsHistory;
        XP = team.XP;
        Date = team.CreationDate;
        Side = team.Side;
        IdList = team.PlayersId;
    }
    public TeamDto(string name, DateOnly date, List<int> ids)
    {
        this.Name = name;
        this.Date = date;
        this.IdList = ids;
    }
    public TeamDto(int id, string name, DateOnly date, List<int> ids)
    {
        this.Name = name;
        this.Date = date;
        this.IdList = ids;
    }

    
}