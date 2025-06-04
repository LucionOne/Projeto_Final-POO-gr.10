using team;
using Model;

namespace Container.DTOs;

public class TeamDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<PlayerDto> Players { get; set; } = new();
    public List<Event> EventsHistory { get; set; } = new();

    public TeamDto() { }

    public TeamDto(Team team)
    {
        Id = team.Id;
        Name = team.Name;
        Players = team.Jogadores.Select(p => new PlayerDto(p)).ToList();
        EventsHistory = team.EventsHistory.Select(e => e).ToList(); // Assuming Event is serializable
    }
}