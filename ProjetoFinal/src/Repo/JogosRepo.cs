using jogo;
using System;
using System.IO;
using System.Text.Json;
using Templates;

namespace MyRepository;

public class GamesRepo : IRepo<Game>
{
    // Const
    private const string FolderPath = "DataBase";
    private const string FileName = "Jogos.json";
    private const string FilePath = "DataBase\\Jogos.json";
    // private string FilePath = Path.Combine(FolderPath, FileName);

    // Private
    private List<Game> _jogosList = new List<Game>();
    private int _nextId;

    // Public
    public List<Game> JogosList {get {return _jogosList;} set {_jogosList = value;}}
    //public Dictionary<int, Jogo> JogosDict {get{return _jogosDict;} set {_jogosDict = value;}} // ⚠️ "private set" not working, gotta look into that 
    public int NextId {get{return _nextId;} set {_nextId = value;}}                              // ⚠️              .·´¯`(>▂<)´¯`·. 
    
    // Constructor
    public static GamesRepo LoadFromDataBase() // ⚠️
    {
        
        string JsonString = File.ReadAllText(FilePath);
        
        GamesRepo? temp = JsonSerializer.Deserialize<GamesRepo>(JsonString);

        if (temp == null)
            throw new Exception($"Failed to deserialize json {FilePath}");

        return temp;
    }
    
    // Methods
    public void Add(Game jogo)
    {
        jogo.Id = _nextId;
        JogosList.Add(jogo);
        _nextId += 1;
    }

    public void Remove(Game jogo)
    {
        _jogosList.Remove(jogo);
    }
    
    public void Update(int id, Game jogador)
    {
        _jogosList[id] = jogador;
    }

    public Game GetById(int id)
    {
        return _jogosList[id];
    }

    public Dictionary<int, Game> GetAll()
    {
        return _jogosList.ToDictionary(game => game.Id, game => game);
    }


    public void VerifyFileExists()
    {
        if (!Directory.Exists(FolderPath))
        {
            Directory.CreateDirectory(FolderPath);
        }
        if (!File.Exists(FilePath))
        {
            File.WriteAllText(FilePath, "{}");
            // File.WriteAllText(FilePath,"{\"_jogadoresDict\": {}, \"_nextId\": 0}");
        }
    }
    
    public string Serialize()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        return JsonSerializer.Serialize(this, options);
    }

    public void WriteToDataBase()
    {
        File.WriteAllText(FilePath,Serialize());
    }
}