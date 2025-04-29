using jogador;
using MyRepository;

// DEMO/Debugging

Jogador jogador = new Jogador("Gp",19,1);
Console.WriteLine(jogador.GetDatasString());

JogadorRepo jogadorRepo = JogadorRepo.LoadFromDataBase();
jogadorRepo.Append(jogador);
Console.WriteLine(jogadorRepo.Serialize());
jogadorRepo.WriteToDataBase();
