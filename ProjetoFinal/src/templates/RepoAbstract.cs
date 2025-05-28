using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Templates;

namespace Templates;

#pragma warning disable CS8766 // IT WON'T TELL IF SOMETHING CAN BE NULL ⚠️⚠️

public abstract class RepoAbstract<T> : IRepo<T> where T : IModel
{
    protected const string FolderPath = "DataBase";
    protected readonly string _fileName;
    protected readonly string _filePath;
    protected readonly List<T> _mainRepo = new();
    private int _nextId;

    public int NextId
    {
        get => _nextId;
        set => _nextId = value;
    }

    

    /// <summary>
    /// Constructor for the repository
    /// </summary>
    /// <param name="fileName">The name of the file.json where the repository will be stored.</param>
    protected RepoAbstract(string fileName)
    {
        _fileName = fileName;
        _filePath = Path.Combine(FolderPath, _fileName);
        // VerifyFileExists();
    }

    /// <summary>
    /// Adds an item in the database list
    /// </summary>
    /// <param name="item">Item to Add</param>
    public virtual void Add(T item)
    {
        item.Id = _nextId;
        _mainRepo.Add(item);
        _nextId += 1;
    }

    /// <summary>
    /// Removes all item with the same Id as the item given
    /// </summary>
    /// <param name="item">Item to Remove</param>
    public virtual void Remove(T item)
    {
        _mainRepo.RemoveAll(x => x.Id == item.Id);
    }

    /// <summary>
    /// Removes an item by its Id
    /// </summary>
    /// <param name="id">The Item's Id</param>
    public virtual void RemoveAt(int id)
    {
        int index = _mainRepo.FindIndex(x => x.Id == id);
        if (index >= 0)
            _mainRepo.RemoveAt(index);
    }

    /// <summary>
    /// You give it an id and and item to substitute whatever item was in said id
    /// </summary>
    /// <param name="id">The old item's Id</param>
    /// <param name="item">The New item</param>
    public virtual void UpdateById(int id, T item)
    {
        int index = _mainRepo.FindIndex(x => x.Id == id);
        if (index >= 0)
            _mainRepo[index] = item;
    }

    /// <summary>
    /// Get a item by its id
    /// </summary>
    /// <returns> the first item it finds or and null if it doesn't find it </returns>
    /// <param name="id">The items Id</param>
    /// <returns></returns>
    public virtual T? GetById(int id)
    {
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

    /// <summary>
    /// Verifies if the file exists, if it doesn't it creates the directory and file
    /// </summary>
    public virtual void VerifyFileExists()
    {
        if (!Directory.Exists(FolderPath))
            Directory.CreateDirectory(FolderPath);

        if (!File.Exists(_filePath))
            File.WriteAllText(_filePath, "{}");
    }

    /// <summary>
    /// Serializes the main repository list to a JSON string
    /// </summary>
    /// <returns>A JSON string representation of the main repository</returns>
    protected virtual string Serialize()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(_mainRepo, options);
    }

    /// <summary>
    /// Writes the main repository list to the database file
    /// </summary>
    public virtual void WriteToDataBase()
    {
        File.WriteAllText(_filePath, Serialize());
    }

    // Optionally, implement a LoadFromDataBase method as needed.
}