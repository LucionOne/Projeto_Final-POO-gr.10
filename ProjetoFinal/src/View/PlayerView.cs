using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Security;
using Container.DTOs;
using Templates;

namespace View;

public class PlayerView : ViewBasicFunctions
{
    public int MainMenu()
    {
        var menulist = new List<string>
        {
            "=============================",
            "|        PLAYER MENU        |",
            "+---------------------------+",
            "|01| Create Player          |",
            "|02| Edit Player            |",
            "|03| Delete Player          |",
            "|04| List Players           |",
            "|05| Save Changes           |",
            "+---------------------------+",
            "|00| Exit                   |",
            "=============================",
        };
        var menu = string.Join("\n", menulist);

        var choice = GetChoice(
            prompt: ">> ",
            menu: menu,
            minimum: 0,
            maximum: 5,
            clear: true
        );
        return choice;
    }

    public PlayerDto GetPlayerInput(PlayerDto? playerComp = null)
    {
        string name;
        int age;
        string positionPrompt;
        Console.WriteLine("Enter player details:");

        if (playerComp != null)
        {
            name = GetValidInput<string>($"Name ({playerComp.Name}): ", clear: true);
            age = GetValidInput<int>($"Age({playerComp.Age}): ", clear: true);
            positionPrompt = $"Position ({playerComp.Position}): ";
        }
        else
        {
            name = GetValidInput<string>("Name: ", clear: true);
            age = GetValidInput<int>("Age: ", clear: true);
            positionPrompt = $"Position: ";
        }
        var isValid = false;

        int position = -1;
        var expPosition = string.Join("\n", new List<string>
        {
            "===================",
            "|00| Other        |",
            "|01| Goalkeeper   |",
            "|02| Defender     |",
            "|03| Attacker     |",
            "===================",
        });
        
        while (!isValid)
        {
            position = GetValidInput<int>(positionPrompt, expPosition, clear: true);

            if (position < 0 || position > 3)
            {
                Console.WriteLine("Invalid position. Please enter a number between 0 and 3.");
            }
            else
            {
                isValid = true;
            }
        }

        return new PlayerDto
        {
            Name = name,
            Age = age,
            Position = position,
        };
    }

    public bool DisplayPlayerList(List<PlayerDto> players, out string stringOutput, bool print = true)
    {

        if (players == null || !players.Any())
        {
            stringOutput = "No players found.";

            if (print) Console.WriteLine(stringOutput);

            return false;
        }

        var stringList = new List<string>
        {
            "====================================================",
            "|              P L A Y E R   L I S T               |",
            "+--------------------------------------------------+",
            "|ID   | Name                | Age  | Position      |",
            "+-----+---------------------+------+---------------+",
        };

        var end = new List<string>
        {
            "+-----+---------------------+------+---------------+",
            "|ID   | Name                | Age  | Position      |",
            "+--------------------------------------------------+",
            "|              P L A Y E R   L I S T               |",
            "===================================================="
        };

        string line;
        foreach (var player in players.AsEnumerable().Reverse())
        {
            line = $"|{player.Id.ToString().PadRight(5)}| {player.Name.PadRight(20)}|  {player.Age.ToString().PadRight(3)} | {player.PositionString.PadRight(14)}|";

            stringList.Add(line);
        }

        stringList.AddRange(end);

        stringOutput = string.Join("\n", stringList);

        if (print) Console.WriteLine(stringOutput);

        return true;
    }

    public int GetPlayerId(List<PlayerDto> players)
    {
        string menu;
        var success = DisplayPlayerList(players, out menu, print: false);
        if (success)
        {
            var input = GetValidInput<int>("ID: ", menu, true);

            if (players.Any(x => x.Id == input))
            {
                return input;
            }
        }
        return -1;
    }

    public string ComparerPlayers(PlayerDto oldPlayer, PlayerDto newPlayer)
    {
        var strList = new List<string>
        {
            "===========================================================",
           $"|   ID   |             {oldPlayer.Id.ToString().PadLeft(5)}    <-->    {newPlayer.Id.ToString().PadRight(5)}             |",
           $"|  Name  |{oldPlayer.Name.PadLeft(18)}    --->    {newPlayer.Name.PadRight(18)}|",
           $"|  Age   |               {oldPlayer.Age.ToString().PadLeft(3)}    --->    {newPlayer.Age.ToString().PadRight(3)}               |",
           $"|Position|        {oldPlayer.PositionString.PadLeft(10)}    --->    {newPlayer.PositionString.PadRight(10)}        |",
           $"===========================================================",
        };

        return string.Join("\n", strList);
    }

}