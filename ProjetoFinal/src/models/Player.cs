using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Templates;
using Container.DTOs;

namespace Models;

// Gestão de Jogadores
// CRUD[1] dos jogadores;
// Os atributos principais são nome, idade e posição;
// As posições podem ser: goleiros, defesa e ataque;
// Este cadastro deve contar todas as pessoas que fazem parte da “associação” ou “grupo” de pessoas que normalmente jogam.  - ????????
// Cada jogador de ter um código único usado como identificador, a exemplo o RA que é usado na Universidade.

public class Player : IModel
{


    public enum PlayerPosition
    {
        Any,
        Goalkeepers,
        Defender,
        Attackers,
        Corrupted
    }

    // private attributes
    private string? _name;

    private int _age;

    private int _position;

    private int _id;

    // public attributes
    public string Name
    {
        get { return _name ?? string.Empty; }
        set { _name = value ?? string.Empty; }
    }

    public int Age { get { return _age; } set { _age = value; } }

    public int Id { get { return _id; } set { _id = value; } }

    public int Position
    {
        get { return _position; }

        set
        {
            if (_position >= 0 || _position <= 3)
            {
                _position = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Posição deve ser de 0 a 3.");
            }
        }
    }

    [JsonIgnore]
    public string PositionString
    {
        get
        {
            return _position switch
            {
                0 => "Unknown",
                1 => "Goleiro",
                2 => "Defesa",
                3 => "Ataque",
                _ => throw new ArgumentOutOfRangeException("PosicaoString: _posição inválida.")
            };
        }
    }

    public Player() { }

    // constructor
    public Player(string nome, int idade, int posicao)
    {
        Name = nome;
        Age = idade;
        Position = posicao;
    }

    public Player(PlayerDto dto)
    {
        Id = dto.Id;
        Name = dto.Name;
        Age = dto.Age;
        Position = dto.Position;
    }


    // methods
    public override string ToString()
    {
        return $"ID: {Id}\tNome: {Name}\tIdade: {Age}\tPosição: {PositionString}";
    }

    public string ToStringAlt()
    {
        return $"ID: {Id}\nNome: {Name}\nIdade: {Age}\nPosição: {PositionString}";
    }
}
