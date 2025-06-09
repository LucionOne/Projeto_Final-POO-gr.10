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
        var input = new StringBuilder();
        int pos = 0;                      // our own cursor position in the buffer
        Console.CursorVisible = true;

        // Initial cursor placement
        Console.SetCursorPosition(X, Y);

        while (true)
        {
            var key = Console.ReadKey(intercept: true);

            // ENTER finishes
            if (key.Key == ConsoleKey.Enter)
                break;

            
            // BACKSPACE: only if there's something to delete
            if (key.Key == ConsoleKey.Backspace)
            {
                if (pos == 0)
                {
                    Console.SetCursorPosition(X, Y);
                }
                else
                {
                    pos--;
                    input.Length--;

                    Console.SetCursorPosition(X + pos, Y);
                    Console.Write(' ');
                    Console.SetCursorPosition(X + pos, Y);
                }
            }

            else if (key.KeyChar != '\0'            // a real char
            && input.Length < MaxLength            // within max length
            && (CharFilter == null || CharFilter(key.KeyChar)))
            {
                // insert the char
                input.Append(key.KeyChar);

                // write it at the correct spot
                Console.SetCursorPosition(X + pos, Y);
                Console.Write(key.KeyChar);

                pos++;
            }
            // you can handle arrows/esc here if you like
        }

        Console.CursorVisible = false;
        return input.ToString();
    }
}
