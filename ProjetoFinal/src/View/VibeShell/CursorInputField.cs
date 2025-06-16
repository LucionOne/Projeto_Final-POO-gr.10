using System.Text;

namespace VS;

public class CursorInputField
{
    public int X { get; }
    public int Y { get; }
    public int MaxLength { get; }
    public Func<char, bool>? CharFilter { get; }

    public CursorInputField(int x, int y, int maxLength = int.MaxValue, Func<char, bool>? charFilter = null)
    {
        X = x;
        Y = y;
        MaxLength = maxLength;
        CharFilter = charFilter;
    }

    public string ReadInput()
    {

        int pos = 0; // position in the Box

        Console.CursorVisible = true;

        var inputBuilder = new StringBuilder();

        // Initial cursor placement
        Console.SetCursorPosition(X, Y);

        while (true)
        {
            var key = Console.ReadKey(intercept: true);

            // ENTER
            if (key.Key == ConsoleKey.Enter)
                break;

            // BACKSPACE
            if (key.Key == ConsoleKey.Backspace)
            {
                if (pos == 0/* && inputBuilder.Length == 0*/)
                {
                    Console.SetCursorPosition(X, Y);
                }
                else
                {
                    pos--;
                    inputBuilder.Length--;

                    Console.SetCursorPosition(X + pos, Y);
                    Console.Write(' ');
                    Console.SetCursorPosition(X + pos, Y);
                }
            }

            else if (key.KeyChar != '\0'                            // not a null character
            && inputBuilder.Length < MaxLength                     // within max length
            && (CharFilter == null || CharFilter(key.KeyChar)))   // c => char.isDigit(c)   or    c => "ABCabc".Contains(c)
            {
                inputBuilder.Append(key.KeyChar);

                Console.SetCursorPosition(X + pos, Y);
                Console.Write(key.KeyChar);

                pos++;
            }
        }

        Console.CursorVisible = false;
        return inputBuilder.ToString();
    }
}
