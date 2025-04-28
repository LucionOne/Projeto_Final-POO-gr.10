using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using jogador;

namespace MyRepository;

public class JogadorRepo
{
    // Const
    private const string FolderPath = "DataBase";
    private string FilePath = Path.Combine(FolderPath, "jogadores.json");
    
    // Private

    private Dictionary<int, Jogador> _jogadoresDict = new Dictionary<int, Jogador>();
    
    private int _NextId;

    // Public
    public Dictionary<int, Jogador> JogadoresDict {get{return _jogadoresDict;}}

    public int NextId {get{return _NextId;}}

    // Construct
    public JogadorRepo()
    {
        VerifyFileExists();
        string jsonString = File.ReadAllText(FilePath);
        JogadorRepo? data = JsonSerializer.Deserialize<JogadorRepo>(jsonString);
        
        if (data != null)
        {
            _jogadoresDict = data._jogadoresDict ?? new Dictionary<int, Jogador>();
            _NextId = data._NextId;
        }
        else
        {
            _jogadoresDict = new Dictionary<int, Jogador>();
            _NextId = 0;
        }
    }

    //methods
    public void Append(Jogador jogador)
    {
        jogador.Id = _NextId;
        _jogadoresDict[_NextId] = jogador;
        _NextId += 1;
    }

    public void Remove(int id)
    {
        _jogadoresDict.Remove(id);
    }
    
    public void Edit(int id, Jogador jogador)
    {
        _jogadoresDict[id] = jogador;
    }
    
    private void VerifyFileExists()
    {
        if (!Directory.Exists(FolderPath))
        {
            Directory.CreateDirectory(FolderPath);
        }
        if (!File.Exists(FilePath))
        {
            File.WriteAllText(FilePath, "");
        }
    }
    
    public string Serialize()
    {
        return JsonSerializer.Serialize(this);
    }

    public void WriteToDataBase()
    {
        File.WriteAllText(FilePath,Serialize());
    }
}