using Controller;
using Context;
using MyRepository;
using View;
using VS;

public class Program
{
    // public static void Main()
    // {
    //     var vibe = new VibeShell();
    //     vibe.Render();
    //     Console.ReadLine();
    //     var Header = new List<string>
    //     {
    //         "Hello World!"
    //     };
    //     vibe.ChangeHeader(Header);
    // }
    public static void Main()
    {
        Console.Clear();

        DataContext data = LoadFiles();
        HomeView _view = new();

        var homeController = new HomeController(data, _view);

        bool isRunning = true;
        while (isRunning)
        {
            homeController.BeginInteraction();

            data.SaveDataBase();

            Console.ReadLine();
            isRunning = false;
        }
    }


    private static DataContext LoadFiles()
    {
        PlayersRepo playersRepoFabric = new PlayersRepo();
        GamesRepo gamesRepoFabric = new GamesRepo();
        TeamRepo teamRepoFabric = new TeamRepo();

        playersRepoFabric.ConfirmFileAndFolderExistence();
        gamesRepoFabric.ConfirmFileAndFolderExistence();
        teamRepoFabric.ConfirmFileAndFolderExistence();

        var playersRepo = playersRepoFabric.LoadFromDataBase();
        var gamesRepo = gamesRepoFabric.LoadFromDataBase();
        var teamRepo = teamRepoFabric.LoadFromDataBase();

        DataContext dataContext = new(playersRepo, gamesRepo, teamRepo);

        return dataContext;
    }


}