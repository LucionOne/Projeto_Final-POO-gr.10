using MyRepository;
namespace Context;

public class DataContext
{
    public PlayersRepo JogadorRepo { get; set; }
    public GamesRepo GamesRepo { get; set; }

    public DataContext(PlayersRepo jogadorRepo, GamesRepo gamesRepo)
    {
        this.JogadorRepo = jogadorRepo;
        this.GamesRepo = gamesRepo;
    }
}