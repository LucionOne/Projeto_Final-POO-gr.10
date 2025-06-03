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

    public void SaveDataBase()
    {
        try
        {
            JogadorRepo.WriteToDataBase();
            GamesRepo.WriteToDataBase();
        }
        catch
        {
            throw new Exception("Couldn't write to data base");
        }
    }
}