using Controller;
using Context;
using MyRepository;
using View;
using VS;

#pragma warning disable CA1416 // Validate platform compatibility

public class Program
{
    // public static void Main()
    // {

    //     Console.OutputEncoding = System.Text.Encoding.UTF8;
    //     VibeShell vibe = new();
    //     vibe.Render();
    //     var choices = new List<VibeShell.SelectableItem>
    //     {
    //     new(101, "Dragons FC",    new List<string>{ "Top-tier team", "Since 2005" }),
    //     new(204, "Sharks United", new List<string>{ "Aggressive play",   "Since 2010" }),
    //     new(309, "Falcons 99",    new List<string>{ "Youth focus",       "Since 1999" }),
    //     new(412, "Phoenix SC",    new List<string>{ "Reborn champions",  "Since 2021" }),
    //     };

    //     int picked = vibe.HandleSelectById(
    //         choices,
    //         headerLines: new() { "== Delete a Team ==" },
    //         exitCode: "XX",
    //         prompt: "Type real team ID (XX to cancel):"
    //     );

    //     if (picked > 0)
    //         Console.WriteLine($"Deleting team with ID {picked}");
    //     else
    //         Console.WriteLine("Cancelled.");



    // }




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