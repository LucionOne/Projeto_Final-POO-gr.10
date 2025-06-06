using Templates;

namespace Templates;

public abstract class ModelAbstract : IModel
{
    protected int _id;
    public int Id { get { return _id; } set { _id = value; } }
}