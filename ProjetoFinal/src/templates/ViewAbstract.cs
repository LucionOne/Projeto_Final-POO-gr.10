

using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks.Sources;

namespace Templates;

public abstract class ViewBasicFunctions : IView
{
    public virtual T GetValidInput<T>(string prompt = "", string? menu = null, bool clear = false, T? _default = default)
    {
        while (true)
        {
            if (clear) { Console.Clear(); } 
            if (!(menu == null)) { Console.WriteLine(menu); }
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (string.IsNullOrEmpty(input) && (!EqualityComparer<T>.Default.Equals(_default, default))) 
            {
                return _default ?? throw new Exception("_default somehow is null");
            }

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


    public virtual int GetChoice(string prompt = ">> ", string? question = null, int minimum = 0, int maximum = 99, bool clear = false, string _default = "0")
    {
        string? warning = null;
        int number;
        while (true)
        {
            if (clear) { Console.Clear(); }

            if (!(question == null)) { Console.WriteLine(question); }

            if (!(warning == null)) { Console.WriteLine(warning); }

            Console.Write(prompt);
            string rawInput = Console.ReadLine() ?? _default;

            if (rawInput == "")
            { rawInput = _default; }

            bool validNumber = int.TryParse(rawInput, out number);
            bool validChoice = (number <= maximum) && (number >= minimum);

            if (validNumber && validChoice)
            { return number; }

            else if (!validNumber)
            { warning = "Invalid Input, Needs to be a full number, Try Again"; }
            else if (!validChoice)
            { warning = $"Invalid Choice, Needs to be between {minimum} & {maximum}, Try Again"; }


        }
    }
}