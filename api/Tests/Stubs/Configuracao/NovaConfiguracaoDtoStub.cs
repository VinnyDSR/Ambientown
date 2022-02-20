using Bogus;
using AmbienTown.Dto.Configuracao;
using AmbienTown.Enums;

namespace Tests.Stubs.Configuracao
{
    public static class NovaConfiguracaoDtoStub
    {
        private static readonly Faker faker = new Faker("pt_BR");

        private static NovaConfiguracaoDto Factory(Idioma idioma, bool som, bool fala, bool coleta) => new NovaConfiguracaoDto
        {
            Idioma = idioma,
            Som = som,
            Fala = fala,
            Coleta = coleta
        };

        public static NovaConfiguracaoDto Completo() =>
            Factory((Idioma)faker.Random.Int(1, 2), faker.Random.Bool(), faker.Random.Bool(), faker.Random.Bool());

        public static NovaConfiguracaoDto SemIdioma() =>
            Factory(0, faker.Random.Bool(), faker.Random.Bool(), faker.Random.Bool());
    }
}