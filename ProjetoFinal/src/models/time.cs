using jogador;

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


public class Team
{
    //private
    private string? _nome;
    private List<Jogador>? _jogadores = new List<Jogador>();

    //public 
    public string? Nome {get {return _nome;} set {_nome = value;}}
    public List<Jogador>? Jogadores {get {return _jogadores;} set {_jogadores = value;}}

    // Constructor
    public Team(string nome, List<Jogador>? jogadores)
    {
        _nome = nome ?? string.Empty;
        
        if (jogadores == null)
            {_jogadores = new List<Jogador>();}
        else
            {_jogadores = jogadores;}
    }

    public void AppendJogador(Jogador jogador)
    {
        if (_jogadores == null)
            {_jogadores = new List<Jogador>();}
        _jogadores.Add(jogador);
    }


}


