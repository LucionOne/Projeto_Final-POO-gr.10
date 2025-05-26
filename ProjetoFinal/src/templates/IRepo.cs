namespace Templates;

public interface IRepo<T>
{
    void Add(T item);
    void RemoveAt(int id);
    void UpdateById(int id, T item);
    T GetById(int id);
    Dictionary<int,T> GetAll();
    void WriteToDataBase();
    void VerifyFileExists();
    // IRepo<T> LoadFromDataBase();
}