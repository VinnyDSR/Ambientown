using AmbienTown.Dto.Leaderboard;
using AmbienTown.Models;
using AmbienTown.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AmbienTown.Repositories
{
    public class ProgressoMapaRepository : Repository<ProgressoMapa>, IProgressoMapaRepository
    {
        public ProgressoMapaRepository(Entities entities) : base(entities)
        {
            DefaultQuery = DefaultQuery.Select(SelectFields);
        }

        public List<ObterLeaderboardDto> ObterLeaderboard(int mapa)
        {
            return DefaultQuery
                .Select(x => new ObterLeaderboardDto
                {
                    Apelido = x.Usuario.Apelido,
                    Id = x.Id,
                    Pontuacao = x.Pontuacao,
                    Mapa = x.Mapa
                })
                .Where(x => ((int)x.Mapa) == mapa).ToList();
        }

        private Expression<Func<ProgressoMapa, ProgressoMapa>> SelectFields => x => new ProgressoMapa
        {
            Id = x.Id,
            Colecionavel = x.Colecionavel,
            ColecionavelEspecial = x.ColecionavelEspecial,
            DataRegistro = x.DataRegistro,
            Mapa = x.Mapa,
            Tempo = x.Tempo,
            Pontuacao = x.Pontuacao,
            Usuario = x.Usuario
        };
    }
}