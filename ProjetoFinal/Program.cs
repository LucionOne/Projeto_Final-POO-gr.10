using jogador;
using MyRepository;

JogadorRepo jogadorRepo = JogadorRepo.LoadFromDataBase();

bool executando = true;

while (executando)
{
    Console.Clear();
    Console.WriteLine("=== Menu Jogador ===");
    Console.WriteLine("1 - Cadastrar jogador");
    Console.WriteLine("2 - Listar jogadores");
    Console.WriteLine("3 - Buscar jogador por ID");
    Console.WriteLine("4 - Remover jogador");
    Console.WriteLine("5 - Sair");
    Console.Write("Escolha uma opção: ");
    string? opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            Console.Write("Nome do jogador: ");
            string nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Idade: ");
            int idade = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("Posição (1 - Goleiro, 2 - Defesa, 3 - Ataque): ");
            int posicao = int.Parse(Console.ReadLine() ?? "0");

            Jogador novoJogador = new Jogador(nome, idade, posicao);
            jogadorRepo.Append(novoJogador);
            jogadorRepo.WriteToDataBase();

            Console.WriteLine("Jogador cadastrado com sucesso!");
            Console.ReadKey();
            break;

        case "2":
            Console.WriteLine("=== Lista de Jogadores ===");
            foreach (var jogador in jogadorRepo.JogadoresDict.Values)
            {
                Console.WriteLine($"ID: {jogador.Id} - {jogador.GetDatasString()}");
            }
            Console.ReadKey();
            break;

        case "3":
            Console.Write("Digite o ID do jogador: ");
            int idBusca = int.Parse(Console.ReadLine() ?? "0");

            if (jogadorRepo.JogadoresDict.ContainsKey(idBusca))
            {
                var jogador = jogadorRepo.get(idBusca);
                Console.WriteLine(jogador.GetDatasString());
            }
            else
            {
                Console.WriteLine("Jogador não encontrado.");
            }
            Console.ReadKey();
            break;

        case "4":
            Console.Write("Digite o ID do jogador para remover: ");
            int idRemove = int.Parse(Console.ReadLine() ?? "0");

            if (jogadorRepo.JogadoresDict.ContainsKey(idRemove))
            {
                jogadorRepo.Delete(idRemove);
                jogadorRepo.WriteToDataBase();
                Console.WriteLine("Jogador removido com sucesso.");
            }
            else
            {
                Console.WriteLine("Jogador não encontrado.");
            }
            Console.ReadKey();
            break;

        case "5":
            executando = false;
            break;

        default:
            Console.WriteLine("Opção inválida.");
            Console.ReadKey();
            break;
    }
}
