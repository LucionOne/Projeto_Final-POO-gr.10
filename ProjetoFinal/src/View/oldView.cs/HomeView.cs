// using Controller;

// using System.ComponentModel.DataAnnotations;

using Templates;

namespace View;

public class HomeView : ViewBasicFunctions
{

    //Environment.NewLine
    private string MenuString = string.Join("\n", new[] {
            "=============================",
            "|       W E L C O M E       |",
            "+---------------------------+",
            "|01| Start Game             |",
            "|02| Load Game              |",
            "|03| Player Management      |",
            "|04| Team Management        |",
            "+---------------------------+",
            "|00| Exit                   |",
            "============================="});


    public void Menu()
    {
        Console.WriteLine(MenuString);
    }


    public void InvalidChoice<T>(T error)
    {
        Console.WriteLine("Invalid choice, please try again.");
        Console.ReadLine();
    }

    public void Bye()
    { Console.WriteLine("Bye!!"); }
}