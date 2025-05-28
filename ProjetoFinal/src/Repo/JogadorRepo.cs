using jogador;
using System;
using System.IO;
using System.Text.Json;
using Templates;
using Container.Wrapper;

namespace MyRepository;

public class PlayersRepo : RepoAbstract<Jogador>
{
    public PlayersRepo(string fileName = "Players.json") : base(fileName) { }

    public override PlayersRepo DataBaseStarter()
    {
        ConfirmFileAndFolderExistence();
        PlayersRepo Repository = LoadFromDataBase();
        return Repository;
    }

    public override PlayersRepo LoadFromDataBase()
    {
        string file = File.ReadAllText(_filePath);
        PlayersRepo temp = JsonSerializer.Deserialize<PlayersRepo>(file)
            ?? throw new NullReferenceException("Deserializer returned null");
        return temp;
    }
}