using Controller;
using Context;
using MyRepository;
using View;

public class Program
{

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

        playersRepoFabric.ConfirmFileAndFolderExistence();
        gamesRepoFabric.ConfirmFileAndFolderExistence();

        var playersRepo = playersRepoFabric.LoadFromDataBase();
        var gamesRepo = gamesRepoFabric.LoadFromDataBase();

        DataContext dataContext = new(playersRepo, gamesRepo);

        return dataContext;
    }

    // private static bool SaveDataBase(DataContext data)
    // {
    //     try
    //     {
    //         data.GamesRepo.WriteToDataBase();
    //         data.JogadorRepo.WriteToDataBase();
    //         return true;
    //     }
    //     catch
    //     {
    //         return false;
    //     }
    // }

}