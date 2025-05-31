using jogo;
namespace Container.DTOs;

public class GameDto
{
    public int HomeGoals { get; set; }
    public int AdversaryGoals { get; set; }
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public string Local { get; set; } = string.Empty;
    public string TipoDeCampo { get; set; } = string.Empty;
    public int QuantidadeJogadoresPorTeam { get; set; }
    // public int LimiteTeams { get; set; }
    // public List<int>? JogadorIdsSemTeam { get; set; } = new();
    // public List<int>? TeamIds { get; set; } = new();

    public GameDto() {}

    public GameDto(Game game)
    {
        Id = game.Id;
        Date = game.Date;
        HoraInicio = game.HoraInicio;
        Local = game.Local;
        TipoDeCampo = game.TipoDeCampo;
        QuantidadeJogadoresPorTeam = game.QuantidadeJogadoresPorTeam;
        HomeGoals = game.HomeGoals;
        AdversaryGoals = game.AdversaryGoals;
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