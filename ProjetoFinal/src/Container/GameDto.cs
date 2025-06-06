using jogo;
using Lib.TeamFormation;
using Model;

namespace Container.DTOs;

public class GameDto
{
    public string Title { get; set; } = string.Empty;

    public int Id { get; set; }

    public int HomeScore { get; set; }
    public int AdversaryScore { get; set; }

    public TeamDto HomeTeam { get; set; }
    public TeamDto AdversaryTeam { get; set; }

    public DateOnly Date { get; set; }
    public TimeOnly HoraInicio { get; set; }

    public string Local { get; set; } = string.Empty;
    public string TipoDeCampo { get; set; } = string.Empty;

    public List<PlayerDto> FilaJogadoresSemTeam { get; set; } = new();
    public List<TeamDto> TeamsToPlay { get; set; } = new();

    public List<Event> Events { get; set; } = new();
    public TeamFormation TeamFormation { get; set; }


    public GameDto()
    {
        HomeTeam = new TeamDto();
        AdversaryTeam = new TeamDto();
        TeamFormation = new TeamFormation();
    }

    public GameDto(Game game)
    {
        Id = game.Id;

        Title = game.Title;

        HomeScore = game.HomeScore;
        AdversaryScore = game.AdversaryScore;

        HomeTeam = new(game.HomeTeam);
        AdversaryTeam = new(game.AdversaryTeam);

        Date = game.Date;
        HoraInicio = game.HoraInicio;

        Local = game.Local;
        TipoDeCampo = game.TipoDeCampo;

        FilaJogadoresSemTeam = game.FilaJogadoresSemTeam?.Select(p => new PlayerDto(p)).ToList()
            ?? throw new ArgumentNullException(nameof(game.FilaJogadoresSemTeam), "FilaJogadoresSemTeam cannot be null");
        TeamsToPlay = game.TeamsToPlay?.Select(t => new TeamDto(t)).ToList()
            ?? throw new ArgumentNullException(nameof(game.TeamsToPlay), "TeamsToPlay cannot be null");

        Events = game.Events?.Select(e => e).ToList()
            ?? throw new ArgumentNullException(nameof(game.Events), "Events cannot be null");
        TeamFormation = game.TeamFormation
            ?? throw new ArgumentNullException(nameof(game.TeamFormation), "TeamFormation cannot be null");
    }

    public override string ToString()
    {
        return $"Id: {Id}, Title: {Title}, Date: {Date}, Start: {HoraInicio}, Local: {Local}, TipoDeCampo: {TipoDeCampo}, HomeScore: {HomeScore}, AdversaryScore: {AdversaryScore})";
    }

}