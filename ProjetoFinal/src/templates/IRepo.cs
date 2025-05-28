namespace Templates;

public interface IRepo<T>
{
    void Add(T item);
    void Remove(T item);
    void RemoveAt(int id);
    void UpdateById(int id, T item);
    T? GetById(int id);
    IEnumerable<T> GetAll();
    Dictionary<int, T> GetAllAsDictionary(int id);
    void WriteToDataBase();
}