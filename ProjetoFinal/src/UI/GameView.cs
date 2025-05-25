
using DTOs;

namespace View;

public class GameView
{



    public GameDto AcquireGameInfo()
    {
        // Maybe later
        // List<string> GameOverview = new List<string>
        // ([
        //     "=================================================",
        //     "|           G A M E   O V E R V I E W           |",
        //     "+-----------------------------------------------+",
        //     "|Date:                                          |", //47 spaces + 2*|
        //     "|Time:                                          |",
        //     "||"
        // ]);
        // GameDto gameDto = new GameDto();
        // Dictionary<string, bool> completedInfo = new Dictionary<string, bool>
        // {
        //     {"Date", false},
        //     {"Time", false},
        // };

        GameDto gameDto = new GameDto();
        Console.Clear();
        Console.WriteLine("Getting game Data");
        Console.WriteLine("");
        Console.WriteLine("What's the date?");
        Console.WriteLine($"Use current date({GetCurrentDate()})? y/n: ");
        bool GetCurrentDateQ = GetValidInput<bool>(">>  ");
        if (GetCurrentDateQ)
        { gameDto.Date = GetCurrentDate(); }
        else
        { gameDto.Date = GetValidInput<DateOnly>(">>  "); }

        Console.WriteLine("What's the time?");
        Console.WriteLine($"Do you want do use the current time({GetCurrentTime()})? y/n: ");
        bool GetCurrentTimeQ = GetValidInput<bool>(">>  ");
        if (GetCurrentTimeQ)
        { gameDto.HoraInicio = GetCurrentTime(); }
        else
        { gameDto.HoraInicio = GetValidInput<TimeOnly>(">>  "); }

        Console.WriteLine("Where is the Game?");
        gameDto.Local = GetValidInput<string>(">>  ");

        Console.WriteLine("What's the type of field?");
        gameDto.TipoDeCampo = GetValidInput<string>(">>  ");

        Console.WriteLine("How many in each team?");
        gameDto.QuantidadeJogadoresPorTeam = GetValidInput<int>(">>  ");

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


    public T GetValidInput<T>(string prompt = "")
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (typeof(T) == typeof(int)) // get INT
            {
                if (int.TryParse(input, out int intValue))
                    return (T)(object)intValue;
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }

            else if (typeof(T) == typeof(string)) // get STRING
            {
                if (!string.IsNullOrWhiteSpace(input))
                    return (T)(object)input;
                Console.WriteLine("Input cannot be empty. Please enter a valid string.");
            }

            else if (typeof(T) == typeof(bool)) // get BOOL from y/n
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    input = input.Trim().ToLower();
                    if (input == "yes" || input == "y")
                        return (T)(object)true;
                    if (input == "no" || input == "n")
                        return (T)(object)false;
                }
                Console.WriteLine("Invalid input. Please enter 'yes'/'y' or 'no'/'n'.");
            }

            else if (typeof(T) == typeof(DateOnly)) // get DATE
            {
                if (!string.IsNullOrWhiteSpace(input) && DateOnly.TryParse(input, out DateOnly dateValue))
                    return (T)(object)dateValue;
                Console.WriteLine("Invalid input. Please enter a valid date (e.g., yyyy-MM-dd).");
            }

            else if (typeof(T) == typeof(TimeOnly)) // get TIME
            {
                if (!string.IsNullOrWhiteSpace(input) && TimeOnly.TryParse(input, out TimeOnly timeValue))
                    return (T)(object)timeValue;
                Console.WriteLine("Invalid input. Please enter a valid time (e.g., HH:mm).");
            }

            else
            {
                throw new NotSupportedException("Type not supported.");
            }
        }
    }







}