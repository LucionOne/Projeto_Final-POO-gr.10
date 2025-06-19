using Models;

namespace Container.DTOs;

public class PlayerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public int Position { get; set; }
    public List<Event> Events { get; set; } = new();
    public string PositionString => Position switch
    {
        0 => "Any",
        1 => "Goalkeeper",
        2 => "Defender",
        3 => "Attacker",
        _ => "Corrupted"
    };
    public string PositionStringMini => Position switch
    {
        0 => "Any",
        1 => "Goal",
        2 => "Def",
        3 => "Att",
        _ => "???"
    };


    public PlayerDto() { }

    public PlayerDto(Player package)
    {
        Id = package.Id;
        Name = package.Name;
        Age = package.Age;
        Position = package.Position;
        Events = package.Events;
    }
    public PlayerDto(int id, string name, int age, int position)
    {
        Id = id;
        Name = name;
        Age = age;
        Position = position;
    }
    public PlayerDto(string name, int age, int position)
    {
        Name = name;
        Age = age;
        Position = position;
    }
}
    