
using jogador;

namespace jogo;


public class Jogo
{
    //private
    private DateOnly _date;
    private string? _local;
    private string? _tipoDeCampo;
    private int _quantidadeJogadoresPorTime;
    private int _limiteJogadores = 12;
    private int _limiteTimes = 2;
    private List<Jogador>? _jogadoresInteressados = new List<Jogador>();
    private int _timesMínimos = 2;
    private int _id;

    //public 
    public DateOnly Date {get {return _date;} set {_date = value;}}
    public string? Local {get {return _local;} set {_local = value;}}
    public string? TipoDeCampo {get {return _tipoDeCampo;} set {_local = _tipoDeCampo;}}
    public int QuantidadeJogadoresPorTime {get {return _quantidadeJogadoresPorTime;} set {_quantidadeJogadoresPorTime = value;}} 
    public int LimiteJogadores {get {return _limiteJogadores;} set {_limiteJogadores = value;}}
    public int LimiteTimes {get {return _limiteTimes;} set {_limiteTimes = value;}}
    public List<Jogador>? JogadoresInteressados {get {return _jogadoresInteressados;} set {_jogadoresInteressados = value;}}
    public int Id {get {return _id;} set {_id = value;}}
    




















}