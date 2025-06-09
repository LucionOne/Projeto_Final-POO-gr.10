using Controller;
using Context;
using MyRepository;
using View;
using VS;

public class Program
{
    public static void Main()
    {
        // Console.Clear();

        // Console.OutputEncoding = System.Text.Encoding.UTF8;

        // var vibe = new VibeShell();
        // vibe.SetSize(52);
        // // vibe.Clear(true);
        // vibe.Render();
        // // Example usage from a view or controller class:
        // var options = new List<string> { "Option 1", "Option 2", "Option 3" };
        // var descriptions = new List<string> { "Description 1", "Description 2", "Description 3" };

        // int choice = vibe.HandleMenu(options, descriptions, defaultIndex: 0,0.8f);

        // vibe.ChangeHeader(new List<string>
        // {
        //     $"{choice}"
        // });
        // Console.Clear();
        // var nameField = new CursorInputField(10, 5, maxLength: 15);
        // string name = nameField.ReadInput();
        // Console.WriteLine();
        // Console.WriteLine();
        // Console.WriteLine(name);


        // var intField = new CursorInputField(10, 6, 5, c => char.IsDigit(c));
        // string digitsOnly = intField.ReadInput();

        Console.InputEncoding = System.Text.Encoding.UTF8;

        List<string> header = new()
        {
            "Welcome to the VibeShell!",
            "This is a simple console application."
        };
        List<string> pageInfo = new()
        {
            "Page 1 of 1       Press enter to quit.",  
        };
        List<string> mainView = new()
        {
            "This is the main view.",
            "You can add more content here."
        };
        List<string> secView = new()
        {
            "This is the secondary view.",
            "You can add more content here as well."
        };
        List<string> infBar = new()
        {
            "Info: This is an information bar.",
            "You can display additional information here."
        };


        VibeShell vibe = new (header, pageInfo, mainView, secView, infBar,102, 0.6f, 10);
        vibe.Render();
    }




    // public static void Main()
    // {
    //     Console.Clear();

    //     Console.InputEncoding = System.Text.Encoding.UTF8;
    //     Console.OutputEncoding = System.Text.Encoding.UTF8;

    //     DataContext data = LoadFiles();
    //     HomeView _view = new();

    //     var homeController = new HomeController(data, _view);

    //     bool isRunning = true;
    //     while (isRunning)
    //     {
    //         homeController.BeginInteraction();

    //         data.SaveDataBase();

    //         Console.ReadLine();
    //         isRunning = false;
    //     }
    // }


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