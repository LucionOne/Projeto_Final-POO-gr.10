using View;

namespace Controller;

public class HomeController
{

    private HomeView console;

    private Dictionary<int, Action> UserActions;


    public HomeController()
    {
        console = new HomeView();
        UserActions = new Dictionary<int, Action>
        {
            { 1, Option1 },
            { 2, Option2 },
            { 3, Option3 },
            { 4, Option4 }
        };
    }


    public void BeginInteraction()
    {
        bool isRunning = true;
        while (isRunning)
        {
            console.Menu();
            int choice = console.Input();
            isRunning = HandleUserChoice(choice);
        }
    }


    private bool HandleUserChoice(int input)
    {

        if (input == 0)
        {
            console.Bye();
            return false;
        }

        if (UserActions.TryGetValue(input, out var action))
            { action(); }
        else
            { console.InvalidChoice(input); }

        return true;
    }


    public void Option1()
    {
        Console.WriteLine("Option 1");
        Console.ReadLine();
    }

    public void Option2()
    {
        Console.WriteLine("Option 2");
        Console.ReadLine();
    }

    public void Option3()
    {
        Console.WriteLine("Option 3");
        Console.ReadLine();
    }

    public void Option4()
    {
        Console.WriteLine("Option 4");
        Console.ReadLine();
    }
}

    // private bool HandleUserChoice(int input)
    // {
    //     switch (input)
    //     {
    //         case 0:
    //             console.Bye();
    //             return false;
    //         case 1:
    //             Console.WriteLine("You chose option 1");
    //             break;
    //         case 2:
    //             Console.WriteLine("You chose option 2");
    //             // Add logic for option 2
    //             break;
    //         case 3:
    //             Console.WriteLine("You chose option 3");
    //             // Add logic for option 3
    //             break;
    //         case 4:
    //             Console.WriteLine("You chose option 4");
    //             // Add logic for option 4
    //             break;
    //         default:
    //             Console.WriteLine("Invalid choice, please try again.");
    //             break;
    //     }
    // }