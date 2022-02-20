using AmbienTown;
using Xunit;

namespace Tests.Fixtures
{
    [CollectionDefinition(nameof(UsuarioAdministradorFixtureCollection))]
    public class UsuarioAdministradorFixtureCollection : ICollectionFixture<AdministradorFixture> { }
    public class AdministradorFixture : IntegrationFixture<StartupTest>
    {
        public AdministradorFixture()
        {

        }
    }
}