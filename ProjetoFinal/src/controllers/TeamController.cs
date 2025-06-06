using Context;
using Models;
using Templates;
using View;
using Container.DTOs;

namespace Controller;

public class TeamController
{
    private DataContext _data;
    private TeamView _view;
    private bool _saved = true;
    private bool isRunning = true;


    public TeamController(DataContext data, TeamView view)
    {
        _data = data;
        _view = view;
    }

    public void BeginInteraction()
    {
        isRunning = true;
        while (isRunning)
        {
            int input = _view.MainMenu(_saved);
            HandleUserChoice(input);
        }
    }

    public void HandleUserChoice(int input)
    {
        switch (input)
        {
            case 0:
                isRunning = _view.Bye(_saved);
                break;
            case 1:
                CreateTeam();
                break;
            case 2:
                EditTeam();
                break;
            case 3:
                DeleteTeam();
                break;
            case 4:
                ListTeams();
                break;
            case 5:
                _data.SaveDataBase();
                _saved = true;
                break;
            default:
                throw new ArgumentOutOfRangeException("Invalid choice. Please select a valid option.");
        }
    }
    
    //needs to be polished but its functional
    private void CreateTeam()
    {
        var teamDto = _view.GetTeamInput();
        var team = new Team(teamDto);
        _data.TeamRepo.Add(team);
        _saved = false;
    }

    private void EditTeam()
    {
        if (_data.TeamRepo.Count == 0) { return; }

        var id = _view.ShowAndGetTeamId(_data.TeamRepo.GetAll().Select(t => new TeamDto(t)).ToList());

        if (id < 0) { return; }

        var teamDto = _view.GetTeamInput();

        _data.TeamRepo.UpdateById(id, new Team(teamDto));
    }

    private void ListTeams()
    {
        var teams = _data.TeamRepo.GetAll();
        _view.ShowTeams(teams.Select(t => new TeamDto(t)).ToList());
    }

    private void DeleteTeam()//can be better ⚠️
    {

        if (_data.TeamRepo.Count == 0) { return; }

        var id = _view.ShowAndGetTeamId(_data.TeamRepo.GetAll().Select(t => new TeamDto(t)).ToList());

        if (id < 0) { return; }

        var team = _data.TeamRepo.GetById(id) ?? throw new NullReferenceException("team can't be null");

        _view.ShowTeam(new TeamDto(team));

        var confirmation = _view.ConfirmDelete(new TeamDto(team));

        if (confirmation)
        {
            _data.TeamRepo.Remove(team);
            _saved = false;
        }

    }
}
