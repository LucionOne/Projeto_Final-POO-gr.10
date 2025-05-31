using MyRepository;
using Templates;
using System.Text.Json;

namespace Container.Wrapper;

public class RepoWrapper<Repo, Model> 
    where Repo : RepoAbstract<Model>
    where Model : IModel
{
    public List<Model> MainRepo;
    public int NextId;

    public RepoWrapper(Repo Package)
    {
        MainRepo = Package.MainRepo;
        NextId = Package.NextId;
    }

    public string jsonSerialize()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(this, options);
    }

    
}