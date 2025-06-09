
// in development, not in use yet, got to make sure the controller works fine

using System.Reflection.Metadata;

namespace VS; //need a better namespace

public class VibeShell //Temp name?
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

    public int MainWindowHeight = 30;

    public List<string> Header = new();
    public List<string> PageInfo = new();
    public List<string> MainWindow { get { return JoinViews(MainView, SecView); } }
    public List<string> InfBar = new();

    public List<string> MainView = new();
    public List<string> SecView = new();






    public VibeShell()
    {
        ScaleFillSize();
        Header = new List<string>
        {
            $"{"Header"}",//.PadRight(FillSize)}",
        };

        PageInfo = new List<string>
        {
            $"{"PageInfo"}",//.PadRight(FillSize)}",
        };

        MainView = new List<string>
        {
            $"{"MainView"}",//.PadRight(MainViewFillSize)}",
        };

        SecView = new List<string>
        {
            $"{"SecView"}",//.PadRight(SecViewFillSize)}",
        };

        InfBar = new List<string>
        {
            $"{"infBar"}",//.PadRight(FillSize)}",
        };
    }

    public VibeShell(int size = 102, float scale = 0.5f, int mainWindowHeight = 30)
    {

        MainWindowHeight = mainWindowHeight;
        WindowSize = size;
        Scale = scale;
        ScaleFillSize();

        Header = new List<string>
        {
            $"{"Header"}",
        };

        PageInfo = new List<string>
        {
            $"{"PageInfo"}",
        };

        MainView = new List<string>
        {
            $"{"MainView"}",
        };

        SecView = new List<string>
        {
            $"{"SecView"}",
        };

        InfBar = new List<string>
        {
            $"{"infBar"}",
        };
    }

public VibeShell(List<string> header, List<string> pageInfo, List<string> mainView, List<string> secView, List<string> infBar, int size = 102, float scale = 0.5f, int mainWindowHeight = 30)
    {
        MainWindowHeight = mainWindowHeight;
        WindowSize = size;
        Scale = scale;
        ScaleFillSize();

        Header = PadLineRight(header, FillSize);
        PageInfo = PadLineRight(pageInfo, FillSize);
        MainView = PadLineRight(mainView, MainViewFillSize);
        SecView = PadLineRight(secView, SecViewFillSize);
        InfBar = PadLineRight(infBar, FillSize);
    }





    public void SetSize(int size)
    {
        WindowSize = size;
        ScaleFillSize();
    }







    public void Clear(bool extraClean = false, bool render = true)
    {
        if (extraClean)
        {
            Header = ["".PadRight(FillSize)];
            PageInfo = ["".PadRight(FillSize)];
        }

        MainView = ["".PadRight(MainViewFillSize)];
        SecView = ["".PadRight(SecViewFillSize)];
        InfBar = ["".PadRight(FillSize)];

        if (render) Render();
    }













    public void ScaleFillSize()
    {
        float secScale = -(Scale - 1f);

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

        SecViewFillSize -= 1; //set a space a division
    }













    public void setScale(float scale)
    {
        Scale = scale;
        ScaleFillSize();
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
    public void ChangeInfBar(List<string> newInfBar, bool render = true)
    {
        InfBar = PadLineRight(newInfBar, FillSize);
        if (render) Render();
    }

    public List<string> GetHeader()
    {
        return TrimList(Header);
    }
    public List<string> GetPageInfo()
    {
        return TrimList(PageInfo);
    }
    public List<string> GetMainView()
    {
        return TrimList(MainView);
    }
    public List<string> GetSecView()
    {
        return TrimList(SecView);
    }
    public List<string> GetInfBar()
    {
        return TrimList(InfBar);
    }












    public static List<string> TrimList(List<string> list)
    {
        List<string> trimmedList = new();

        for (int i = 0; i < list.Count; i++)
        {
            trimmedList.Add(list[i].Trim());
        }

        return trimmedList;
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
        EqualizeViewsCount(viewA, viewB);

        if (viewA.Count != viewB.Count) throw new Exception("viewA needs to be equal to viewB");

        List<string> strings = new();

        for (int i = 0; i < viewA.Count; i++)
        {
            strings.Add(viewA[i] + '|' + viewB[i]);
        }




        return strings;
    }



    private void EqualizeViewsCount(List<string> viewA, List<string> viewB)
    {
        var difference = viewA.Count - viewB.Count;



        if (difference > 0) //viewA bigger
        {
            for (var x = difference; x > 0; x--)
            {
                viewB.Add($"{new string(' ', SecViewFillSize)}");
            }
        }

        else if (difference < 0)// viewB bigger
        {
            for (var x = difference; x < 0; x++)
            {
                viewA.Add($"{new string(' ', MainViewFillSize)}");
            }
        }

        int minHeightDif = MainWindowHeight - viewA.Count;

        if (minHeightDif > 0)
        {
            for (var x = minHeightDif; x > 0; x--)
            {
                viewA.Add($"{new string(' ', MainViewFillSize)}");
                viewB.Add($"{new string(' ', SecViewFillSize)}");
            }
        }

    }



























public int HandleMenu(
    List<string> options,
    List<string>? descriptions = null,
    int defaultIndex = 0,
    float? menuScale = null,
    bool renderEachChange = true
)
    {
        // 1) If the caller gave us a new scale, apply it now:
        if (menuScale.HasValue)
            setScale(menuScale.Value);  // you already call ScaleFillSize()

        // 2) Ensure descriptions aligns with options
        if (descriptions == null || descriptions.Count != options.Count)
            descriptions = Enumerable.Repeat(string.Empty, options.Count).ToList();

        int index = defaultIndex;

        // Local helper: pad/truncate lines to height and width
        List<string> PadLines(List<string> lines, int targetCount, int width)
        {
            var result = new List<string>(lines);
            // truncate or pad each existing line
            for (int i = 0; i < result.Count; i++)
                result[i] = result[i].PadRight(width).Substring(0, width);

            // add blank lines if too few
            while (result.Count < targetCount)
                result.Add(new string(' ', width));

            return result;
        }

        void RenderMenu()
        {
            // Build menu lines with highlighting
            var menuLines = options
                .Select((opt, i) => ((i == index) ? "> " : "  ") + opt)
                .Select(line => line.PadRight(MainViewFillSize).Substring(0, MainViewFillSize))
                .ToList();

            ChangeMainView(menuLines, render: false);

            // Build description panel
            var descLines = PadLines(
                new List<string> { descriptions[index] },
                menuLines.Count,
                SecViewFillSize
            );
            ChangeSecView(descLines, render: false);

            if (renderEachChange)
                Render();
        }

        // Initial draw
        RenderMenu();
        Console.CursorVisible = false;

        // Input loop
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.LeftArrow:
                    index = (index - 1 + options.Count) % options.Count;
                    break;

                case ConsoleKey.DownArrow:
                case ConsoleKey.RightArrow:
                    index = (index + 1) % options.Count;
                    break;

                case ConsoleKey.Enter:
                    Console.CursorVisible = false;
                    return index;
            }
            RenderMenu();
        }
        while (true);
    }












    public T GetParsedInput<T>(int x, int y, int width = 20)
    {
        T? result;

        var inputField = new CursorInputField(x, y, width);

        while (true)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(new string(' ', width)); // clear old input

            string input = inputField.ReadInput();

            if (Parser.TryParse<T>(input, out result))
                return result!;
            else
            {
                string error = "invalid input";
                var list = GetPageInfo();
                if (list.Count == 0) { list.Add(error); ChangePageInfo(list); }
                else
                {
                    list[0] += $"{error}";//need to make it better
                    ChangePageInfo(list);
                }
            }
        }
    }

    private List<string> PadLinesToHeight(List<string> lines, int height, int width)
    {
        var padded = new List<string>(lines);
        while (padded.Count < height)
        {
            padded.Add(new string(' ', width));
        }
        return padded;
    }

}