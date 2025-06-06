using MyRepository;
namespace Context;


public class DataContext
{
    public PlayersRepo JogadorRepo { get; set; }
    public GamesRepo GamesRepo { get; set; }
    public TeamRepo TeamRepo { get; set; }

    public DataContext(PlayersRepo jogadorRepo, GamesRepo gamesRepo, TeamRepo teamRepo)
    {
        this.TeamRepo = teamRepo;
        this.JogadorRepo = jogadorRepo;
        this.GamesRepo = gamesRepo;
    }

    public void SaveDataBase()
    {
        try
        {
            TeamRepo.WriteToDataBase();
        }
        catch
        {
            throw new Exception("Couldn't write TeamRepo to its DataBase");
        }
        try
        {
            JogadorRepo.WriteToDataBase();
        }
        catch
        {
            throw new Exception("Couldn't write JogadorRepo to its DataBase");
        }
        try
        {
            GamesRepo.WriteToDataBase();
        }
        catch
        {
            throw new Exception("Couldn't write GamesRepo to its DataBase");
        }
    }
}