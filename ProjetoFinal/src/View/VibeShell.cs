
// in development, not in use yet, got to make sure the controller works fine

using System.Dynamic;
using System.Reflection.Metadata;

namespace VS; //need a better namespace

public class VibeShell //Temp name?
{
    #region Properties
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

    public int MainWindowHeight = 10;

    private List<string> Header = new();
    private List<string> PageInfo = new();
    private List<string> MainWindow { get { return JoinViews(MainView, SecView); } }
    private List<string> InfBar = new();

    private List<string> MainView = new();
    private List<string> SecView = new();

    private List<string> RawHeader = new();
    private List<string> RawPageInfo = new();
    private List<string> RawMainView = new();
    private List<string> RawSecView = new();
    private List<string> RawInfBar = new();
    #endregion Properties


    #region Constructor

    public VibeShell()
    {
        setScale(Scale);
        var _header = new List<string>
        {
            $"{"Header"}",
        };

        var _pageInfo = new List<string>
        {
            $"{"PageInfo"}",
        };

        var _mainView = new List<string>
        {
            $"{"MainView"}",
        };

        var _secView = new List<string>
        {
            $"{"SecView"}",
        };

        var _infBar = new List<string>
        {
            $"{"infBar"}",
        };

        ChangeHeader(_header, false);
        ChangePageInfo(_pageInfo, false);
        ChangeMainView(_mainView, false);
        ChangeSecView(_secView, false);
        ChangeInfBar(_infBar, false);
    }

    public VibeShell(int size = 102, float scale = 0.5f, int mainWindowHeight = 10)
    {

        MainWindowHeight = mainWindowHeight;
        WindowSize = size;
        Scale = scale;
        ScaleFillSize();

        var _header = new List<string>
        {
            $"{"Header"}",
        };

        var _pageInfo = new List<string>
        {
            $"{"PageInfo"}",
        };

        var _mainView = new List<string>
        {
            $"{"MainView"}",
        };

        var _secView = new List<string>
        {
            $"{"SecView"}",
        };

        var _infBar = new List<string>
        {
            $"{"infBar"}",
        };

        ChangeHeader(_header, false);
        ChangePageInfo(_pageInfo, false);
        ChangeMainView(_mainView, false);
        ChangeSecView(_secView, false);
        ChangeInfBar(_infBar, false);


    }

    public VibeShell(List<string> header, List<string> pageInfo, List<string> mainView, List<string> secView, List<string> infBar, int size = 102, float scale = 0.5f, int mainWindowHeight = 30)
    {
        MainWindowHeight = mainWindowHeight;
        WindowSize = size;
        Scale = scale;
        ScaleFillSize();

        ChangeHeader(header, false);
        ChangePageInfo(pageInfo, false);
        ChangeMainView(mainView, false);
        ChangeSecView(secView, false);
        ChangeInfBar(infBar, false);
    }
    #endregion Constructor




    #region Scale


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
        ChangeAllPads(false);
    }

    public void SetSize(int size)
    {
        WindowSize = size;
        ScaleFillSize();
        ChangeAllPads(false);
    }



    #endregion Scale


    #region Getters


    public int GetHeaderHeight()
    {
        return Header.Count;
    }
    public int GetPageInfoHeight()
    {
        return PageInfo.Count;
    }
    public int GetMainViewHeight()
    {
        return MainView.Count;
    }
    public int GetSecViewHeight()
    {
        return SecView.Count;
    }
    public int GetInfBarHeight()
    {
        return InfBar.Count;
    }
    public int GetMainWindowHeight()
    {
        return MainWindow.Count;
    }
    public int GetFullHeight()
    {
        return GetHeaderHeight() + GetPageInfoHeight() + GetMainWindowHeight() + GetInfBarHeight() + 5; // 5 for borders
    }

    public int GetMaxLineLength()
    {
        var minSize = 0;

        foreach (var line in RawMainView)
        {
            if (line.Length > minSize)
                minSize = line.Length;
        }
        return minSize;
    }

    public float GetMinScaleForMainView()
    {
        int maxLineLength = GetMaxLineLength();
        if (maxLineLength == 0) return 0.0f;
        // Ensure at least 1 column for the separator
        int availableWidth = FillSize;// - 1;
        float minScale = (float)maxLineLength / availableWidth;
        // Clamp between 0 and 1
        return Math.Min(Math.Max(minScale, 0f), 1f);
    }

    public (int X, int Y) GetHeaderPosition()
    {
        int X = 0;
        int Y = 0;

        Y += 1; //for border
        X += 1; //for border

        return (X, Y);
    }
    public (int X, int Y) GetPageInfoPosition()
    {
        int X = 0;
        int Y = 0;

        Y += GetHeaderHeight();
        Y += 1; //for border

        X += 1; //for border
        return (X, Y);
    }
    public (int X, int Y) GetMainViewPosition()
    {
        int X = 0;
        int Y = 0;

        Y += GetHeaderHeight();
        Y += GetPageInfoHeight();
        Y += 3; //for border

        X += 1; //for border
        return (X, Y);
    }
    public (int X, int Y) GetSecViewPosition()
    {
        int X = 0;
        int Y = 0;

        Y += GetHeaderHeight();
        Y += GetPageInfoHeight();
        Y += 3; //for border

        X += MainViewFillSize;
        X += 2; //for border

        return (X, Y);
    }
    public (int X, int Y) GetInfBarPosition()
    {
        int X = 0;
        int Y = 0;

        Y += GetHeaderHeight();
        Y += GetPageInfoHeight();
        Y += GetMainWindowHeight();

        Y += 4; //for border

        X += 1; //for border
        return (X, Y);
    }
    public (int X, int Y) GetReadLinePosition()
    {
        int X = 0;
        int Y = 0;

        Y += GetHeaderHeight();
        Y += GetPageInfoHeight();
        Y += GetMainWindowHeight();
        Y += GetInfBarHeight();

        Y += 5; //for border

        return (X, Y);
    }

    public int GetFillSize()
    {
        return FillSize;
    }


    #endregion Getters





    #region Render

    public void Render()
    {
        Console.Clear();

        Console.WriteLine($"┏{new string('━', FillSize)}┓");

        foreach (var section in new[] { Header, PageInfo, MainWindow, InfBar })
        {
            if (!Object.ReferenceEquals(section, Header))
            { Console.WriteLine($"┣{new string('─', FillSize)}┫"); }

            foreach (var line in section)
            {
                Console.WriteLine($"┃{line}┃");
            }
        }
        Console.WriteLine($"┗{new string('━', FillSize)}┛");
    }

    #endregion Render
















    #region Edit Views


    public void ChangeHeader(List<string> newHeader, bool render = true)
    {
        RawHeader = newHeader;
        Header = ProcessView(newHeader, FillSize);
        if (render) Render();
    }

    public void ChangePageInfo(List<string> newPageInfo, bool render = true)
    {
        RawPageInfo = newPageInfo;
        PageInfo = ProcessView(newPageInfo, FillSize);
        if (render) Render();
    }

    public void ChangeMainView(List<string> newMainView, bool render = true)
    {
        RawMainView = newMainView;
        MainView = ProcessView(newMainView, MainViewFillSize);
        if (render) Render();
    }

    public void ChangeSecView(List<string> newSecView, bool render = true)
    {
        RawSecView = newSecView;
        SecView = ProcessView(newSecView, SecViewFillSize);
        if (render) Render();
    }
    public void ChangeInfBar(List<string> newInfBar, bool render = true)
    {
        RawInfBar = newInfBar;
        InfBar = ProcessView(newInfBar, FillSize);
        if (render) Render();
    }

    public void ChangeAllPads(bool _render = false)
    {
        Header = ProcessView(RawHeader, FillSize);
        PageInfo = ProcessView(RawPageInfo, FillSize);
        MainView = ProcessView(RawMainView, MainViewFillSize);
        SecView = ProcessView(RawSecView, SecViewFillSize);
        InfBar = ProcessView(RawInfBar, FillSize);

        if (_render) Render();
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



    public void clearHeader(bool render = true)
    {
        ChangeHeader(new List<string> { "" });
        if (render) Render();
    }
    public void clearPageInfo(bool render = true)
    {
        ChangePageInfo(new List<string> { "" });
        if (render) Render();
    }
    public void clearMainView(bool render = true)
    {
        ChangeMainView(new List<string> { "" });
        if (render) Render();
    }
    public void clearSecView(bool render = true)
    {
        ChangeSecView(new List<string> { "" });
        if (render) Render();
    }
    public void clearInfBar(bool render = true)
    {
        ChangeInfBar(new List<string> { "" });
        if (render) Render();
    }



    public void Clear(bool extraClean = false, bool render = true)
    {
        if (extraClean)
        {
            ChangeHeader(new List<string> { "" });
            ChangePageInfo(new List<string> { "" });
        }

        ChangeMainView(new List<string> { "" });
        ChangeSecView(new List<string> { "" });
        ChangeInfBar(new List<string> { "" });

        if (render) Render();
    }
    #endregion Edit Views














    #region PostProcessing Views

    private List<string> ProcessView(List<string> view, int fill)
    {
        List<string> processedView = view.ToList(); //clones the list

        processedView = PadLineRight(processedView, fill);

        return processedView;
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




    private List<string> JoinViews(List<string> _mainView, List<string> _secView)
    {
        EqualizeViewsCount(_mainView, _secView);

        if (_mainView.Count != _secView.Count) throw new Exception("viewA needs to be equal to viewB");

        List<string> strings = new();

        for (int i = 0; i < _mainView.Count; i++)
        {
            strings.Add(_mainView[i] + '|' + _secView[i]);
        }


        return strings;
    }



    private void EqualizeViewsCount(List<string> _mainView, List<string> _secView)
    {
        var difference = _mainView.Count - _secView.Count;



        if (difference > 0) // Main bigger
        {
            for (var x = difference; x > 0; x--)
            {
                _secView.Add($"{new string(' ', SecViewFillSize)}");
            }
        }

        else if (difference < 0)// Sec bigger
        {
            for (var x = difference; x < 0; x++)
            {
                _mainView.Add($"{new string(' ', MainViewFillSize)}");
            }
        }

        int minHeightDif = MainWindowHeight - _mainView.Count;

        if (minHeightDif > 0)
        {
            for (var x = minHeightDif; x > 0; x--)
            {
                _mainView.Add($"{new string(' ', MainViewFillSize)}");
                _secView.Add($"{new string(' ', SecViewFillSize)}");
            }
        }

    }

    #endregion PostProcessing Views
































    #region Menu Handling




    // public T BasicInput<T>()
    // {
    //     while (true)
    //     {
    //         var input = Console.ReadLine() ?? string.Empty;
    //         if (Parser.TryParse<T>(input, out var result))
    //             return result!;
    //         Console.WriteLine("Invalid input. Please try again:");
    //     }
    // }

    // public string ReadLine()
    // {
    //     var Input = Console.ReadLine() ?? string.Empty;
    //     return Input;
    // }



    public int HandleMenu(
        List<string> options,
        List<List<string>>? descriptions = null,
        int defaultIndex = 0,
        float? menuScale = null,
        bool renderEachChange = true
    )
    {
        // Apply new scale if provided
        if (menuScale.HasValue)
            setScale(menuScale.Value);

        // Ensure descriptions aligns with options: each description is a list of lines
        if (descriptions == null || descriptions.Count != options.Count)
        {
            descriptions = Enumerable.Repeat(
                new List<string>(),
                options.Count
            ).ToList();
        }

        int index = defaultIndex;

        // Helper: pad or truncate a list of strings to targetCount and width
        List<string> PadLines(List<string> lines, int targetCount, int width)
        {
            var result = new List<string>(lines.Select(line => line.PadRight(width).Substring(0, width)));
            while (result.Count < targetCount)
                result.Add(new string(' ', width));
            return result;
        }

        void RenderMenu()
        {
            // Build menu lines with highlighting
            var menuLines = options
                .Select((opt, i) => (i == index ? "> " : "  ") + opt)
                .Select(line => line.PadRight(MainViewFillSize).Substring(0, MainViewFillSize))
                .ToList();

            ChangeMainView(menuLines, render: false);

            // Build description panel from multiple lines
            var descPanel = PadLines(
                descriptions[index],       // the list of lines for current description
                menuLines.Count,           // match number of menu lines
                SecViewFillSize
            );
            ChangeSecView(descPanel, render: false);

            if (renderEachChange)
                Render();
        }

        // Initial render
        RenderMenu();
        Console.CursorVisible = false;

        // Input loop
        do
        {
            var key = Console.ReadKey(true).Key;
            switch (key)
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











    public T GetParsedInput<T>(int x, int y, int width = 20, Func<char, bool>? charFilter = null)
    {
        T? result;

        var inputField = new CursorInputField(x, y, width, charFilter);

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

    public List<int> PickMultipleById(List<string> items, string exitCode = "XX", string prompt = "Enter ID (or XX to finish):")
    {
        var numbered = items
            .Select((text, idx) => $"{idx + 1}. {text}")
            .ToList();

        var pickedIds = new List<int>();

        // Initial layout
        ChangeMainView(numbered, render: false);

        var (secX, secY) = GetSecViewPosition();
        int inputY = secY;
        int inputX = 2 + prompt.Length + secX;


        void RenderSec(string footer = "Selected: —")
        {
            var secView = new List<string>();

            secView.Add(prompt.PadRight(SecViewFillSize));
            for (int i = 1; i < MainView.Count - 1; i++)
                secView.Add(new string(' ', SecViewFillSize));

            secView.Add(footer.PadRight(SecViewFillSize));
            ChangeSecView(secView);
        }

        RenderSec();
        Console.CursorVisible = true;

        while (true)
        {
            Console.SetCursorPosition(inputX, inputY);
            Console.Write(new string(' ', 10)); // clear input box
            Console.SetCursorPosition(inputX, inputY);

            string raw = new CursorInputField(inputX, inputY, 10).ReadInput().Trim();

            if (raw.Equals(exitCode, StringComparison.OrdinalIgnoreCase))
                break;

            if (int.TryParse(raw, out int id) && id >= 1 && id <= items.Count)
            {
                pickedIds.Add(id);
                RenderSec("Selected: " + string.Join(", ", pickedIds));
            }
            else
            {
                RenderSec($"Invalid ID (1–{items.Count})");
                Thread.Sleep(800);
                RenderSec("Selected: " + string.Join(", ", pickedIds));
            }
        }

    Console.CursorVisible = false;
    return pickedIds;
}

#endregion Menu Handling
}