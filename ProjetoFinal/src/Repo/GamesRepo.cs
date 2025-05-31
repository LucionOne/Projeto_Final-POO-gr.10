using jogo;
using System;
using System.IO;
using System.Text.Json;
using Templates;
using Container.Wrapper;

namespace MyRepository;

public class GamesRepo : RepoAbstract<Game>
{
    public GamesRepo(string fileName = "Games.json") : base(fileName) { }

    public GamesRepo() : base() 
    {
        _fileName = "Games.json";
        _filePath = Path.Combine(FolderPath, _fileName);
    }

    // public GamesRepo(List<Game> mainRepo, int nextId)
    // {
    // _mainRepo = mainRepo ?? new List<Game>();
    // _nextId = nextId;

    // _fileName = "default.json";
    // _filePath = Path.Combine(FolderPath, _fileName);
    // }

    public override GamesRepo DataBaseStarter()
    {
        ConfirmFileAndFolderExistence();
        GamesRepo Repository = LoadFromDataBase();
        return Repository;
    }


    public override GamesRepo LoadFromDataBase()
    {
        string file = File.ReadAllText(_filePath);
        GamesRepo temp = JsonSerializer.Deserialize<GamesRepo>(file)
            ?? throw new NullReferenceException("Deserializer returned null");
        return temp;
    }
    




    // public override GamesRepo LoadFromDataBase()
    // {
    //     string file = File.ReadAllText(_filePath);
    //     var wrapper = JsonSerializer.Deserialize<RepoWrapper<GamesRepo, Game>>(file)
    //         ?? throw new Exception("Deserialization returned null");

    //     GamesRepo repo = new GamesRepo(_fileName);
    //     // repo._mainRepo = wrapper.MainRepo;

    //     repo._mainRepo.AddRange(wrapper.MainRepo);
    //     repo.NextId = wrapper.NextId;

    //     return repo;
    // }

    // public override GamesRepo LoadFromDataBase()
    // {
    //     string file = File.ReadAllText(_filePath);
    //     GamesRepo temp = JsonSerializer.Deserialize<GamesRepo>(file)
    //         ?? throw new NullReferenceException("Deserializer returned null");
    //     return temp;
    // }

}














// Const

    // // private string FilePath = Path.Combine(FolderPath, FileName);

    // // Private
    // // private List<Game> _jogosList = new List<Game>();
    // // private int _nextId;


    // // Public
    // public List<Game> JogosList { get { return _mainRepo; } }
    // //public Dictionary<int, Jogo> JogosDict {get{return _jogosDict;} set {_jogosDict = value;}} // ⚠️ "private set" not working, gotta look into that 
    // // public int NextId {get{return _nextId;} set {_nextId = value;}}                              // ⚠️              .·´¯`(>▂<)´¯`·. 




    // // Constructor
    // public GamesRepo LoadFromDataBase() // ⚠️
    // {

    //     string JsonString = File.ReadAllText(_filePath);

    //     GamesRepo? temp = JsonSerializer.Deserialize<GamesRepo>(JsonString);

    //     if (temp == null)
    //         throw new Exception($"Failed to deserialize json {_filePath}");

    //     return temp;
    // }




    // public GamesRepo(string fileName) : base(fileName)
    // {}