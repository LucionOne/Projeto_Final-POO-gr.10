using Container.DTOs;
using Models;
using Context;

namespace Mapper;


public static class TeamDtoMapper
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




}