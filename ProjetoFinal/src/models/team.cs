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

        private string? _nome;
        private List<Jogador>? _jogadores = new List<Jogador>();
        private List<Jogador>? _goleiros = new List<Jogador>();
        private List<Jogador>? _defesas = new List<Jogador>();
        private List<Jogador>? _atacantes = new List<Jogador>();
        private List<Jogador>? _unknown = new List<Jogador>();
    
    #endregion

    #region Public Attributes

        public string? Nome
            {get {return _nome;} set {_nome = value;}}

        public List<Jogador> Jogadores
            {get {return _jogadores ?? new List<Jogador>();}}
        // public int CountJogadores
        //     {get {return _jogadores?.Count ?? 0;}}

        public List<Jogador> Goleiros
            {get {return _goleiros ?? new List<Jogador>();}}
        // public int CountGoleiros
        //     {get {return _goleiros?.Count ?? 0;}}

        public List<Jogador> Defesas
            {get {return _defesas ?? new List<Jogador>();}}
        // public int CountDefesas
        //     {get {return _defesas?.Count ?? 0;}}
        
        public List<Jogador> Atacantes
            {get {return _atacantes ?? new List<Jogador>();}}
        // public int CountAtacantes
        //     {get {return _atacantes?.Count ?? 0;}}

        public List<Jogador> Unknown
            {get {return _unknown ?? new List<Jogador>();}}
        // public int CountUnknown
        //     {get {return _unknown?.Count ?? 0;}}

    #endregion


    // Constructor

    public Team(string nome, List<Jogador>? jogadores)
    {
        _nome = nome;

        if (jogadores == null)
            {jogadores = new List<Jogador>();}
        _jogadores = jogadores;

        SortJogadores(jogadores);
    }


    // Methods

    public void AddJogador(Jogador jogador)
    {
        if (_jogadores == null)
            {_jogadores = new List<Jogador>();}
        _jogadores?.Add(jogador);

        SortJogador(jogador);
    }


    public void RemoveJogador(Jogador jogador)
    {
        if (_jogadores == null)
            {return;}

        _jogadores?.Remove(jogador);

        switch (jogador.Posicao)
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


    private void SortJogadores(List<Jogador> jogadores)
    {
        foreach (var jogador in jogadores)
            {SortJogador(jogador);}
    }


    private void SortJogador(Jogador jogador)
    {
        switch (jogador.Posicao)
        {
            case 0:
                if (_unknown == null)
                    {_unknown = new List<Jogador>();}
                _unknown.Add(jogador);
                break;
            case 1:
                if (_goleiros == null)
                    {_goleiros = new List<Jogador>();}
                _goleiros.Add(jogador);
                break;
            case 2:
                if (_defesas == null)
                    {_defesas = new List<Jogador>();}
                _defesas.Add(jogador);
                break;
            case 3:
                if (_atacantes == null)
                    {_atacantes = new List<Jogador>();}
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


                