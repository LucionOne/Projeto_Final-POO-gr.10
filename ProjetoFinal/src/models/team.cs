using jogador;
using Templates;

namespace team;

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

        private string _nome = string.Empty;
        private List<Player>? _jogadores = new List<Player>();
        private List<Player>? _goleiros = new List<Player>();
        private List<Player>? _defesas = new List<Player>();
        private List<Player>? _atacantes = new List<Player>();
        private List<Player>? _unknown = new List<Player>();
    
    #endregion

    #region Public Attributes

        public string? Nome
            {get {return _nome;} set {_nome = value;}}

        public List<Player> Jogadores
            {get {return _jogadores ?? new List<Player>();}}
        // public int CountJogadores
        //     {get {return _jogadores?.Count ?? 0;}}

        public List<Player> Goleiros
            {get {return _goleiros ?? new List<Player>();}}
        // public int CountGoleiros
        //     {get {return _goleiros?.Count ?? 0;}}

        public List<Player> Defesas
            {get {return _defesas ?? new List<Player>();}}
        // public int CountDefesas
        //     {get {return _defesas?.Count ?? 0;}}
        
        public List<Player> Atacantes
            {get {return _atacantes ?? new List<Player>();}}
        // public int CountAtacantes
        //     {get {return _atacantes?.Count ?? 0;}}

        public List<Player> Unknown
            {get {return _unknown ?? new List<Player>();}}
    // public int CountUnknown
    //     {get {return _unknown?.Count ?? 0;}}

    #endregion


    // Constructor

    public Team () {}

    public Team(string nome, List<Player>? jogadores)
    {
        _nome = nome;

        if (jogadores == null)
        { jogadores = new List<Player>(); }
        _jogadores = jogadores;

        SortJogadores(jogadores);
    }


    // Methods

    public void AddJogador(Player jogador)
    {
        if (_jogadores == null)
            {_jogadores = new List<Player>();}
        _jogadores.Add(jogador);

        SortJogador(jogador);
    }


    public void RemoveJogador(Player jogador)
    {
        if (_jogadores == null)
            {return;}

        _jogadores?.Remove(jogador);

        switch (jogador.Position)
        {
            case 0:
                _unknown?.Remove(jogador);
                break;
            case 1:
                _goleiros?.Remove(jogador);
                break;
            case 2:
                _defesas?.Remove(jogador);
                break;
            case 3:
                _atacantes?.Remove(jogador);
                break;
            default:
                throw new ArgumentOutOfRangeException("Posição inválida.");
        }
    }


    private void SortJogadores(List<Player> jogadores)
    {
        foreach (var jogador in jogadores)
            {SortJogador(jogador);}
    }


    private void SortJogador(Player jogador)
    {
        switch (jogador.Position)
        {
            case 0:
                if (_unknown == null)
                    {_unknown = new List<Player>();}
                _unknown.Add(jogador);
                break;
            case 1:
                if (_goleiros == null)
                    {_goleiros = new List<Player>();}
                _goleiros.Add(jogador);
                break;
            case 2:
                if (_defesas == null)
                    {_defesas = new List<Player>();}
                _defesas.Add(jogador);
                break;
            case 3:
                if (_atacantes == null)
                    {_atacantes = new List<Player>();}
                _atacantes.Add(jogador);
                break;
            default:
                throw new ArgumentOutOfRangeException("Posição inválida.");
        }
    }


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


                