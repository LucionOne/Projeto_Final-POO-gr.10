using team;
using jogador;

namespace jogo;

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


public class Jogo
{

    #region Constant Attributes

        public const int TeamsMínimos = 2;

    #endregion
    
    #region Private Attributes

        private DateOnly _date;
        private TimeOnly _horaInicio;
        private string _local = string.Empty;
        private string _tipoDeCampo = string.Empty;
        private int _quantidadeJogadoresPorTeam;
    // private int _limiteJogadores;
        private int _limiteTeams;
        private int _id;
        private List<Jogador>? _filaJogadoresSemTeam = new List<Jogador>();
    // private List<Jogador>? FilaGoleiros = new List<Jogador>();
        private List<Team>? _Teams = new List<Team>();

        private int QuantidadeGoleiro;
        private int QuantidadeDefesa;
        private int QuantidadeAtacante;
    
    #endregion

    #region Public Attributes

        public DateOnly Date 
            {get {return _date;} set {_date = value;}}
        public TimeOnly HoraInicio 
            {get {return _horaInicio;} set {_horaInicio = value;}}
        public string Local 
            {get {return _local;} set {_local = value ?? string.Empty;}}
        public string TipoDeCampo 
            {get {return _tipoDeCampo;} set {_tipoDeCampo = value ?? string.Empty;}}
        public int QuantidadeJogadoresPorTeam 
            {get {return _quantidadeJogadoresPorTeam;} set {_quantidadeJogadoresPorTeam = value;}} 
    // public int LimiteJogadores 
        // {get {return _limiteJogadores;} set {_limiteJogadores = value;}}
        public int LimiteTeams 
            {get {return _limiteTeams;} set {_limiteTeams = value;}}
        public int Id 
            {get {return _id;} set {_id = value;}}
        public List<Jogador>? FilaJogadoresSemTeam 
            {get {return _filaJogadoresSemTeam;} set {_filaJogadoresSemTeam = value;}}
        public List<Team>? Teams 
            {get {return _Teams;} set {_Teams = value;}}

    #endregion


    // Constructor

    public Jogo(DateOnly date, TimeOnly horaInicio, string local, string tipoDeCampo, int quantidadeJogadoresPorTeam, int limiteJogadores, int limiteTeams)
    {
        _date = date;
        _horaInicio = horaInicio;
        _local = local ?? string.Empty;
        _tipoDeCampo = tipoDeCampo ?? string.Empty;
        _quantidadeJogadoresPorTeam = quantidadeJogadoresPorTeam;
     // _limiteJogadores = limiteJogadores;
        _limiteTeams = limiteTeams;
    }


    // Methods

    public void AddJogadorSemTeam(Jogador jogador)
    {
        if (_filaJogadoresSemTeam == null)
            {_filaJogadoresSemTeam = new List<Jogador>();}
        _filaJogadoresSemTeam.Add(jogador);
    }

    public bool ValidateTeam(Team team)
    {
        if (team.Jogadores == null)
            return false;

        if (team.Jogadores.Count != _quantidadeJogadoresPorTeam)
            return false;
        
        if (team.Goleiros.Count != QuantidadeGoleiro)
            return false;
        
        if (team.Defesas.Count != QuantidadeDefesa)
            return false;
        
        if (team.Atacantes.Count != QuantidadeAtacante)
            return false;
        
        // if (team.Unknown.Count > 0)
        //     return false;

        return true;
    }

    


}