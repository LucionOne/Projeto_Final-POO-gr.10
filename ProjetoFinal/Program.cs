using Controller;

public class Program
{
    public static void Main()
    {
        Console.Clear();

        bool isRunning = true;
        while (isRunning)
        {
            var homeController = new HomeController();
            homeController.BeginInteraction();





            Console.ReadLine();
            isRunning = false;
        }
    }
}