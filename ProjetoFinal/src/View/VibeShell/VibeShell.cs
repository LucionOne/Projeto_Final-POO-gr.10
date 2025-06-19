
// in development, not in use yet, got to make sure the controller works fine

using System.Dynamic;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

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

    public int MinMainWindowHeight = 10;

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


    public const string Alphabet = "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string Numbers = "1234567890";

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

        MinMainWindowHeight = mainWindowHeight;
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
        MinMainWindowHeight = mainWindowHeight;
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










    #region Methods















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

    public void BetterChangeHeader(string newHeader, bool render = true, char character = ' ')
    {
        int fill = (FillSize - newHeader.Length) / 2;
        string betterHeader = new string(character, fill) + newHeader;
        ChangeHeader([betterHeader], render);

    }
    public void BetterChangePageInfo(string newPageInfo, bool render = true, char character = ' ')
    {
        int fill = (FillSize - newPageInfo.Length) / 2;
        string betterPageInfo = new string(character, fill) + newPageInfo;
        ChangePageInfo([betterPageInfo], render);
    }

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



    public List<string> GetRawHeader()
    {
        return RawHeader;
    }
    public List<string> GetRawPageInfo()
    {
        return RawPageInfo;
    }
    public List<string> GetRawMainView()
    {
        return RawMainView;
    }
    public List<string> GetRawSecView()
    {
        return RawSecView;
    }
    public List<string> GetRawInfBar()
    {
        return RawInfBar;
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
        ChangeInfBar(new List<string> { });
        if (render) Render();
    }



    public void Clear(bool extraClean = false, bool render = true)
    {
        if (extraClean)
        {
            ChangeHeader(new List<string> { "" });
        }

        ChangePageInfo(new List<string> { "" });
        ChangeMainView(new List<string> { "" });
        ChangeSecView(new List<string> { "" });
        ChangeInfBar(new List<string>());

        if (render) Render();
    }
    #endregion Edit Views














    #region PostProcessing

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

        int minHeightDif = MinMainWindowHeight - _mainView.Count;

        if (minHeightDif > 0)
        {
            for (var x = minHeightDif; x > 0; x--)
            {
                _mainView.Add($"{new string(' ', MainViewFillSize)}");
                _secView.Add($"{new string(' ', SecViewFillSize)}");
            }
        }

    }

    #endregion PostProcessing




















    #region Menu Handling




















    #region HandleMenu
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
    #endregion HandleMenu









    #region Simple Input
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
                var list = GetRawPageInfo();
                if (list.Count == 0) { list.Add(error); ChangePageInfo(list); }
                else
                {
                    list[0] += $"{error}"; //need to make it better
                    ChangePageInfo(list); //barely functional
                }
            }
        }
    }




    public T HandleInputAt<T>(
        string prompt,
        int x, int y,
        int width = 20,
        Func<char, bool>? charFilter = null,
        int errorDelayMs = 800,
        bool clear = false
)
    {
        int inputX = x + prompt.Length;
        int inputY = y;
        var inputField = new CursorInputField(inputX, inputY, width, charFilter);

        while (true)
        {
            // Draw prompt
            Console.SetCursorPosition(x, y);
            Console.Write(prompt);

            // Clear input area
            Console.SetCursorPosition(inputX, inputY);
            Console.Write(new string(' ', width));
            Console.SetCursorPosition(inputX, inputY);

            // Read raw input (ReadInput stops at Enter without producing a newline)
            string raw = inputField.ReadInput();

            // Try to parse
            if (Parser.TryParse<T>(raw, out var result))
            {
                if (clear)

                    // Clear any leftover message
                    Console.SetCursorPosition(x, y);
                Console.Write(new string(' ', width + prompt.Length));
                return result!;
            }

            // Write error in the same box
            Console.SetCursorPosition(inputX, inputY);
            string err = "Invalid".PadRight(width);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(err);
            Console.ResetColor();

            Thread.Sleep(errorDelayMs);

            // Clear the error message before retry
            Console.SetCursorPosition(inputX, inputY);
            Console.Write(new string(' ', err.Length));
        }
    }

    public void WaitForClick()
    {
        // var pos = GetReadLinePosition();
        Console.CursorVisible = false;
        Console.SetCursorPosition(0, 0);
        Console.ReadKey();
        Console.CursorVisible = true;
    }
    #endregion Simple Input









    #region Show Items

    /// <summary>
    /// Show a list of header lines + numbered items in MainView. In SecView, prompt for an ID and display
    /// its multi-line description. Loop until the user enters exitCode.
    /// </summary>
    public void HandleListItems(
    List<SelectableItem> items,
    List<string> headerLines,
    string exitCode = "XX",
    string prompt = "Enter ID (or XX to exit):"
)
    {
        // 1) Build MainView: header + real IDs with labels
        var numbered = new List<string>(headerLines);
        numbered.AddRange(items.Select(item => $"{item.Label}"));//{item.Id}. {item.Label}"));
        ChangeMainView(numbered, render: false);

        // 2) Determine input position in SecView
        var (secX, secY) = GetSecViewPosition();
        int inputX = secX + prompt.Length + 1;
        int inputY = secY;

        void RenderSec(List<string> descLines)
        {
            var sec = new List<string> { prompt.PadRight(SecViewFillSize) };
            int slotCount = numbered.Count - 1;

            for (int i = 0; i < slotCount; i++)
            {
                sec.Add(i < descLines.Count
                    ? descLines[i].PadRight(SecViewFillSize).Substring(0, SecViewFillSize)
                    : new string(' ', SecViewFillSize));
            }

            sec.Add(new string(' ', SecViewFillSize));
            ChangeSecView(sec, render: false);
            Render();
        }

        RenderSec(new());
        Console.CursorVisible = true;

        while (true)
        {
            Console.SetCursorPosition(inputX, inputY);
            Console.Write(new string(' ', 10));
            Console.SetCursorPosition(inputX, inputY);

            string raw = new CursorInputField(inputX, inputY, 10).ReadInput().Trim();

            if (raw.Equals(exitCode, StringComparison.OrdinalIgnoreCase))
                break;

            if (int.TryParse(raw, out int id))
            {
                var match = items.FirstOrDefault(i => i.Id == id);
                if (match != null)
                {
                    RenderSec(match.Description ?? new());
                    continue;
                }
            }

            // Invalid ID
            var sec = GetRawSecView();
            sec[^1] = "Invalid ID".PadRight(SecViewFillSize);
            ChangeSecView(sec, render: false);
            Render();
            Thread.Sleep(700);
            RenderSec(new());
        }

        Console.CursorVisible = false;
    }


    #endregion Show Items









    #region id handlers

    public List<int> HandleMultiSelectIds(
        List<SelectableItem> choices,
        List<string> headerLines,
        string exitCode = "XX",
        string prompt = "Enter ID (or XX to finish):"
    )
    {
        // 1) Show all choices in MainView
        var numbered = new List<string>(headerLines);
        numbered.AddRange(choices.Select(c => $"{c.Label}")); //{c.Id}. 
        ChangeMainView(numbered, render: false);

        var (secX, secY) = GetSecViewPosition();
        int inputX = secX + prompt.Length + 1;
        int inputY = secY;

        List<int> selectedIds = new();
        SelectableItem? lastSelected = null;

        void RenderSec(List<string> descLines, string footer)
        {
            var sec = new List<string>();
            sec.Add(prompt.PadRight(SecViewFillSize));
            int contentHeight = numbered.Count - 1;

            for (int i = 0; i < contentHeight; i++)
            {
                if (i < descLines.Count)
                    sec.Add(descLines[i]
                        .PadRight(SecViewFillSize)
                        .Substring(0, SecViewFillSize));
                else
                    sec.Add(new string(' ', SecViewFillSize));
            }

            sec.Add(footer.PadRight(SecViewFillSize));
            ChangeSecView(sec, render: false);
            Render();
        }

        RenderSec(new(), "Selected: —");
        Console.CursorVisible = true;

        while (true)
        {
            Console.SetCursorPosition(inputX, inputY);
            Console.Write(new string(' ', 10));
            Console.SetCursorPosition(inputX, inputY);

            string raw = new CursorInputField(inputX, inputY, 10).ReadInput().Trim();

            if (raw.Equals(exitCode, StringComparison.OrdinalIgnoreCase))
                break;

            if (int.TryParse(raw, out int id))
            {
                var match = choices.FirstOrDefault(c => c.Id == id);
                if (match != null)
                {
                    if (!selectedIds.Contains(id))
                        selectedIds.Add(id);

                    lastSelected = match;
                    string footer = $"Selected: {string.Join(", ", selectedIds)}";
                    RenderSec(match.Description, footer);
                    continue;
                }
            }

            // Invalid ID
            var secFb = GetRawSecView();
            secFb[^1] = $"Invalid ID".PadRight(SecViewFillSize);
            ChangeSecView(secFb, render: false);
            Render();
            Thread.Sleep(700);
            if (lastSelected != null)
            {
                string footer = $"Selected: {string.Join(", ", selectedIds)}";
                RenderSec(lastSelected.Description, footer);
            }
            else
            {
                RenderSec(new(), "Selected: —");
            }
        }

        Console.CursorVisible = false;
        return selectedIds;
    }


    /// <summary>
    /// Show only validIds in MainView (with header). Prompt for an ID to view its description.
    /// When the user types exitCode, return the last valid ID they viewed (or -1 if none).
    /// </summary>
    public int HandleSelectById(
    List<SelectableItem> choices,
    List<string> headerLines,
    string exitCode = "XX",
    string prompt = "Enter ID (or XX to confirm):"
)
    {
        
        var numbered = new List<string>(headerLines);
        numbered.AddRange(choices.Select(c => $"{c.Label}"));//{c.Id}. {c.Label}"));
        ChangeMainView(numbered, render: false);

        var (secX, secY) = GetSecViewPosition();
        int inputX = secX + prompt.Length + 1;
        int inputY = secY;

        int lastId = -1;
        void RenderSec(List<string> desc, string footer)
        {
            var sec = new List<string> { prompt.PadRight(SecViewFillSize) };
            int slots = numbered.Count - 1;
            for (int i = 0; i < slots; i++)
                sec.Add(i < desc.Count
                    ? desc[i].PadRight(SecViewFillSize).Substring(0, SecViewFillSize)
                    : new string(' ', SecViewFillSize));
            sec.Add(footer.PadRight(SecViewFillSize));
            ChangeSecView(sec, render: false);
            Render();
        }

        // first draw
        RenderSec(new(), "Selected: —");
        Console.CursorVisible = true;

        while (true)
        {
            Console.SetCursorPosition(inputX, inputY);
            Console.Write(new string(' ', 5));
            Console.SetCursorPosition(inputX, inputY);

            string rawInput = new CursorInputField(inputX, inputY, 5).ReadInput().Trim();
            if (rawInput.Equals(exitCode, StringComparison.OrdinalIgnoreCase))
                break;

            if (int.TryParse(rawInput, out var id))
            {
                // find the item with that real ID
                var item = choices.FirstOrDefault(c => c.Id == id);
                if (item != null)
                {
                    lastId = id;
                    RenderSec(item.Description, $"Selected: {id}");
                    continue;
                }
            }

            // invalid
            var secFb = GetRawSecView();
            secFb[^1] = $"Not a valid ID";
            ChangeSecView(secFb, render: false);
            Render();
            Thread.Sleep(700);
            if (lastId > 0)
            {
                var prev = choices.First(c => c.Id == lastId);
                RenderSec(prev.Description, $"Selected: {lastId}");
            }
            else
            {
                RenderSec(new(), "Selected: —");
            }
        }

        Console.CursorVisible = false;
        return lastId;
    }
    #endregion id handlers




    #endregion Menu Handling




    #endregion Methods










    #region  Support Classes


    public class SelectableItem
    {
        public int Id { get; }
        public string Label { get; }
        public List<string> Description { get; }

        public SelectableItem(int id, string label, List<string> description)
        {
            Id = id;
            Label = label;
            Description = description ?? new List<string>();
        }

    }
    #endregion Support Classes
}
