namespace Templates;

public interface IRepository<T> where T : IModel
{
    void Add(T item);
    void Remove(T item);
    void RemoveAt(int id);
    void UpdateById(int id, T item);
    T? GetById(int id);
    List<T> GetAll();
    Dictionary<int, T> GetAllAsDictionary();
    void WriteToDataBase();
    void ConfirmFileAndFolderExistence();
}
