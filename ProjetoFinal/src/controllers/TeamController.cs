using Context;
using team;
using Templates;
using View;
using Container.DTOs;

namespace Controller;

public class TeamController
{
    private DataContext _data;
    private IView _view;

    public TeamController(DataContext data, IView view)
    {
        _data = data;
        _view = view;
    }

    public void BeginInteraction()
    {
        bool isRunning = true;
        while (isRunning)
        {
            // TODO: Implement menu and handle user choices
            // Example: int input = _view.MainMenu();
            // isRunning = HandleUserChoice(input);
            isRunning = false; // Placeholder to prevent infinite loop
        }
    }

    // Add methods for CreateTeam, EditTeam, DeleteTeam, ListTeams, etc.
}
