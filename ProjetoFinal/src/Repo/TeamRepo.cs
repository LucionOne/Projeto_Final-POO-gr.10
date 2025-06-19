using Models;
using System.Text.Json;
using Templates;
using Container.DTOs;

namespace MyRepository;

public class TeamRepo : RepoAbstract<Team>
{
    public TeamRepo(string fileName = "Teams.json") : base(fileName) { }

    public TeamRepo()
    {
        _fileName = "Teams.json";
        _filePath = Path.Combine(FolderPath, _fileName);
    }

    public override TeamRepo DataBaseStarter()
    {
        ConfirmFileAndFolderExistence();
        TeamRepo repository = LoadFromDataBase();
        return repository;
    }

    public override TeamRepo LoadFromDataBase()
    {
        string file = File.ReadAllText(_filePath);
        TeamRepo temp = JsonSerializer.Deserialize<TeamRepo>(file)
            ?? throw new NullReferenceException("Deserializer returned null");
        return temp;
    }

    public List<TeamDto> ToDto()
    {
        return _mainRepo.Select(t => new TeamDto(t)).ToList();
    }

    public List<Team> Map(List<int> ids)
    {
        return MapperTools.MapTeamsByIds(ids, this);
    }

    public List<TeamDto> ToDto(PlayersRepo players)
    {
        return this._mainRepo.Select(team =>
        {
            var dto = new TeamDto(team);
            dto.Players = MapperTools.MapPlayersByIds(team.PlayersId, players).Select(player => new PlayerDto(player)).ToList();
            return dto;
        }).ToList();
    }    


}

