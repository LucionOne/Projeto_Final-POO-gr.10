using jogador;
using System;
using System.IO;
using System.Text.Json;

namespace MyRepository;

public class JogadorRepo
{
    // Const
    private const string FolderPath = "DataBase";
    private const string FileName = "jogadores.json";
    private const string FilePath = "DataBase\\jogadores.json";
    // private string FilePath = Path.Combine(FolderPath, FileName);

    // Private
    private Dictionary<int, Jogador> _jogadoresDict = new Dictionary<int, Jogador>();
    private int _nextId;

    // Public
    public Dictionary<int, Jogador> JogadoresDict {get{return _jogadoresDict;} set {_jogadoresDict = value;}} // ⚠️ "private set" not working, gotta look into that 
    public int NextId {get{return _nextId;} set {_nextId = value;}}                                           // ⚠️              .·´¯`(>▂<)´¯`·. 
    
    // Constructor
    public static JogadorRepo LoadFromDataBase()
    {
        
        VerifyFileExists();
        
        string JsonString = File.ReadAllText(FilePath);
        
        JogadorRepo? temp = JsonSerializer.Deserialize<JogadorRepo>(JsonString);

        if (temp == null)
            throw new Exception($"Failed to deserialize json {FilePath}");

        return temp;
    }
    
    // Methods
    public void Append(Jogador jogador)
    {
        jogador.Id = _nextId;
        _jogadoresDict.Add(_nextId, jogador);
        _nextId += 1;
    }

    public void Remove(int id)
    {
        _jogadoresDict.Remove(id);
    }
    
    public void Edit(int id, Jogador jogador)
    {
        _jogadoresDict[id] = jogador;
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