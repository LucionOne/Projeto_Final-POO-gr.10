using DTOs;
using Templates;

namespace View;

public class GameView : ViewConsole, IGameView 
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
        gameDto.Date = Date;
        gameDto.HoraInicio = HoraInicio;
        gameDto.Local = Local;
        gameDto.TipoDeCampo = TipoDeCampo;
        gameDto.QuantidadeJogadoresPorTeam = QuantidadeJogadoresPorTeam;

        Console.WriteLine(gameDto.ToString());
        Console.ReadLine();


        return gameDto;
    }


    private DateOnly GetCurrentDate()
    {
        return DateOnly.FromDateTime(DateTime.Now);
    }


    private TimeOnly GetCurrentTime()
    {
        return TimeOnly.FromDateTime(DateTime.Now);
    }

}