using Container.DTOs;
using Templates;
using Lib.TeamFormation;
using System.Text.Json.Serialization;

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
    private List<Team> _teamsLineUp = new List<Team>();

    private List<Event> events = new();
    private TeamFormation _teamFormation = new();
    private bool _initialized = false;
    #endregion

    #region Public Attributes

    public string Title
    { get { return _title; } set { _title = value ?? string.Empty; } }
    [JsonIgnore]
    public int HomeScore
    { get { return _homeScore; } set { _homeScore = value; } }
    [JsonIgnore]
    public int AdversaryScore
    { get { return _adversaryScore; } set { _adversaryScore = value; } }

    public Team HomeTeam
    { get { return _homeTeam; } set { _homeTeam = value ?? new Team(); } }
    public Team GuestTeam
    { get { return _adversaryTeam; } set { _adversaryTeam = value ?? new Team(); } }

    public DateOnly Date
    { get { return _date; } set { _date = value; } }
    public TimeOnly HoraInicio
    { get { return _horaInicio; } set { _horaInicio = value; } }

    public string Local
    { get { return _local; } set { _local = value ?? string.Empty; } }
    public string TipoDeCampo
    { get { return _tipoDeCampo; } set { _tipoDeCampo = value ?? string.Empty; } }

    public List<Player> PlayersLineUp
    { get { return _filaJogadoresSemTeam; } set { _filaJogadoresSemTeam = value; } }
    public List<Team> TeamsLineUp
    { get { return _teamsLineUp; } set { _teamsLineUp = value; } }

    public List<Event> Events
    { get { return events; } set { events = value; } }
    public TeamFormation TeamFormation
    { get { return _teamFormation; } set { _teamFormation = value; } }

    public bool Initialized => _initialized;

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
        _adversaryTeam = package.GuestTeam != null ? new Team(package.GuestTeam) : new Team();
        _date = package.Date;
        _horaInicio = package.HoraInicio;
        _local = package.Local ?? string.Empty;
        _tipoDeCampo = package.TipoDeCampo ?? string.Empty;

        _filaJogadoresSemTeam = package.FilaJogadoresSemTeam?.Select(p => new Player(p)).ToList() ?? new List<Player>();
        _teamsLineUp = package.TeamsToPlay?.Select(t => new Team(t)).ToList() ?? new List<Team>();
        events = package.Events?.Select(e => new Event(e)).ToList() ?? new List<Event>();
        _teamFormation = package.TeamFormation != null ? new TeamFormation(package.TeamFormation) : new TeamFormation();
    }

    public Game(int id)
    {
        _id = id;
    }

    public Game() { }

    // Methods

    public void AddPlayerToLineUp(Player jogador)
    {
        // if (_filaJogadoresSemTeam == null)
        // { _filaJogadoresSemTeam = new List<Player>(); }
        _filaJogadoresSemTeam.Add(jogador);
    }

    public void AddPlayersToLineUp(List<Player> players)
    {
        _filaJogadoresSemTeam.AddRange(players);
    }

    public void AddTeamToLineUp(Team team)
    {
        _teamsLineUp.Add(team);
    }
    public void AddTeamsToLineUp(List<Team> teams)
    {
        _teamsLineUp.AddRange(teams);
    }

    public GameDto ToDto()
    {
        return new GameDto(this);
    }

    public Team? PopNextTeam()
    {
        if (_teamsLineUp.Count == 0)
        {
            return null;
        }

        Team nextTeam = _teamsLineUp[0];
        _teamsLineUp.RemoveAt(0);
        return nextTeam;
    }

    public bool initializeTeams()
    {
        if (_initialized) return false;
        if (_teamsLineUp.Count < 2) return false;

        HomeTeam = PopNextTeam()!;
        GuestTeam = PopNextTeam()!;
        _initialized = true;

        return true;
    }

    public List<int> GetAllUniquePlayerIds()
    {
        HashSet<int> playerIds = new HashSet<int>();

        foreach (var player in _filaJogadoresSemTeam)
        {
            playerIds.Add(player.Id);
        }

        foreach (var team in _teamsLineUp)
        {
            foreach (var id in team.PlayersId)
            {
                playerIds.Add(id);
            }
        }

        foreach (var id in HomeTeam.PlayersId)
        {
            playerIds.Add(id);
        }

        foreach (var id in GuestTeam.PlayersId)
        {
            playerIds.Add(id);
        }

        return playerIds.ToList();
    }

    // public void SetHome()
    // {

    // }

    // public void SwitchDefeated(Controller.GameController.TeamEnumRL winner)
    // {
    //     if (HomeTeam.Side != winner)
    //     {
    //         HomeTeam = TeamsLineUp[0];
    //     }
    // }

}