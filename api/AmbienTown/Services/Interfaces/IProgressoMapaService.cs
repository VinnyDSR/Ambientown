using AmbienTown.Dto.Leaderboard;
using AmbienTown.Dto.ProgressoMapa;
using System.Collections.Generic;
using TimeSheet.Api.Utils.Result;

namespace AmbienTown.Services.Interfaces
{
    public interface IProgressoMapaService
    {
        Result<ObterProgressoMapaDto> ObterPorId(int id);
        IEnumerable<ObterProgressoMapaDto> Listar();
        Result<int> Cadastrar(NovoProgressoMapaDto progressoMapa);
        Result Remover(int id);
        Result Existe(int id);
        public Result<List<ObterLeaderboardDto>> ObterLeaderboard(int mapa);
    }
}