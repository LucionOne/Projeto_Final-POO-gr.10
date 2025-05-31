using Controller;
using Context;
using MyRepository;
using View;

public class Program
{
    public static void Main()
    {
        Console.Clear();
      
        Console.WriteLine("Start");
        DataContext data = LoadFiles();

        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("1° Loop");
            HomeView _view = new();
            var homeController = new HomeController(data, _view);
            homeController.BeginInteraction();

            
            Console.ReadLine();
            isRunning = false;
        }
    }
    
    //KILL ME AND DO IT NOW ┻━┻ ︵ヽ(`Д´)ﾉ︵ ┻━┻ VAR THIS = PAIN;

    // private static DataContext LoadFiles()
    // {
    //     PlayersRepo PlayersRepo = new PlayersRepo("Jogadores.json").DataBaseStarter();
    //         Console.WriteLine("----------1.1----------");
    //     GamesRepo gamesRepo = new GamesRepo("Games.json").DataBaseStarter();
    //         Console.WriteLine("----------1.2----------");

    //     DataContext dataContext = new DataContext(PlayersRepo, gamesRepo);
    //         Console.WriteLine("----------1.3----------");
    //     return dataContext;
    // }

    private static DataContext LoadFiles()
    {
        PlayersRepo playersRepoFabric = new PlayersRepo();
        GamesRepo gamesRepoFabric = new GamesRepo();

        playersRepoFabric.ConfirmFileAndFolderExistence();
        gamesRepoFabric.ConfirmFileAndFolderExistence();

        // playersRepoFabric.WriteToDataBase();
        // gamesRepoFabric.WriteToDataBase();

        var playersRepo = playersRepoFabric.DataBaseStarter();
        var gamesRepo = gamesRepoFabric.DataBaseStarter();

        DataContext dataContext = new(playersRepo, gamesRepo);

        return dataContext;
    }

}