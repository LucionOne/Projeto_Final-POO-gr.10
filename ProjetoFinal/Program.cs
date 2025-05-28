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
        
        bool isRunning = true;
        while (isRunning)
        {
            HomeView _view = new();
            var homeController = new HomeController(data, _view);
            homeController.BeginInteraction();


            Console.ReadLine();
            isRunning = false;
        }
    }

    private static DataContext LoadFiles()
    {
        PlayersRepo PlayersRepo = new PlayersRepo("Jogadores.json").DataBaseStarter();
        GamesRepo gamesRepo = new GamesRepo("Games.json").DataBaseStarter();

        DataContext dataContext = new DataContext(PlayersRepo, gamesRepo);
        return dataContext;
    }
}