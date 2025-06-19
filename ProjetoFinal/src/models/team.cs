using System.Drawing;
using System.Dynamic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Container.DTOs;
using Controller;
using MyRepository;
using Templates;

namespace Models;

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


public class Team : ModelAbstract
{

    #region Private Attributes

    private GameController.Sides _side = GameController.Sides.Cancel;
    private string _name = string.Empty;
    private List<Player> _jogadores = new(); //Prep to be deleted
    private List<int> _playersId = new();
    private List<Event> _eventsHistory = new();
    private int _xp;
    private DateOnly _creationDate;


    #endregion

    #region Public Attributes

    public string Name
    { get { return _name; } set { _name = value; } }
    public List<Event> EventsHistory
    { get { return _eventsHistory; } set { _eventsHistory = value; } }
    public DateOnly CreationDate
    { get { return _creationDate; } set { _creationDate = value; } }
    public List<int> PlayersId { get { return _playersId; } set { _playersId = value ?? new List<int>(); } }
    
    [JsonIgnore]
    public List<Player> Jogadores
    { get { return _jogadores ?? new List<Player>(); } }
    [JsonIgnore]
    public GameController.Sides Side { get { return _side; } set { _side = value; } }
    [JsonIgnore]
    public int XP
    { get { return _xp; } set { _xp = value; } }

    #endregion


    // Constructor

    public Team() { }

    public Team(string nome, List<Player> jogadores)
    {

        _name = nome;

        _jogadores = jogadores ?? new();

        // SortJogadores(jogadores);
    }

    public Team(TeamDto package)
    {
        this._id = package.Id;
        this._name = package.Name;
        this._eventsHistory = package.EventsHistory;
        this._xp = package.XP;
        this._playersId = package.IdList;
    }

    public Team(int id)
    {
        _id = id;
    }

    // Methods

    public void AddJogador(Player jogador)
    {
        if (_jogadores == null) _jogadores = new();

        _jogadores.Add(jogador);

        // SortJogador(jogador);
    }

    public bool RemoveJogadorById(int Id)
    {
        var index = _jogadores.FindIndex(j => j.Id == Id);
        if (index < 0) return false;
        _jogadores.RemoveAt(index);
        return true;

    }

    public TeamDto ToDto()
    {
        return new TeamDto(this);
    }

    public TeamDto toDto(PlayersRepo players)
    {
        var dto = new TeamDto(this);
        dto.Players = MapperTools.MapPlayersByIds(this._playersId, players)
            .Select(p => new PlayerDto(p)).ToList();
        return dto;
    }


}


                