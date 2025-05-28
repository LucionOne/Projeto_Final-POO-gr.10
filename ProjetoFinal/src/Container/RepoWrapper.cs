using MyRepository;
using Templates;
using System.Text.Json;

namespace Container.Wrapper;

public class RepoWrapper<T> where T : RepoAbstract<IModel>
{
    public List<IModel> MainRepo;
    public int NextId;

    public RepoWrapper(T Package)
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