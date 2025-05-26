using jogador;
using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Text.Json;
using Templates;

namespace MyRepository;

public class JogadorRepo : IRepo<Jogador>
{
    // Const
    private const string FolderPath = "DataBase";
    private const string FileName = "jogadores.json";
    private const string FilePath = "DataBase\\jogadores.json";
    // private string FilePath = Path.Combine(FolderPath, FileName);

    // Private
    private Dictionary<int, Jogador> _jogadoresDict = new Dictionary<int, Jogador>();
    private List<Jogador> _jogadoresList = new();
    private int _nextId;
    
    // Public
    public Dictionary<int, Jogador> JogadoresDict { get { return _jogadoresDict; } set { _jogadoresDict = value; } } // ⚠️ "private set" not working, gotta look into that 
    public int NextId {get{return _nextId;} set {_nextId = value;}}                                           // ⚠️              .·´¯`(>▂<)´¯`·. 
    
    // Constructor
    public static JogadorRepo LoadFromDataBase()
    {
        
        string JsonString = File.ReadAllText(FilePath);
        
        JogadorRepo? temp = JsonSerializer.Deserialize<JogadorRepo>(JsonString);

        if (temp == null)
            throw new Exception($"Failed to deserialize json {FilePath}");

        return temp;
    }
    
    // Methods
    public void Add(Jogador jogador)
    {
        jogador.Id = _nextId;
        _jogadoresList.Add(jogador);
        _nextId += 1;
    }

    public void RemoveAt(int id)
    {
        int index = _jogadoresList.FindIndex(x => x.Id == id);
        _jogadoresList.RemoveAt(index);
    }
    
    public void UpdateById(int id, Jogador jogador)
    {
        int index = _jogadoresList.FindIndex(x => x.Id == id);
        _jogadoresList[index] = jogador;
    }

    public Jogador GetById(int id)
    {
        return _jogadoresList[id];
    }

    public Dictionary<int, Jogador> GetAll()
    {
        return _jogadoresList.ToDictionary(Player => Player.Id, Player => Player);
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