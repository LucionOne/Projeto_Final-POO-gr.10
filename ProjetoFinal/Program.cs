using Controller;
using View;

Console.Clear();

bool isRunning = true;
while (isRunning)
{
    GameView gameView = new GameView();
    gameView.AcquireGameInfo();
    // HomeController home = new HomeController();
    // home.BeginInteraction();




    Console.ReadLine();
    isRunning = false;
}