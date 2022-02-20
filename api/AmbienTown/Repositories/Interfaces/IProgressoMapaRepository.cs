using AmbienTown.Dto.Leaderboard;
using AmbienTown.Models;
using System.Collections.Generic;

namespace AmbienTown.Repositories.Interfaces
{
    public interface IProgressoMapaRepository : IRepository<ProgressoMapa>
    {
        public List<ObterLeaderboardDto> ObterLeaderboard(int mapa);
    }
}