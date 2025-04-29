using jogo;
using System;
using System.IO;
using System.Text.Json;

namespace MyRepository;

public class JogosRepo
{
    // Const
    private const string FolderPath = "DataBase";
    private const string FileName = "Jogos.json";
    private const string FilePath = "DataBase\\Jogos.json";
    // private string FilePath = Path.Combine(FolderPath, FileName);

    // Private
    private List<Jogo> _jogosList = new List<Jogo>();
    private int _nextId;

    // Public
    public List<Jogo> JogosList {get {return _jogosList;} set {_jogosList = value;}}
    //public Dictionary<int, Jogo> JogosDict {get{return _jogosDict;} set {_jogosDict = value;}} // ⚠️ "private set" not working, gotta look into that 
    public int NextId {get{return _nextId;} set {_nextId = value;}}                              // ⚠️              .·´¯`(>▂<)´¯`·. 
    
    // Constructor
    public static JogosRepo LoadFromDataBase() // ⚠️
    {
        
        VerifyFileExists();
        
        string JsonString = File.ReadAllText(FilePath);
        
        JogosRepo? temp = JsonSerializer.Deserialize<JogosRepo>(JsonString);

        if (temp == null)
            throw new Exception($"Failed to deserialize json {FilePath}");

        return temp;
    }
    
    // Methods
    public void Append(Jogo jogo)
    {
        jogo.Id = _nextId;
        JogosList.Add(jogo);
        _nextId += 1;
    }

    public void Delete(Jogo jogo)
    {
        _jogosList.Remove(jogo);
    }
    
    public void Edit(int id, Jogo jogador)
    {
        _jogosList[id] = jogador;
    }

    public Jogo Get(int id)
    {
        return _jogosList[id];
    }

    private static void VerifyFileExists()
    {
        if (!Directory.Exists(FolderPath))
        {
            Directory.CreateDirectory(FolderPath);
        }
        if (!File.Exists(FilePath))
        {
            File.WriteAllText(FilePath,"{}");
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