using jogo;
namespace Container.DTOs;

public class GameDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;   

    public int HomeGoals { get; set; }
    public int AdversaryGoals { get; set; }
    public int HomeTeamId { get; set; }
    public int AdversaryTeamId { get; set; }

    public DateOnly Date { get; set; }
    public TimeOnly HoraInicio { get; set; }

    public string Local { get; set; } = string.Empty;
    public string TipoDeCampo { get; set; } = string.Empty;

    public int QuantidadeJogadoresPorTeam { get; set; }
    // public int LimiteTeams { get; set; }
    // public List<int>? JogadorIdsSemTeam { get; set; } = new();
    // public List<int>? TeamIds { get; set; } = new();

    public GameDto() { }

    public GameDto(Game game)
    {
        Id = game.Id;
        Title = game.Title;

        Date = game.Date;
        HoraInicio = game.HoraInicio;

        Local = game.Local;
        TipoDeCampo = game.TipoDeCampo;

        QuantidadeJogadoresPorTeam = game.QuantidadeJogadoresPorTeam;

        HomeGoals = game.HomeGoals;
        AdversaryGoals = game.AdversaryGoals;
        HomeTeamId = game.HomeTeam.Id;
        AdversaryTeamId = game.AdversaryTeam.Id;
    }

    public override string ToString()
    {
        List<string> str = new List<string>
        {
            $"Id: {Id}",
            $"Date: {Date}",
            $"HoraInicio: {HoraInicio}",
            $"Local: {Local}",
            $"TipoDeCampo: {TipoDeCampo}",
            $"QuantidadeJogadoresPorTeam: {QuantidadeJogadoresPorTeam}",
            // $"JogadorIdsSemTeam: [{string.Join(", ", JogadorIdsSemTeam ?? new List<int>())}]",
            // $"TeamIds: [{string.Join(", ", TeamIds ?? new List<int>())}]",
        };

        return string.Join("\n", str);
    }
}