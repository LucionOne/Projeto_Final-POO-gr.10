using Models;
using System.Text.Json;
using Templates;

namespace MyRepository;

public class TeamRepo : RepoAbstract<Team>
{
    public TeamRepo(string fileName = "Teams.json") : base(fileName) { }

    public TeamRepo()
    {
        _fileName = "Teams.json";
        _filePath = Path.Combine(FolderPath, _fileName);
    }

    public override TeamRepo DataBaseStarter()
    {
        ConfirmFileAndFolderExistence();
        TeamRepo repository = LoadFromDataBase();
        return repository;
    }

    public override TeamRepo LoadFromDataBase()
    {
        string file = File.ReadAllText(_filePath);
        TeamRepo temp = JsonSerializer.Deserialize<TeamRepo>(file)
            ?? throw new NullReferenceException("Deserializer returned null");
        return temp;
    }
}

