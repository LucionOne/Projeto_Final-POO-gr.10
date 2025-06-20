using Context;
using Models;
using Templates.view;
using View;
using Container.DTOs;
// using Mapper;

namespace Controller;

public class TeamController
{
    private DataContext _data;
    private ITeamView _view;

    private bool _saved { get { return _data.TeamRepo.Saved; } }

    private bool isRunning = true;

    private List<TeamDto> RepoDto { get { return TeamRepoToDto(); } }

    public TeamController(ITeamView view, DataContext data)
    {
        _view = view;
        _data = data;
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

    private void HandleUserChoice(int choice)
    {
        switch (choice)
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
                SaveToDataBase();
                break;
            default:
                throw new ArgumentOutOfRangeException("Invalid choice. Please select a valid option.");
        }
    }

    // Main interactions

    private void SaveToDataBase()
    {
        bool confirmation = _view.ConfirmSaveToDB();
        if (confirmation) { _data.TeamRepo.WriteToDataBase(); }
    }

    private void CreateTeam()
    {
        var teamDto = _view.GetTeamInput(_data.PlayerRepo.ToDtoList());
        var team = new Team(teamDto);
        _data.TeamRepo.Add(team);
    }

    private void ListTeams()
    {
        _view.ShowTeams(RepoDto);
    }

    private void EditTeam()
    {
        var id = _view.GetTeamId(RepoDto);

        if (id < 0) { return; } //return if user return string.Empty

        var teamToEdit = _data.TeamRepo.GetById(id) ?? new Team(id);

        var teamDto = _view.GetTeamEdit(_data.PlayerRepo.ToDtoList(), MapTeam(teamToEdit));

        _data.TeamRepo.UpdateById(id, new Team(teamDto));
    }

    private void DeleteTeam()//can be better ⚠️
    {

        var id = _view.GetTeamId(RepoDto);

        if (id < 0) { return; }

        var teamToDelete = _data.TeamRepo.GetById(id);

        if (teamToDelete == null)
        {
            return;
        }

        var confirmation = _view.ConfirmDeleteTeam(MapTeam(teamToDelete));

        if (confirmation)
        {
            _data.TeamRepo.RemoveAt(id);
        }

    }

    // Map methods

    public List<TeamDto> TeamRepoToDto()
    {
        return /*Mapper.*/MapperTools.MapToDtoList(_data.TeamRepo.GetAll(), _data);
    }

    public TeamDto MapTeam(Team team)
    {
        return /*Mapper.*/MapperTools.MapToDto(team, _data);
    }
    
}
