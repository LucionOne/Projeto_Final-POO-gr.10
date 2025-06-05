using System.ComponentModel.DataAnnotations;
using System.IO.Pipelines;
using Container.DTOs;
using jogo;
using Templates;

namespace View;

public class GameView : ViewBasicFunctions, IGameView
{

    public GameDto AcquireGameInfo()
    {
        DateOnly Date;
        TimeOnly HoraInicio;
        string Local;
        string TipoDeCampo;
        int QuantidadeJogadoresPorTeam;

        Console.Clear();
        Console.WriteLine("Getting Game Data");
        Console.WriteLine("");

        Console.WriteLine("The Games Title");
        string title = GetValidInput<string>(">> ");

        Console.WriteLine("What's the date?");
        Console.WriteLine($"Use current date({GetCurrentDate()})? y/n: ");
        bool GetCurrentDateQ = GetValidInput<bool>(">>  ");
        if (GetCurrentDateQ)
        { Date = GetCurrentDate(); }
        else
        { Date = GetValidInput<DateOnly>(">>  "); }

        Console.WriteLine("What's the time?");
        Console.WriteLine($"Do you want do use the current time({GetCurrentTime()})? y/n: ");
        bool GetCurrentTimeQ = GetValidInput<bool>(">>  ");
        if (GetCurrentTimeQ)
        { HoraInicio = GetCurrentTime(); }
        else
        { HoraInicio = GetValidInput<TimeOnly>(">>  "); }

        Console.WriteLine("Where is the Game?");
        Local = GetValidInput<string>(">>  ");

        Console.WriteLine("What's the type of field?");
        TipoDeCampo = GetValidInput<string>(">>  ");

        Console.WriteLine("How many in each team?");
        QuantidadeJogadoresPorTeam = GetValidInput<int>(">>  ");


        GameDto gameDto = new GameDto();
        
        gameDto.Title = title;

        gameDto.Date = Date;
        gameDto.HoraInicio = HoraInicio;

        gameDto.Local = Local;
        gameDto.TipoDeCampo = TipoDeCampo;

        Console.WriteLine(gameDto.ToString());
        Console.ReadLine();


        return gameDto;
    }

    public int GetIdForGame(List<GameDto> games)
    {
        ShowData(games);

        int id = 0;
        bool validId = false;
        while (!validId)
        {
            id = GetValidInput<int>("id: ");
            validId = IdExists(games, id);

            if (!validId)
            {
                Console.WriteLine("Id doesn't Exist");
            }
        }
        return id;
    }

    public void ShowGameAndOptions(GameDto game)
    {
        var str = new List<string>
        {
            "=======================================",
            "|            O P T I O N S            |",
            "+-------------------------------------+",
            "|01| Add Goal                         |",
            "|02|                                  |",
            "|03| Add player to line               |",
            "|04| Create Team                      |",
            "|05|                                  |",
            "|06|                                  |",
            "|07| Next Match                       |",
            "|08|                                  |",
            "|09| Edit Game Info                   |",
            "+-------------------------------------+",
            "|00| Exit Game                        |",
            "=======================================",
        };
        Console.WriteLine(string.Join("\n", str));
    }

    public (bool team, int score) AcquireGoalInfo(string homeName = "Home", string AdvName = "Visitant")
    {
        var str = new List<string>
        {
            "=======================================",
            "|         W H O   S C O R E D ?       |",
            "+-------------------------------------+",
           $"|01| {homeName}{new string(' ', 33 - homeName.Length)}|", // 33
           $"|02| {AdvName}{new string(' ', 33 - AdvName.Length)}|",
            "=======================================",
        };
        var teamQuestion = string.Join("\n", str);

        int teamInt = GetChoice(">> ", teamQuestion, 1, 2, true);

        var team = teamInt == 1;

        var goalStr = new List<string>
        {
            "=======================================",
            "|   H O W   M A N Y   P O I N T S ?   |",
            "=======================================",
        };
        var goalQuestion = string.Join("\n", goalStr);

        int score = GetValidInput<int>(">> ", goalQuestion, true, 1);

        return (team, score);

        
    }    


    #region Support Methods

    private void ShowData(List<GameDto> games)
    {
        Console.WriteLine("|ID\tName\tDate - Time\tLocal");
        foreach (GameDto game in games)
        {
            Console.WriteLine($"|{game.Id}\t{game.Title}\t{game.Date} - {game.HoraInicio}\t{game.Local}");
        }
    }

    private bool IdExists(List<GameDto> games, int id)
    {
        return games.Any(x => x.Id == id);
    }

    private DateOnly GetCurrentDate()
    {
        return DateOnly.FromDateTime(DateTime.Now);
    }

    private TimeOnly GetCurrentTime()
    {
        return TimeOnly.FromDateTime(DateTime.Now);
    }

    #endregion
}