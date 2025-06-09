using System.Drawing;
using System.Dynamic;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using Container.DTOs;
using Controller;
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

    private TeamEnumRL _side = TeamEnumRL.Unset;
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

    public List<Player> Jogadores
    { get { return _jogadores ?? new List<Player>(); } }

    public List<Event> EventsHistory
    { get { return _eventsHistory; } set { _eventsHistory = value; } }

    public int XP
    { get { return _xp; } set { _xp = value; } }

    public DateOnly CreationDate
    { get { return _creationDate; } set { _creationDate = value; } }

    public List<int> PlayersId { get { return _playersId; } set { _playersId = value ?? new List<int>(); } }
    
    [JsonIgnore]
    public TeamEnumRL Side {get{ return _side; } set{ _side = value; }}

    // public int CountJogadores
    //     {get {return _jogadores?.Count ?? 0;}}

    //     public List<Player> Goleiros
    //         {get {return _goleiros ?? new List<Player>();}}
    //     // public int CountGoleiros
    //     //     {get {return _goleiros?.Count ?? 0;}}

    //     public List<Player> Defesas
    //         {get {return _defesas ?? new List<Player>();}}
    //     // public int CountDefesas
    //     //     {get {return _defesas?.Count ?? 0;}}

    //     public List<Player> Atacantes
    //         {get {return _atacantes ?? new List<Player>();}}
    //     // public int CountAtacantes
    //     //     {get {return _atacantes?.Count ?? 0;}}

    //     public List<Player> Unknown
    //         {get {return _unknown ?? new List<Player>();}}
    // // public int CountUnknown
    //     {get {return _unknown?.Count ?? 0;}}

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
        this._jogadores = package.Players.Select(playerDto => new Player(playerDto)).ToList();
        this._eventsHistory = package.EventsHistory;
        this._xp = package.XP;
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

    // public void RemoveJogador(Player jogador)
    // {
    //     if (_jogadores == null)
    //         {return;}

    //     _jogadores?.Remove(jogador);

    //     switch (jogador.Position)
    //     {
    //         case 0:
    //             _unknown?.Remove(jogador);
    //             break;
    //         case 1:
    //             _goleiros?.Remove(jogador);
    //             break;
    //         case 2:
    //             _defesas?.Remove(jogador);
    //             break;
    //         case 3:
    //             _atacantes?.Remove(jogador);
    //             break;
    //         default:
    //             throw new ArgumentOutOfRangeException("Posição inválida.");
    //     }
    // }


    // private void SortJogadores(List<Player> jogadores)
    // {
    //     foreach (var jogador in jogadores)
    //         {SortJogador(jogador);}
    // }


    // private void SortJogador(Player jogador)
    // {
    //     switch (jogador.Position)
    //     {
    //         case 0:
    //             if (_unknown == null)
    //                 {_unknown = new List<Player>();}
    //             _unknown.Add(jogador);
    //             break;
    //         case 1:
    //             if (_goleiros == null)
    //                 {_goleiros = new List<Player>();}
    //             _goleiros.Add(jogador);
    //             break;
    //         case 2:
    //             if (_defesas == null)
    //                 {_defesas = new List<Player>();}
    //             _defesas.Add(jogador);
    //             break;
    //         case 3:
    //             if (_atacantes == null)
    //                 {_atacantes = new List<Player>();}
    //             _atacantes.Add(jogador);
    //             break;
    //         default:
    //             throw new ArgumentOutOfRangeException("Posição inválida.");
    //     }
    // }


    #region unused

    // public void AddGoleiro(Jogador goleiro)
    // {
    //     if (_goleiros == null)
    //         {_goleiros = new List<Jogador>();}
    //     _goleiros?.Add(goleiro);
    // }

    // public void AddDefesa(Jogador defesa)
    // {
    //     if (_defesas == null)
    //         {_defesas = new List<Jogador>();}
    //     _defesas?.Add(defesa);
    // }

    // public void AddAtacante(Jogador atacante)
    // {
    //     if (_atacantes == null)
    //         {_atacantes = new List<Jogador>();}
    //     _atacantes?.Add(atacante);
    // }

    #endregion


}


                