using AmbienTown.Dto.Personagem;
using AmbienTown.Enums;
using Bogus;

namespace Tests.Stubs.Personagem
{
    public static class NovoPersonagemDtoStub
    {
        private static readonly Faker faker = new Faker("pt_BR");

        private static NovoPersonagemDto Factory(Genero genero, Pele pele, int roupaId) => new NovoPersonagemDto
        {
            Genero = genero,
            Pele = pele,
            RoupaId = roupaId
        };

        public static NovoPersonagemDto Completo(int roupaId) =>
            Factory((Genero)faker.Random.Int(1, 2), (Pele)faker.Random.Int(1, 3), roupaId);

        public static NovoPersonagemDto SemGenero(int roupaId) =>
            Factory(0, (Pele)faker.Random.Int(1, 3), roupaId);

        public static NovoPersonagemDto SemPele(int roupaId) =>
            Factory((Genero)faker.Random.Int(1, 2), 0, roupaId);

        public static NovoPersonagemDto SemRoupa() =>
            Factory((Genero)faker.Random.Int(1, 2), (Pele)faker.Random.Int(1, 3), 0);
    }
}