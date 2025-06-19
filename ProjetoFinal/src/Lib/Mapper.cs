using Container.DTOs;
using Models;
using Context;
using MyRepository;

// namespace Mapper;


public static class MapperTools
{
    public static TeamDto MapToDto(Team team, DataContext context)
    {
        if (team == null)
        {
            throw new ArgumentNullException(nameof(team), "Team cannot be null");
        }

        if (context == null)
        {
            throw new ArgumentNullException(nameof(context), "DataContext cannot be null");
        }

        List<int> playersIdList = team.PlayersId ?? new List<int>();
        List<PlayerDto> playersInstances = new();

        foreach (var id in playersIdList)
        {
            Player? player = context.PlayerRepo.GetById(id) ?? new(id);
            PlayerDto playerDto = new(player);
            playersInstances.Add(playerDto);
        }
        return new TeamDto(team, playersInstances);

    }
    public static List<TeamDto> MapToDtoList(List<Team> teams, DataContext context)
    {
        if (teams == null)
        {
            throw new ArgumentNullException(nameof(teams), "Teams list cannot be null");
        }

        if (context == null)
        {
            throw new ArgumentNullException(nameof(context), "DataContext cannot be null");
        }

        return teams.Select(team => MapToDto(team, context)).ToList();
    }

    public static List<Player> MapTeam(Team team, DataContext data)
    {
        List<int> playersIdList = team.PlayersId ?? new List<int>();
        List<Player> playersInstances = new();

        foreach (var id in playersIdList)
        {
            Player? player = data.PlayerRepo.GetById(id) ?? new(id);
            playersInstances.Add(player);
        }
        return playersInstances;
    }

    public static List<Player> MapPlayersByIds(List<int> ids, DataContext data)
    {
        List<Player> playersInstances = new();
        foreach (var id in ids)
        {
            Player? player = data.PlayerRepo.GetById(id) ?? new(id);
            playersInstances.Add(player);
        }
        return playersInstances;
    }
    public static List<Player> MapPlayersByIds(List<int> ids, PlayersRepo repo)
    {
        List<Player> playersInstances = new();
        foreach (var id in ids)
        {
            Player? player = repo.GetById(id) ?? new(id);
            playersInstances.Add(player);
        }
        return playersInstances;
    }

    public static List<Team> MapTeamsByIds(List<int> ids, TeamRepo repo)
    {
        List<Team> teamsInstances = new();
        foreach (var id in ids)
        {
            Team? team = repo.GetById(id) ?? new(id);
            teamsInstances.Add(team);
        }
        return teamsInstances;

    }

    public static List<Team> MapTeamsByIds(List<int> ids, DataContext data)
    {
        List<Team> teamsInstances = new();
        foreach (var id in ids)
        {
            Team? team = data.TeamRepo.GetById(id) ?? new(id);
            teamsInstances.Add(team);
        }
        return teamsInstances;
    }



}