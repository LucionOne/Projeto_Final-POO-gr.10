using Models;

namespace Models;

public enum EventType
{
    Goal,
    Foul,
    YellowCard,
    RedCard,
    Injury,
    Other
}

public class Event
{

    public int PlayerId;
    public EventType Type { get; set; }
    public DateTime Time { get; set; }
    public string Description = "No Description";

    public string TypeString => Type.ToString();

    public Event(int playerId, EventType type, DateTime time)
    {
        PlayerId = playerId;
        Time = time;
        Type = type;
    }

    public Event(int playerId, EventType type, DateTime time, string description)
    {
        PlayerId = playerId;
        Time = time;
        Type = type;
        Description = description ?? string.Empty;
    }

    public Event()
    {
        Type = EventType.Goal;
    }
    
    public Event(Event package)
    {
        if (package == null)
        {
            throw new ArgumentNullException(nameof(package), "Event cannot be null");
        }
        
        PlayerId = package.PlayerId;
        Time = package.Time;
        Type = package.Type;
        Description = package.Description ?? string.Empty;
    }
}