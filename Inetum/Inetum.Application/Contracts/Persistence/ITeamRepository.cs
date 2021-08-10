using Inetum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inetum.Application.Contracts.Persistence
{
    public interface ITeamRepository : IAsyncRepository<Team>
    {
        Task<List<Team>> GetTeamsWithPlayers(bool includePlayers);
        Task<Team> GetTeamWithPlayers(Guid teamId);
    }
}
