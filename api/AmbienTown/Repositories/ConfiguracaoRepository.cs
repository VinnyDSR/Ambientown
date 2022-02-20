using AmbienTown.Models;
using AmbienTown.Repositories.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AmbienTown.Repositories
{
    public class ConfiguracaoRepository : Repository<Configuracao>, IConfiguracaoRepository
    {
        public ConfiguracaoRepository(Entities entities) : base(entities)
        {
            DefaultQuery = DefaultQuery.Select(SelectFields);
        }

        private Expression<Func<Configuracao, Configuracao>> SelectFields => x => new Configuracao
        {
            Id = x.Id,
            Coleta = x.Coleta,
            Fala = x.Fala,
            Idioma = x.Idioma,
            Som = x.Som
        };
    }
}