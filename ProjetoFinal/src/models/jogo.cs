using time;
using jogador;

namespace jogo;

// CRUD dos Jogos;
// Os atributos principais são: a data do jogo, o local, o tipo de campo e quantos jogadores por time incluindo o goleiro;
// Adicionar um atributo opcional que limita a quantidade de times e/ou de jogadores;
// A quantidade mínima para confirmar a partida são pelo menos dois times completos.
// Lista de Interessados para registrar quem pretende ir ao próximo jogo



// Criar pelo menos 03 formas de gerar os times
// Por ordem de chegada no local do jogo,
// Por exemplo: se for um jogo de futsal, os 04 primeiros ficam no primeiro time e os 04 seguintes ficam no segundo time.
// Exceto os goleiros: 01 para cada time
// Se não houver goleiros suficiente, fica o próximo da lista
// Por ordem de posição, tentando deixar os times mais equilibrados:
// Um goleiro para cada time
// Quantidade de jogadores de defesa e ataques equilibrados entre os times, quando possível
// Algum outro critério desenvolvido pelo grupo
// Usem a criatividade
// Podem adicionar outros atributos para atender este requisito
// Validar com o Professor antes de implementar
// Os times devem ser criados à medida que as partidas vão acontecendo, ou seja, cria-se os dois primeiros times, e o terceiro será gerado após o término do primeiro jogo.
// Se não houver jogadores suficientes “fora”, pode usar os jogadores do time derrotado para completar.


public class Jogo
{
    // Update

    // Const
    public const int TimesMínimos = 2;

    // private 
    private DateOnly _date;
    private TimeOnly _horaInicio;
    private string _local = string.Empty;
    private string _tipoDeCampo = string.Empty;
    private int _quantidadeJogadoresPorTime;
    private int _limiteJogadores;
    private int _limiteTimes;
    private int _id;
    private List<Jogador>? _filaJogadoresSemTime = new List<Jogador>();
    // private List<Jogador>? FilaGoleiros = new List<Jogador>();
    private List<Time>? _times = new List<Time>();

    // public 
    public DateOnly Date 
        {get {return _date;} set {_date = value;}}
    public TimeOnly HoraInicio 
        {get {return _horaInicio;} set {_horaInicio = value;}}
    public string Local 
        {get {return _local;} set {_local = value ?? string.Empty;}}
    public string TipoDeCampo 
        {get {return _tipoDeCampo;} set {_tipoDeCampo = value ?? string.Empty;}}
    public int QuantidadeJogadoresPorTime 
        {get {return _quantidadeJogadoresPorTime;} set {_quantidadeJogadoresPorTime = value;}} 
    public int LimiteJogadores 
        {get {return _limiteJogadores;} set {_limiteJogadores = value;}}
    public int LimiteTimes 
        {get {return _limiteTimes;} set {_limiteTimes = value;}}
    public int Id 
        {get {return _id;} set {_id = value;}}
    public List<Jogador>? FilaJogadoresSemTime 
        {get {return _filaJogadoresSemTime;} set {_filaJogadoresSemTime = value;}}
    public List<Time>? Times 
        {get {return _times;} set {_times = value;}}

    // Constructor

    public Jogo(DateOnly date, TimeOnly horaInicio, string local, string tipoDeCampo, int quantidadeJogadoresPorTime, int limiteJogadores, int limiteTimes)
    {
        _date = date;
        _horaInicio = horaInicio;
        _local = local ?? string.Empty;
        _tipoDeCampo = tipoDeCampo ?? string.Empty;
        _quantidadeJogadoresPorTime = quantidadeJogadoresPorTime;
        _limiteJogadores = limiteJogadores;
        _limiteTimes = limiteTimes;
    }

    // Methods

    public void VerificarJogadoresSTime()
    {

    }

















}