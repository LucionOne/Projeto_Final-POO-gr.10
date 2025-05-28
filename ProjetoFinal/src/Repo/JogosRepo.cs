using jogo;
using System;
using System.IO;
using System.Text.Json;
using Templates;

namespace MyRepository;

public class GamesRepo : RepoAbstract<Game> 
{
    // Const

    // private string FilePath = Path.Combine(FolderPath, FileName);

    // Private
    // private List<Game> _jogosList = new List<Game>();
    // private int _nextId;


    // Public
    public List<Game> JogosList { get { return _mainRepo; } }
    //public Dictionary<int, Jogo> JogosDict {get{return _jogosDict;} set {_jogosDict = value;}} // ⚠️ "private set" not working, gotta look into that 
    // public int NextId {get{return _nextId;} set {_nextId = value;}}                              // ⚠️              .·´¯`(>▂<)´¯`·. 




    // Constructor
    public GamesRepo LoadFromDataBase() // ⚠️
    {

        string JsonString = File.ReadAllText(_filePath);

        GamesRepo? temp = JsonSerializer.Deserialize<GamesRepo>(JsonString);

        if (temp == null)
            throw new Exception($"Failed to deserialize json {_filePath}");

        return temp;
    }




    public GamesRepo(string fileName) : base(fileName)
    {}

}