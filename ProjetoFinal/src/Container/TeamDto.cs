using Controller;
using Models;

namespace Container.DTOs;

public class TeamDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<PlayerDto> Players { get; set; } = new();
    public List<Event> EventsHistory { get; set; } = new();
    public int XP { get; set; }
    public DateOnly date = new();
    public TeamEnumRL Side = TeamEnumRL.Unset;

    public TeamDto() { }

    public TeamDto(Team team)
    {
        Id = team.Id;
        Name = team.Name;
        Players = team.Jogadores.Select(p => p.ToDto()).ToList();
        EventsHistory = team.EventsHistory;
        XP = team.XP;
        date = team.CreationDate;
        Side = team.Side;
    }
    public TeamDto(Team team, List<PlayerDto> players)
    {
        Id = team.Id;
        Name = team.Name;
        Players = players;
        EventsHistory = team.EventsHistory;
        XP = team.XP;
        date = team.CreationDate;
        Side = team.Side;
    }
}