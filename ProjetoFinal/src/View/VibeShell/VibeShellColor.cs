namespace VS; //need a better namespace

public class VibeShellColor //Temp name?
{

    public int WindowSize = 102;
    public int FillSize { get { return WindowSize - 2; } }
    public int MainViewFillSize;
    public int SecViewFillSize;
    private float _scale = 0.5f;
    public float Scale
    {
        get => _scale;
        set => _scale = (value >= 0 && value <= 1) ? value : 0.5f;
    }

    public List<string> Header = new();
    public List<string> PageInfo = new();
    public List<string> MainWindow { get { return JoinViews(MainView, SecView); } }
    public List<string> InfBar = new();

    public List<string> MainView = new();
    public List<string> SecView = new();


    public VibeShellColor()
    {
        Header = new List<string>
        {
            $"{"Header".PadRight(FillSize)}",
        };

        PageInfo = new List<string>
        {
            $"{"PageInfo".PadRight(FillSize)}",
        };

        MainView = new List<string>
        {
            $"{"MainView".PadRight(MainViewFillSize)}",
        };

        SecView = new List<string>
        {
            $"{"SecView".PadRight(SecViewFillSize)}",
        };

        InfBar = new List<string>
        {
            $"{"infBar".PadRight(FillSize)}",
        };
        ScaleFillSize();
    }

    public void Clear(bool extraClean)
    {
        if (extraClean)
        {
            Header = ["".PadRight(FillSize)];
            PageInfo = ["".PadRight(FillSize)];
        }
        MainView = ["".PadRight(MainViewFillSize)];
        SecView = ["".PadRight(SecViewFillSize)];
        InfBar = ["".PadRight(FillSize)];
    }

    private void ScaleFillSize()
    {
        float secScale = Scale - 1f;

        var mainTempSize = FillSize * Scale;
        var secTempSize = FillSize * secScale;

        MainViewFillSize = (int)Math.Round(mainTempSize);
        SecViewFillSize = (int)Math.Round(secTempSize);

        int diference = MainViewFillSize + SecViewFillSize - FillSize;

        if (diference != 0)
        {
            if (diference > 0) // view is bigger
            {
                SecViewFillSize -= diference;
            }
            else if (diference < 0) // fill is bigger
            {
                MainViewFillSize += -diference;
            }
        }

        SecViewFillSize -= 1; //set a space for division
    }

    public void Render()
    {
        Console.Clear();

        Console.WriteLine(new string('=', WindowSize));

        foreach (var section in new[] { Header, PageInfo, MainWindow, InfBar })
        {
            if (!Object.ReferenceEquals(section, Header))
            { Console.WriteLine(new string('-', WindowSize)); }

            foreach (var line in section)
            {
                Console.WriteLine($"|{line}|");
            }
        }

        Console.WriteLine(new string('=', WindowSize));
    }


    public void ChangeHeader(List<string> newHeader, bool render = true)
    {
        Header = PadLineRight(newHeader, FillSize);
        if (render) Render();
    }

    public void ChangePageInfo(List<string> newPageInfo, bool render = true)
    {
        PageInfo = PadLineRight(newPageInfo, FillSize);
        if (render) Render();
    }

    public void ChangeMainView(List<string> newMainView, bool render = true)
    {
        MainView = PadLineRight(newMainView, MainViewFillSize);
        if (render) Render();
    }

    public void ChangeSecView(List<string> newSecView, bool render = true)
    {
        SecView = PadLineRight(newSecView, SecViewFillSize);
        if (render) Render();
    }


    private List<string> PadLineRight(List<string> strings, int pad)
    {
        List<string> paddedList = new();
        foreach (var str in strings)
        {
            paddedList.Add(str.PadRight(pad));
        }
        return paddedList;
    }


    private List<string> JoinViews(List<string> viewA, List<string> viewB)
    {
        (viewA, viewB) = EqualizeViewsCount(viewA, viewB);

        if (viewA.Count != viewB.Count) throw new Exception("viewA needs to be equal to viewB");

        List<string> strings = new();

        for (int i = 0; i < viewA.Count; i++)
        {
            strings.Add(viewA[i] + '|' + viewB[i]);
        }

        return strings;
    }

    private (List<string> ViewA, List<string> ViewB) EqualizeViewsCount(List<string> viewA, List<string> viewB)
    {
        var difference = viewA.Count - viewB.Count;

        if (difference > 0) //viewA bigger
        {
            for (var x = difference; x > 0; x--)
            {
                viewB.Add($"|{new string(' ', SecViewFillSize)}");
            }
            return (viewA, viewB);
        }
        else if (difference < 0)// viewB bigger
        {
            for (var x = difference; x < 0; x++)
            {
                viewA.Add($"{new string(' ', MainViewFillSize)}");
            }
            return (viewA, viewB);
        }
        else
        {
            return (viewA, viewB);
        }

    }

}

public class WrtLine
{
    public List<Wrt> Line = new();

    public WrtLine(List<Wrt> line)
    {
        Line = line;
    }

    public static WrtLine operator +(WrtLine a, WrtLine b)
    {
        var combined = new List<Wrt>(a.Line);
        combined.AddRange(b.Line);
        return new WrtLine(combined);
    }

    public void RenderWrtLine(WrtLine line)
    {
        foreach (var wrt in line.Line)
        {
            Console.ForegroundColor = wrt.ForeColor;
            Console.BackgroundColor = wrt.BackColor;
            Console.Write(wrt.Text);
        }
        Console.ResetColor();
        Console.WriteLine();
    }

    public void PadRight(int totalWidth, ConsoleColor backColor = ConsoleColor.Black, char padChar = ' ', ConsoleColor foreColor = ConsoleColor.Black)
    {
        int currentLength = Line.Sum(w => w.Text.Length);
        int paddingNeeded = Math.Max(0, totalWidth - currentLength);

        if (paddingNeeded > 0)
            Line.Add(new Wrt(new string(padChar, paddingNeeded), foreColor, backColor));
    }

}

public class Wrt
{
    public string Text { get; }
    public ConsoleColor ForeColor { get; }
    public ConsoleColor BackColor { get; }

    public Wrt(string text, ConsoleColor? foreColor = null, ConsoleColor? backColor = null)
    {
        Text = text;
        ForeColor = foreColor ?? ConsoleDefaults.DefaultForeColor;
        BackColor = backColor ?? ConsoleDefaults.DefaultBackColor;
    }

    
}

public static class ConsoleDefaults
{
    public static readonly ConsoleColor DefaultForeColor = Console.ForegroundColor;
    public static readonly ConsoleColor DefaultBackColor = Console.BackgroundColor;
}