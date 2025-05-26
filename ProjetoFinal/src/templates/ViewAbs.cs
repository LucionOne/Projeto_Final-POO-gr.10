

namespace Templates;

public abstract class ViewConsole
{
    public virtual T GetValidInput<T>(string prompt = "")
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