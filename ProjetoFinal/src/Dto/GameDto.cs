namespace DTOs;

public class GameDto
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public string Local { get; set; } = string.Empty;
    public string TipoDeCampo { get; set; } = string.Empty;
    public int QuantidadeJogadoresPorTeam { get; set; }
    // public int LimiteTeams { get; set; }
    public List<int>? JogadorIdsSemTeam { get; set; } = new();
    public List<int>? TeamIds { get; set; } = new();

    public override string ToString()
    {
        return $@"""
Id: {Id}
Date: {Date}
HoraInicio: {HoraInicio}
Local: {Local}
TipoDeCampo: {TipoDeCampo}
QuantidadeJogadoresPorTeam: {QuantidadeJogadoresPorTeam}
JogadorIdsSemTeam: [{string.Join(", ", JogadorIdsSemTeam ?? new List<int>())}]
TeamIds: [{string.Join(", ", TeamIds ?? new List<int>())}]
""";
    }
}