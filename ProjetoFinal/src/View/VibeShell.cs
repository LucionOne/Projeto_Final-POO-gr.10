

namespace VS;

public class VibeShell
{

    public int WindowSize = 102;
    public int FillSize { get { return WindowSize - 2; } }

    public List<string> Header = new();
    public List<string> PageInfo = new();
    public List<string> MainWindow { get { return JoinViews(MainView, SecView); } }
    public List<string> InfBar = new();


    // public int MainViewSize;
    // public int SecViewSize;
    public List<string> MainView = new();
    public List<string> SecView = new();


    public VibeShell()
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
            $"{"MainView".PadRight(50)}",
        };

        SecView = new List<string>
        {
            $"{"SecView".PadRight(49)}",
        };

        InfBar = new List<string>
        {
            $"{"infBar".PadRight(FillSize)}",
        };
    }



    public void Render()
    {
        Console.Clear();

        Console.WriteLine(new string('=', WindowSize));
        
        foreach (var section in new[] { Header, PageInfo, MainWindow, InfBar })
        {
            Console.WriteLine(new string('-', WindowSize));
            foreach (var line in section)
            {
                Console.WriteLine($"|{line}|");
            }
        }
            Console.WriteLine(new string('=', WindowSize));
    }


    public void ChangeHeader(List<string> newHeader, bool render = true)
    {
        Header = PadLineRight(newHeader,FillSize);
        if (render) Render();
    }

    public void ChangePageInfo(List<string> newPageInfo, bool render = true)
    {
        PageInfo = PadLineRight(newPageInfo, FillSize);
        if (render) Render();
    }

    public void ChangeMainView(List<string> newMainView, bool render = true)
    {
        MainView = PadLineRight(newMainView, 50);
        if (render) Render();
    }

    public void ChangeSecView(List<string> newSecView, bool render = true)
    {
        SecView = PadLineRight(newSecView, 49);
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
                viewB.Add($"|{new string(' ', 49)}|");
            }
            return (viewA, viewB);
        }
        else if (difference < 0)// viewB bigger
        {
            for (var x = difference; x < 0; x++)
            {
                viewA.Add($"|{new string(' ', 50)}");
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
}

public class Wrt
{
    public string str = string.Empty;
    public ConsoleColor ForeColor;
    public ConsoleColor BackColor;
}