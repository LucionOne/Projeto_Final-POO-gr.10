using Models;
using System;
using System.IO;
using System.Text.Json;
using Templates;
using Microsoft.VisualBasic;
using Container.DTOs;

namespace MyRepository;

public class PlayersRepo : RepoAbstract<Player>
{
    public PlayersRepo(string fileName = "Players.json") : base(fileName) { }


    public PlayersRepo()
    {
        _fileName = "Players.json";
        _filePath = Path.Combine(FolderPath, _fileName);
    }


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

    public List<PlayerDto> ToDtoList()
    {
        return _mainRepo.Select(p => new PlayerDto(p)).ToList();
    }

    // public override PlayersRepo LoadFromDataBase()
    // {
    //     string file = File.ReadAllText(_filePath);
    //     var wrapper = JsonSerializer.Deserialize<RepoWrapper<PlayersRepo, Player>>(file)
    //         ?? throw new Exception("Deserialization returned null");

    //     PlayersRepo repo = new PlayersRepo(_fileName);
    //     // repo._mainRepo = wrapper.MainRepo;

    //     repo._mainRepo.AddRange(wrapper.MainRepo);
    //     repo.NextId = wrapper.NextId;

    //     return repo;
    // }

    // public override PlayersRepo 'LoadFromDataBase()
    // {
    //     string file = File.ReadAllText(_filePath);
    //     PlayersRepo temp = JsonSerializer.Deserialize<PlayersRepo>(file)
    //         ?? throw new NullReferenceException("Deserializer returned null");
    //     return temp;
    // }
}