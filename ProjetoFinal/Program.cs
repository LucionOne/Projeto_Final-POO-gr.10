using System;
using System.Collections.Generic;
using jogador;
using jogo;

class Program
{
    static List<Jogador> jogadores = new List<Jogador>();
    static JogoRepo jogoRepo = new JogoRepo();
    static int proximoIdJogador = 1;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== Menu Inicial ====");
            Console.WriteLine("1. Gerenciar Jogadores");
            Console.WriteLine("2. Gerenciar Jogos");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha: ");
            var escolha = Console.ReadLine();

            if (escolha == "1") MenuJogadores();
            else if (escolha == "2") MenuJogos();
            else if (escolha == "0") break;
            else Console.WriteLine("Opção inválida. Pressione Enter.");
            Console.ReadLine();
        }
    }

    static void MenuJogadores()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Menu Jogadores ===");
            Console.WriteLine("1. Cadastrar Jogador");
            Console.WriteLine("2. Listar Jogadores");
            Console.WriteLine("3. Atualizar Jogador");
            Console.WriteLine("4. Remover Jogador");
            Console.WriteLine("0. Voltar");
            Console.Write("Escolha uma opção: ");
            int.TryParse(Console.ReadLine(), out opcao);

            switch (opcao)
            {
                case 1: CadastrarJogador(); break;
                case 2: ListarJogadores(); break;
                case 3: AtualizarJogador(); break;
                case 4: RemoverJogador(); break;
                case 0: break;
                default: Console.WriteLine("Opção inválida."); break;
            }
            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
        } while (opcao != 0);
    }

    static void MenuJogos()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Menu Jogos ===");
            Console.WriteLine("1. Criar Jogo");
            Console.WriteLine("2. Listar Jogos");
            Console.WriteLine("3. Atualizar Jogo");
            Console.WriteLine("4. Remover Jogo");
            Console.WriteLine("5. Adicionar Interessado");
            Console.WriteLine("6. Remover Interessado");
            Console.WriteLine("0. Voltar");
            Console.Write("Escolha uma opção: ");
            int.TryParse(Console.ReadLine(), out opcao);

            switch (opcao)
            {
                case 1: CriarJogo(); break;
                case 2: ListarJogos(); break;
                case 3: AtualizarJogo(); break;
                case 4: RemoverJogo(); break;
                case 5: AdicionarInteressado(); break;
                case 6: RemoverInteressado(); break;
                case 0: break;
                default: Console.WriteLine("Opção inválida."); break;
            }
            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
        } while (opcao != 0);
    }

    // --- Funções Jogador ---
    static void CadastrarJogador()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine() ?? "";
        Console.Write("Idade: ");
        int idade = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Posição (1 - Goleiro, 2 - Defesa, 3 - Ataque): ");
        int posicao = int.Parse(Console.ReadLine() ?? "0");

        Jogador novo = new Jogador(nome, idade, posicao) { Id = proximoIdJogador++ };
        jogadores.Add(novo);
        Console.WriteLine("Jogador cadastrado!");
    }

    static void ListarJogadores()
    {
        if (jogadores.Count == 0) Console.WriteLine("Nenhum jogador.");
        else foreach (var j in jogadores) Console.WriteLine($"ID: {j.Id} - {j.GetDatasString()}");
    }

    static void AtualizarJogador()
    {
        Console.Write("ID do jogador: ");
        int id = int.Parse(Console.ReadLine() ?? "0");
        var jogador = jogadores.Find(j => j.Id == id);
        if (jogador == null) { Console.WriteLine("Não encontrado."); return; }

        Console.Write("Novo nome: ");
        jogador.Nome = Console.ReadLine() ?? "";
        Console.Write("Nova idade: ");
        jogador.Idade = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Nova posição (1 - Goleiro, 2 - Defesa, 3 - Ataque): ");
        jogador.Posicao = int.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine("Jogador atualizado.");
    }

    static void RemoverJogador()
    {
        Console.Write("ID do jogador: ");
        int id = int.Parse(Console.ReadLine() ?? "0");
        var jogador = jogadores.Find(j => j.Id == id);
        if (jogador != null)
        {
            jogadores.Remove(jogador);
            Console.WriteLine("Removido.");
        }
        else Console.WriteLine("Não encontrado.");
    }

    // --- Funções Jogo ---
    static void CriarJogo()
    {
        Console.Write("Data (dd/mm/aaaa): ");
        DateTime data = DateTime.Parse(Console.ReadLine() ?? "");
        Console.Write("Local: ");
        string local = Console.ReadLine() ?? "";
        Console.Write("Tipo de campo: ");
        string tipoCampo = Console.ReadLine() ?? "";
        Console.Write("Jogadores por time: ");
        int jogadoresPorTime = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Limite de times (opcional, pressione Enter para ignorar): ");
        string limiteStr = Console.ReadLine() ?? "";
        int? limite = int.TryParse(limiteStr, out int l) ? l : null;

        Jogo jogo = new Jogo
        {
            Data = data,
            Local = local,
            TipoCampo = tipoCampo,
            JogadoresPorTime = jogadoresPorTime,
            LimiteDeTimes = limite
        };

        jogoRepo.Adicionar(jogo);
        Console.WriteLine("Jogo criado.");
    }

    static void ListarJogos()
    {
        var jogos = jogoRepo.ListarTodos();
        if (jogos.Count == 0) Console.WriteLine("Nenhum jogo.");
        else foreach (var j in jogos)
        {
            Console.WriteLine(j.GetInfo());
            Console.WriteLine($"Interessados (IDs): {string.Join(", ", j.InteressadosIds)}");
        }
    }

    static void AtualizarJogo()
    {
        Console.Write("ID do jogo: ");
        int id = int.Parse(Console.ReadLine() ?? "0");
        var antigo = jogoRepo.BuscarPorId(id);
        if (antigo == null) { Console.WriteLine("Jogo não encontrado."); return; }

        Console.Write("Nova data (dd/mm/aaaa): ");
        DateTime data = DateTime.Parse(Console.ReadLine() ?? "");
        Console.Write("Novo local: ");
        string local = Console.ReadLine() ?? "";
        Console.Write("Novo tipo de campo: ");
        string tipoCampo = Console.ReadLine() ?? "";
        Console.Write("Jogadores por time: ");
        int jogadoresPorTime = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Limite de times (opcional, Enter para manter): ");
        string limiteStr = Console.ReadLine() ?? "";
        int? limite = int.TryParse(limiteStr, out int l) ? l : antigo.LimiteDeTimes;

        var novo = new Jogo
        {
            Data = data,
            Local = local,
            TipoCampo = tipoCampo,
            JogadoresPorTime = jogadoresPorTime,
            LimiteDeTimes = limite
        };

        jogoRepo.Atualizar(id, novo);
        Console.WriteLine("Jogo atualizado.");
    }

    static void RemoverJogo()
    {
        Console.Write("ID do jogo: ");
        int id = int.Parse(Console.ReadLine() ?? "0");
        if (jogoRepo.Remover(id)) Console.WriteLine("Removido.");
        else Console.WriteLine("Não encontrado.");
    }

    static void AdicionarInteressado()
    {
        Console.Write("ID do jogo: ");
        int jogoId = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("ID do jogador: ");
        int jogadorId = int.Parse(Console.ReadLine() ?? "0");

        if (jogoRepo.AdicionarInteressado(jogoId, jogadorId))
            Console.WriteLine("Interessado adicionado.");
        else Console.WriteLine("Erro ao adicionar.");
    }

    static void RemoverInteressado()
    {
        Console.Write("ID do jogo: ");
        int jogoId = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("ID do jogador: ");
        int jogadorId = int.Parse(Console.ReadLine() ?? "0");

        if (jogoRepo.RemoverInteressado(jogoId, jogadorId))
            Console.WriteLine("Interessado removido.");
        else Console.WriteLine("Erro ao remover.");
    }
}
