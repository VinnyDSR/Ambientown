using AmbienTown.Models;
using AmbienTown.Repositories.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AmbienTown.Repositories
{
    public class PersonagemRepository : Repository<Personagem>, IPersonagemRepository
    {
        public PersonagemRepository(Entities entities) : base(entities)
        {
            DefaultQuery = DefaultQuery.Select(SelectFields);
        }

        private Expression<Func<Personagem, Personagem>> SelectFields => x => new Personagem
        {
            Id = x.Id,
            Genero = x.Genero,
            Pele = x.Pele,
            RoupaId = x.RoupaId
        };
    }
}