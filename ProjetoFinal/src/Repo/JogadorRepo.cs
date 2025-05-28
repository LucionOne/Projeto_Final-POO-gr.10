using jogador;
using System;
using System.IO;
using System.Text.Json;
using Templates;

namespace MyRepository;

public class JogadorRepo : RepoAbstract<Jogador>
{
    public JogadorRepo(string fileName) : base(fileName) {}

    // public static JogadorRepo LoadFromDataBase(string fileName)
    // {
    //     string filePath = Path.Combine("DataBase", fileName);
    //     if (!File.Exists(filePath))
    //         throw new FileNotFoundException($"File not found: {filePath}");

    //     string jsonString = File.ReadAllText(filePath);
    //     JogadorRepo? temp = JsonSerializer.Deserialize<JogadorRepo>(jsonString);
    //     if (temp == null)
    //         throw new Exception($"Failed to deserialize json {filePath}");
    //     return temp;
    // }
}