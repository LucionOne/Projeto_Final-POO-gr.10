using Container.DTOs;
using Templates;
using Lib.TeamFormation;

namespace Models;

// CRUD dos Jogos;
// Os atributos principais são: a data do jogo, o local, o tipo de campo e quantos jogadores por Team incluindo o goleiro;
// Adicionar um atributo opcional que limita a quantidade de Teams e/ou de jogadores;
// A quantidade mínima para confirmar a partida são pelo menos dois Teams completos.
// Lista de Interessados para registrar quem pretende ir ao próximo jogo



// Criar pelo menos 03 formas de gerar os Teams
// Por ordem de chegada no local do jogo,
// Por exemplo: se for um jogo de futsal, os 04 primeiros ficam no primeiro Team e os 04 seguintes ficam no segundo Team.
// Exceto os goleiros: 01 para cada Team
// Se não houver goleiros suficiente, fica o próximo da lista
// Por ordem de posição, tentando deixar os Teams mais equilibrados:
// Um goleiro para cada Team
// Quantidade de jogadores de defesa e ataques equilibrados entre os Teams, quando possível
// Algum outro critério desenvolvido pelo grupo
// Usem a criatividade
// Podem adicionar outros atributos para atender este requisito
// Validar com o Professor antes de implementar
// Os Teams devem ser criados à medida que as partidas vão acontecendo, ou seja, cria-se os dois primeiros Teams, e o terceiro será gerado após o término do primeiro jogo.
// Se não houver jogadores suficientes “fora”, pode usar os jogadores do Team derrotado para completar.


public class Game : ModelAbstract
{

    #region Constant Attributes

    public const int TeamsMínimos = 2;

    #endregion

    #region Private Attributes

    private string _title = string.Empty;

    private int _homeScore;
    private int _adversaryScore;
    private Team _homeTeam = new Team();
    private Team _adversaryTeam = new Team();

    private DateOnly _date;
    private TimeOnly _horaInicio;

    private string _local = string.Empty;
    private string _tipoDeCampo = string.Empty;

    private List<Player> _filaJogadoresSemTeam = new List<Player>();
    private List<Team> _teamsToPlay = new List<Team>();

    private List<Event> events = new();
    private TeamFormation _teamFormation = new();

    #endregion

    #region Public Attributes

    public string Title
    { get { return _title; } set { _title = value ?? string.Empty; } }

    public int HomeScore
    { get { return _homeScore; } set { _homeScore = value; } }
    public int AdversaryScore
    { get { return _adversaryScore; } set { _adversaryScore = value; } }

    public Team HomeTeam
    { get { return _homeTeam; } set { _homeTeam = value ?? new Team(); } }
    public Team AdversaryTeam
    { get { return _adversaryTeam; } set { _adversaryTeam = value ?? new Team(); } }

    public DateOnly Date
    { get { return _date; } set { _date = value; } }
    public TimeOnly HoraInicio
    { get { return _horaInicio; } set { _horaInicio = value; } }

    public string Local
    { get { return _local; } set { _local = value ?? string.Empty; } }
    public string TipoDeCampo
    { get { return _tipoDeCampo; } set { _tipoDeCampo = value ?? string.Empty; } }

    public List<Player> FilaJogadoresSemTeam
    { get { return _filaJogadoresSemTeam; } set { _filaJogadoresSemTeam = value; } }
    public List<Team> TeamsToPlay
    { get { return _teamsToPlay; } set { _teamsToPlay = value; } }

    public List<Event> Events
    { get { return events; } set { events = value; } }
    public TeamFormation TeamFormation
    { get { return _teamFormation; } set { _teamFormation = value; } }

    #endregion

    // Constructor

    public Game(GameDto package)
    {
        if (package == null) return;

        _id = package.Id;
        _title = package.Title ?? string.Empty;
        _homeScore = package.HomeScore;
        _adversaryScore = package.AdversaryScore;
        _homeTeam = package.HomeTeam != null ? new Team(package.HomeTeam) : new Team();
        _adversaryTeam = package.AdversaryTeam != null ? new Team(package.AdversaryTeam) : new Team();
        _date = package.Date;
        _horaInicio = package.HoraInicio;
        _local = package.Local ?? string.Empty;
        _tipoDeCampo = package.TipoDeCampo ?? string.Empty;

        _filaJogadoresSemTeam = package.FilaJogadoresSemTeam?.Select(p => new Player(p)).ToList() ?? new List<Player>();
        _teamsToPlay = package.TeamsToPlay?.Select(t => new Team(t)).ToList() ?? new List<Team>();
        events = package.Events?.Select(e => new Event(e)).ToList() ?? new List<Event>();
        _teamFormation = package.TeamFormation != null ? new TeamFormation(package.TeamFormation) : new TeamFormation();
    }

    public Game(int id)
    {
        _id = id;
    }


    public Game() { }

    // public Game(DateOnly date, TimeOnly horaInicio, string local, string tipoDeCampo, int quantidadeJogadoresPorTeam, int limiteJogadores, int limiteTeams)
    // {
    //     _date = date;
    //     _horaInicio = horaInicio;
    //     _local = local ?? string.Empty;
    //     _tipoDeCampo = tipoDeCampo ?? string.Empty;
    //     _quantidadeJogadoresPorTeam = quantidadeJogadoresPorTeam;
    //     // _limiteJogadores = limiteJogadores;
    //     _limiteTeams = limiteTeams;
    // }


    // Methods

    public void AddJogadorSemTeam(Player jogador)
    {
        if (_filaJogadoresSemTeam == null)
        { _filaJogadoresSemTeam = new List<Player>(); }
        _filaJogadoresSemTeam.Add(jogador);
    }

    public void AddTeamInLine(Team team)
    {
        _teamsToPlay.Add(team);
    }


    public GameDto ToDto()
    {
        return new GameDto(this);
    }

    public void SetHome()
    {

    }

    public void SwitchDefeated(Controller.TeamEnumRL winner)
    {
        if (HomeTeam.Side != winner)
        {
            HomeTeam = TeamsToPlay[0];
        }
    }


}