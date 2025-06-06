using MyRepository;
namespace Context;


public class DataContext
{
    public PlayersRepo PlayerRepo { get; set; }
    public GamesRepo GamesRepo { get; set; }
    public TeamRepo TeamRepo { get; set; }

    public DataContext(PlayersRepo PlayerRepo, GamesRepo gamesRepo, TeamRepo teamRepo)
    {
        this.TeamRepo = teamRepo;
        this.PlayerRepo = PlayerRepo;
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
            PlayerRepo.WriteToDataBase();
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