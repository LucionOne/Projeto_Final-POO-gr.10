using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace jogador;

public class Jogador
{
    // private attributes
    private string? _nome;
    private int _idade;
    private int _posicao;
    private int _id;

    // public attributes
    public string Nome 
    {
        get{return _nome ?? string.Empty;}
        set{_nome = value ?? string.Empty;}
    }
    public int Idade { get{return _idade;} set{_idade = value;}}
    public int Posicao 
    {   
        get{return _posicao;}
        
        set
        {
            if (_posicao >= 0 || _posicao <= 3)
            {
                _posicao = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Posição deve ser entre 0 e 3.");
            }
        }
    }
    [JsonIgnore]
    public string PosicaoString
    {
        get
        {
            return _posicao switch
            {
                0 => "Unknown",
                1 => "Goleiro",
                2 => "Defesa",
                3 => "Ataque",
                _ => throw new ArgumentOutOfRangeException("PosicaoString: _posição inválida.")
            };
        }
    }


    // public int Id { get{return _id;} set{_id = value;}}


    // constructor
    public Jogador(string nome, int idade, int posicao)
    {
        Nome = nome;
        Idade = idade;
        Posicao = posicao;
    }
    // methods
    public string GetDatasString()
    {
        return $"Nome: {Nome}\tIdade: {Idade}\tPosição: {PosicaoString}";
    }
    public string Serialize()
    {
        return JsonSerializer.Serialize(this);
    }
}
