using jogador;

namespace Model;

public abstract class Event
{
    public int Minute { get; set; }
    public Player PlayerInvolved { get; set; }

    public Event(int minute, Player player)
    {
        Minute = minute;
        PlayerInvolved = player;
    }

    // public abstract void ApplyEvent(Match match);
}