using Templates;

namespace Templates;

public abstract class ModelAbstract : IModel
{
    private int _id;
    public int Id { get { return _id; } set { _id = value; } }
}