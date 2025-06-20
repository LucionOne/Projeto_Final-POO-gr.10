using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Templates;

#pragma warning disable CS8766 // IT WON'T TELL IF SOMETHING CAN BE NULL ⚠️⚠️

public abstract class RepoAbstract<T> : IRepo<T> where T : IModel
{
    protected const string FolderPath = "DataBase";
    protected string _fileName;
    protected string _filePath;

    private bool _saved = true;
    [JsonIgnore]
    public bool Saved { get { return _saved; } } 

    protected List<T> _mainRepo = new();
    protected int _nextId;

    public List<T> MainRepo { get { return _mainRepo; } set { _mainRepo = value; } }
    [JsonIgnore]
    public int Count { get { return _mainRepo.Count; } }

    public int NextId { get { return _nextId; } set { _nextId = value; } }

    [JsonConstructor]
    public RepoAbstract()
    {
        _fileName = "default.json";
        _filePath = Path.Combine(FolderPath, _fileName);
    }

    /// <summary>
    /// Constructor for the repository
    /// </summary>
    /// <param name="fileName">The name of the file.json where the repository will be stored.</param>
    protected RepoAbstract(string fileName)
    {
        _fileName = fileName;
        _filePath = Path.Combine(FolderPath, _fileName);
    }

    //Methods

    /// <summary>
    /// Adds an item in the database list
    /// </summary>
    /// <param name="item">Item to Add</param>
    public virtual void Add(T item)
    {
        item.Id = _nextId;
        _mainRepo.Add(item);
        _nextId += 1;
        _saved = false;
    }

    /// <summary>
    /// Removes all item with the same Id as the item given
    /// </summary>
    /// <param name="item">Item to Remove</param>
    public virtual void Remove(T item)
    {
        _mainRepo.RemoveAll(x => x.Id == item.Id);
        _saved = false;
    }

    /// <summary>
    /// Removes an item by its Id
    /// </summary>
    /// <param name="id">The Item's Id</param>
    public virtual void RemoveAt(int id)
    {
        int index = _mainRepo.FindIndex(x => x.Id == id);
        _mainRepo.RemoveAt(index);
        _saved = false;
    }

    /// <summary>
    /// You give it an id and and item to substitute whatever item was in said id
    /// </summary>
    /// <param name="id">The old item's Id</param>
    /// <param name="item">The New item</param>
    public virtual void UpdateById(int id, T item)
    {
        int index = _mainRepo.FindIndex(x => x.Id == id);

        if (index == -1)
        {
            _mainRepo.Add(item);
            return;
        }
        
        item.Id = id;
        _mainRepo[index] = item;
        _saved = false;
    }

    /// <summary>
    /// Get a item by its id
    /// </summary>
    /// <returns> the first item it finds or and null if it doesn't find it </returns>
    /// <param name="id">The items Id</param>
    /// <returns></returns>
    public virtual T? GetById(int id)
    {
        if (id < 0) return default;
        return _mainRepo.FirstOrDefault(x => x.Id == id);
    }

    /// <summary>
    /// Gets all items in the database list
    /// </summary>
    /// <returns>All items</returns>
    public virtual List<T> GetAll()
    {
        return _mainRepo;
    }

    /// <summary>
    /// Gets all items in the database list as a dictionary
    /// </summary>
    /// <returns>All items as a dictionary with Ids as keys</returns>
    public virtual Dictionary<int, T> GetAllAsDictionary()
    {
        return _mainRepo.ToDictionary(item => item.Id, item => item);
    }
    
    public virtual T? Last()
    {
        return _mainRepo.LastOrDefault();
    }

    /// <summary>
    /// Verifies if the file exists, if it doesn't it creates the directory and file
    /// </summary>
    public virtual void ConfirmFileAndFolderExistence()
    {
        if (!Directory.Exists(FolderPath))
            Directory.CreateDirectory(FolderPath);

        if (!File.Exists(_filePath))
            File.WriteAllText(_filePath, "{  \"MainRepo\": [],  \"NextId\": 0}");
    }

    /// <summary>
    /// Serializes the main repository list to a JSON string
    /// </summary>
    /// <returns>A JSON string representation of the main repository</returns>
    protected virtual string Serialize()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(this, options);
    }

    /// <summary>
    /// Writes the main repository list to the database file
    /// </summary>
    public virtual void WriteToDataBase()
    {
        File.WriteAllText(_filePath, Serialize());
        _saved = true;
    }

    public virtual RepoAbstract<T> DataBaseStarter() ////Needs to be in the inheriting class
    {
        ConfirmFileAndFolderExistence();
        RepoAbstract<T> Repository = LoadFromDataBase();
        return Repository;
    }

    public virtual RepoAbstract<T> LoadFromDataBase() //Needs to be in the inheriting class
    {
        string file = File.ReadAllText(_filePath);
        RepoAbstract<T> temp = JsonSerializer.Deserialize<RepoAbstract<T>>(file)
            ?? throw new NullReferenceException("Deserializer returned null");
        return temp;
    }


}