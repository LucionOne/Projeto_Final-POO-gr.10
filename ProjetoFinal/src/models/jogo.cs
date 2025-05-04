using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace jogo;

public class Jogo
{
    private int _id;
    private DateTime _data;
    private string? _local;
    private string? _tipoCampo;
    private int _jogadoresPorTime;
    private int? _limiteDeTimes;

    public int Id { get => _id; set => _id = value; }
    public DateTime Data { get => _data; set => _data = value; }
    public string Local { get => _local ?? ""; set => _local = value ?? ""; }
    public string TipoCampo { get => _tipoCampo ?? ""; set => _tipoCampo = value ?? ""; }
    public int JogadoresPorTime { get => _jogadoresPorTime; set => _jogadoresPorTime = value; }
    public int? LimiteDeTimes { get => _limiteDeTimes; set => _limiteDeTimes = value; }

    // Lista de interessados no jogo
    public List<int> InteressadosIds { get; set; } = new List<int>();

    [JsonIgnore]
    public int TotalInteressados => InteressadosIds.Count;

    [JsonIgnore]
    public bool JogoConfirmado => LimiteDeTimes.HasValue
        ? TotalInteressados >= LimiteDeTimes.Value * JogadoresPorTime
        : TotalInteressados >= 2 * JogadoresPorTime;

    public string GetInfo()
    {
        return $"Jogo #{Id} - Data: {Data.ToShortDateString()}, Local: {Local}, Campo: {TipoCampo}, Jogadores/Time: {JogadoresPorTime}, Confirmado: {(JogoConfirmado ? "Sim" : "Não")}";
    }
}
