// using Controller;

// using System.ComponentModel.DataAnnotations;

namespace View;

public class HomeView
{

    //Environment.NewLine
    private string MenuString = string.Join("\n", new[] {
            "=============================",
            "|       W E L C O M E       |",
            "+---------------------------+",
            "|01| Start Game             |",
            "|02| Load Game              |",
            "|03| Player Management      |",
            "|04| View db                |",
            "+---------------------------+",
            "|00| Exit                   |",
            "============================="});


    public void Menu()
    {
        Console.WriteLine(MenuString);
    }


    public int Input()
    {
        int number;
        while (true)
        {
            string rawInput = Console.ReadLine() ?? "0";

            if (rawInput == "")
            { rawInput = "0"; }
            
            bool valid = int.TryParse(rawInput, out number);

            if (valid)
            { return number; }
            else
            { Console.WriteLine("Invalid input. Enter a valid number:"); }
        }
    }
    public void InvalidChoice(int input)
    {
        Console.WriteLine("Invalid choice, please try again.");
        Console.ReadLine();
    }

    public void Bye()
    { Console.WriteLine("Bye!!"); }
}