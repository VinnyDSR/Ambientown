using AmbienTown.Dto.Configuracao;
using AmbienTown.Enums;
using Bogus;

namespace Tests.Stubs.Configuracao
{
    public static class AtualizarConfiguracaoDtoStub
    {
        private static readonly Faker faker = new Faker("pt_BR");

        private static AtualizarConfiguracaoDto Factory(int id, Idioma idioma, bool som, bool fala, bool coleta) => new AtualizarConfiguracaoDto
        {
            Id = id,
            Idioma = idioma,
            Som = som,
            Fala = fala,
            Coleta = coleta
        };

        public static AtualizarConfiguracaoDto Completo(int id) =>
            Factory(id, (Idioma)faker.Random.Int(1, 2), faker.Random.Bool(), faker.Random.Bool(), faker.Random.Bool());

        public static AtualizarConfiguracaoDto SemIdioma(int id) =>
            Factory(id, 0, faker.Random.Bool(), faker.Random.Bool(), faker.Random.Bool());
    }
}