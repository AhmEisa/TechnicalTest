using AutoMapper;
using Inetum.Application.Features.Players.Commands;
using Inetum.Application.Features.Teams.Queries;
using Inetum.Domain.Entities;
using System;

namespace Inetum.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Team, TeamListVm>().ReverseMap();
            CreateMap<Team, TeamListWithPlayersVm>().ReverseMap();
            CreateMap<Player, PlayerListVm>().ReverseMap();

            CreateMap<CreateTeamCommand, Team>()
                .ForMember(d => d.TeamId, s => s.MapFrom(c => Guid.NewGuid()));

            CreateMap<Team, CreateTeamDto>();

            CreateMap<CreatePlayerDto, Player>()
                 .ForMember(d => d.PlayerId, s => s.MapFrom(c => Guid.NewGuid()));

            CreateMap<UpdateTeamCommand, Team>();

            CreateMap<AddPlayerCommand, Player>()
                .ForMember(d => d.PlayerId, s => s.MapFrom(c => Guid.NewGuid()));

            CreateMap<Player, AddPlayerDto>();
            CreateMap<UpdatePlayerCommand, Player>();

        }
    }
}
