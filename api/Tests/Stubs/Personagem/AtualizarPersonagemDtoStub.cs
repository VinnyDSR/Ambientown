using AmbienTown.Dto.Personagem;
using AmbienTown.Enums;
using Bogus;

namespace Tests.Stubs.Personagem
{
    public static class AtualizarPersonagemDtoStub
    {
        private static readonly Faker faker = new Faker("pt_BR");

        private static AtualizarPersonagemDto Factory(int id, Genero genero, Pele pele, int roupaId) => new AtualizarPersonagemDto
        {
            Id = id,
            Genero = genero,
            Pele = pele,
            RoupaId = roupaId
        };

        public static AtualizarPersonagemDto Completo(int id, int roupaId) =>
            Factory(id, (Genero)faker.Random.Int(1, 2), (Pele)faker.Random.Int(1, 3), roupaId);

        public static AtualizarPersonagemDto SemGenero(int id, int roupaId) =>
            Factory(id, 0, (Pele)faker.Random.Int(1, 3), roupaId);

        public static AtualizarPersonagemDto SemPele(int id, int roupaId) =>
            Factory(id, (Genero)faker.Random.Int(1, 2), 0, roupaId);

        public static AtualizarPersonagemDto SemRoupa(int id) =>
            Factory(id, (Genero)faker.Random.Int(1, 2), (Pele)faker.Random.Int(1, 3), 0);
    }
}