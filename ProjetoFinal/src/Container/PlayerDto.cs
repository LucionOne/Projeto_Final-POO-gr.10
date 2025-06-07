using Models;

namespace Container.DTOs;

public class PlayerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public int Position { get; set; }
    public string PositionString => Position switch
    {
        0 => "Unknown",
        1 => "Goalkeeper",
        2 => "Defender",
        3 => "Attacker",
        _ => "Corrupted"
    };


    public PlayerDto() { }

    public PlayerDto(Player package)
    {
        Id = package.Id;
        Name = package.Name;
        Age = package.Age;
        Position = package.Position;
    }
}
    