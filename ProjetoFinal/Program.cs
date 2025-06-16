using Controller;
using Context;
using MyRepository;
using View;
using VS;

#pragma warning disable CA1416 // Validate platform compatibility

public class Program
{

    // public static void Main() { }

    public static void Main()
    {
        Console.Clear();

        // Console.BufferHeight = 60;

        Console.InputEncoding = System.Text.Encoding.UTF8;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        DataContext data = LoadFiles();
        VibeShell vibe = new();


        VibeHomeView _view = new(vibe);


        var homeController = new HomeController(data, _view);

        bool isRunning = true;
        while (isRunning)
        {
            homeController.BeginInteraction();

            data.SaveDataBase();

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