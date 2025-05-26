using jogador;
using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Text.Json;
using Templates;

namespace MyRepository;

public abstract class RepoAbs<T> : IRepo<baseModel>
{
    // Const
    private const string FolderPath = "DataBase";
    private string FileName;
    private string FilePath = "";
    // private string FilePath = Path.Combine(FolderPath, FileName);

    // Private
    // private Dictionary<int, Jogador> _jogadoresDict = new Dictionary<int, Jogador>();
    private List<baseModel> MainRepo = new();
    private int _nextId;
    
    // Public
    // public Dictionary<int, Jogador> JogadoresDict { get { return _jogadoresDict; } set { _jogadoresDict = value; } } // ⚠️ "private set" not working, gotta look into that 
    public int NextId {get{return _nextId;} set {_nextId = value;}}                                           // ⚠️              .·´¯`(>▂<)´¯`·. 
    
    // Constructor
    public static RepoAbs<baseModel> LoadFromDataBase()
    {
        string JsonString = File.ReadAllText(FilePath);
        
        RepoAbs<baseModel>? temp = JsonSerializer.Deserialize<RepoAbs<baseModel>>(JsonString);

        if (temp == null)
            throw new Exception($"Failed to deserialize json {FilePath}");

        return temp;
    }
    
    // Methods
    public void Add(baseModel baseModel)
    {
        baseModel.Id = _nextId;
        MainRepo.Add(baseModel);
        _nextId += 1;
    }

    public void RemoveAt(int id)
    {
        int index = MainRepo.FindIndex(x => x.Id == id);
        MainRepo.RemoveAt(index);
    }
    
    public void UpdateById(int id, baseModel baseModel)
    {
        int index = MainRepo.FindIndex(x => x.Id == id);
        MainRepo[index] = baseModel;
    }

    public baseModel GetById(int id)
    {
        int index = MainRepo.FindIndex(x => x.Id == id);
        return MainRepo[index];
    }

    public Dictionary<int, baseModel> GetAll()
    {
        return MainRepo.ToDictionary(Player => Player.Id, Player => Player);
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