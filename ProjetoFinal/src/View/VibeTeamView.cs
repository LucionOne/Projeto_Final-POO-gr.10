// using Templates;
// using Templates.view;
// using Container.DTOs;
// using System.Dynamic;
// using System.ComponentModel.Design;
// using VS;
// using System.Xml.Serialization;



// namespace View;


// public class VibeTeamView
// {
//     public VibeShell _vibe;

//     public VibeTeamView(VibeShell vibe)
//     {
//         _vibe = vibe;

//         List<string> header = new()
//         {
//             "Team Management",
//         };

//         List<string> pageInfo = new()
//         {
//             "Manage your teams efficiently",
//         };

//         List<string> mainView = new()
//         {
//             "01. Create Team",
//             "02. Edit Team",
//             "03. Delete Team",
//             "04. List Teams",
//             "05. Save Changes",
//             "00. Exit",
//         };
//         List<string> infBar = new()
//         { };

//         _vibe.ChangeHeader(header, false);
//         _vibe.ChangePageInfo(pageInfo, false);
//         _vibe.ChangeMainView(mainView, false);
//         _vibe.ChangeInfBar(infBar, false);

//     }

//     public int MainMenu(bool saved)
//     {
//         string savedStr;
//         string savedStrDescription;
//         if (saved)
//         {
//             savedStrDescription = "Saved";
//             savedStr = "";
//         }
//         else
//         {
//             savedStrDescription = "Unsaved Changes";
//             savedStr = "*";
//         }

//         List<string> options = new()
//         {
//             "01. Create Team",
//             "02. Edit Team",
//             "03. Delete Team",
//             "04. List Teams",
//            $"05. Save Changes{savedStr}",
//             "00. Exit",
//         };

//         List<string>? createTeamDescription = new()
//         {
//             "Create a new team with a unique name.",
//         };
//         List<string>? editTeamDescription = new()
//         {
//             "Edit an existing team by ",
//             "selecting it from the list."
//         };
//         List<string>? deleteTeamDescription = new()
//         {
//             "Delete an existing team ",
//             "by selecting it from the list."
//         };
//         List<string>? listTeamsDescription = new()
//         {
//             "List all existing teams.",
//         };
//         List<string>? saveChangesDescription = new()
//         {
//             $"Save changes to the teams.",
//             $"Current status: {savedStrDescription}"
//         };
//         List<string>? exitDescription = new()
//         {
//             "Exit the team management menu.",
//         };

//         List<List<string>>? descriptionsList = new()
//         {
//             createTeamDescription,
//             editTeamDescription,
//             deleteTeamDescription,
//             listTeamsDescription,
//             saveChangesDescription,
//             exitDescription,
//         };

//         var choice = _vibe.HandleMenu(
//             options: options,
//             descriptions: descriptionsList,
//             defaultIndex: 0,
//             menuScale: _vibe.Scale,
//             renderEachChange: true
//         );

//         if (choice == -1)
//         {
//             choice = 0; // Default to first option if invalid
//         }
//         return choice;
//     }

//     public bool Bye(bool saved)
//     {
//         if (saved)
//         {
//             _vibe.Clear(true, true);
//             return false; // Exit the menu
//         }
//         else
//         {
//             List<string> options = new()
//             {
//                 "Yes, exit without saving",
//                 "No, go back to the menu",
//             };

//             _vibe.clearSecView(render: false);

//             int choice = _vibe.HandleMenu(
//                 options: options,
//                 defaultIndex: 0,
//                 menuScale: _vibe.Scale,
//                 renderEachChange: true
//             );

//             if (choice == 0)
//             {
//                 _vibe.Clear(true, true);
//                 return false; // Exit the menu
//             }
//             else
//             {
//                 _vibe.Clear(false, true);
//                 return true; // Go back to the menu
//             }
//         }
//     }

//     private List<string> GetPlayerListString(List<PlayerDto> players)
//     {
//         List<string> strings = new();

//         var stringList = new List<string>
//         {
//             "====================================================",//52
//             "|              P L A Y E R   L I S T               |",
//             "+--------------------------------------------------+",
//             "|ID   | Name                | Age  | Position      |",
//             "+-----+---------------------+------+---------------+",
//         };

//         foreach (PlayerDto player in players)
//         {
//             strings.Add($"{player.Id.ToString().PadRight(5)}| {player.Name.PadRight(20)}| {player.Age.ToString().PadRight(3)}| {player.PositionString.PadRight(14)}");
//         }
//         return strings;
//     }

//     public TeamDto? GetTeamInput(List<PlayerDto> players)
//     {
//         _vibe.Clear();
//         if (players == null || players.Count == 0)
//         {
//             _vibe.ChangeMainView(new List<string> { "No players available to create a team." }, true);
//             return null;
//         }

//         var query = new List<string>
//         {
//             "Team Creation",
//             "",
//             "Enter the team name:",
//         };

//         _vibe.ChangeMainView(query, true);

//         bool confirmation = false;
//         while (!confirmation)
//         {
//             string teamName = _vibe.GetParsedInput<string>(21, 8, 20);
//             confirmation = _vibe.GetParsedInput<bool>(21, 9, 2);
//         }

//         var playersList = GetPlayerListString(players);

//         _vibe.Clear();

//         _vibe.ChangeMainView(playersList);

//         int input;
//         bool end = false;
//         while (end)
//         {
//             input = _vibe.BasicInput<int>();

//             var val = false;
//             while (val)
//             {
//                 var yn = _vibe.ReadLine();
//                 val = Parser.TryParse<bool>(yn, out end);
//             }
//         }


        






//     }
// }