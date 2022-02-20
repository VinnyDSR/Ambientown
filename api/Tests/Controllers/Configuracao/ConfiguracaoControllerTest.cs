using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tests.Fixtures;
using Tests.Stubs.Configuracao;
using Xunit;

namespace Tests.Controllers.Configuracao
{
    [Collection(nameof(UsuarioAdministradorFixtureCollection))]
    public class ConfiguracaoControllerTest
    {
        private readonly AdministradorFixture _testsFixture;

        public ConfiguracaoControllerTest(AdministradorFixture testsFixture)
        {
            this._testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Cadastro com sucesso")]
        [Trait("Categoria", "Nova Configuração")]
        public async Task QuandoCriarNovaConfiguração_DeveRetornar_Sucesso()
        {
            //Arrange
            var dto = NovaConfiguracaoDtoStub.Completo();

            //Act
            var response = await _testsFixture.Client.PostAsJsonAsync("api/configuracoes", dto);

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Cadastro sem idioma")]
        [Trait("Categoria", "Nova Configuração")]
        public async Task QuandoCriarConfiguracaoSemIdioma_DeveRetornar_BadRequest()
        {
            //Arrange
            var dto = NovaConfiguracaoDtoStub.SemIdioma();

            //Act
            var response = await _testsFixture.Client.PostAsJsonAsync("api/configuracoes", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar com sucesso")]
        [Trait("Categoria", "Atualizar Configuração")]
        public async Task QuandoAtualizarConfiguracaoValida_DeveRetornar_Sucesso()
        {
            //Arrange
            var id = await CadastrarConfiguracaoRetornarId(_testsFixture.Client);

            var dto = AtualizarConfiguracaoDtoStub.Completo(id);

            //Act
            var response = await _testsFixture.Client.PutAsJsonAsync("api/configuracoes", dto);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar informando Id inexistente")]
        [Trait("Categoria", "Atualizar Configuração")]
        public async Task QuandoAtualizarConfiguracao_IdInexistente_DeveRetornar_NotFound()
        {
            //Act
            var response = await _testsFixture.Client.PutAsJsonAsync("api/configuracoes", AtualizarConfiguracaoDtoStub.Completo(0));

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar sem idioma")]
        [Trait("Categoria", "Atualizar Configuração")]
        public async Task QuandoAtualizarConfiguracaoSemIdioma_DeveRetornar_BadRequest()
        {
            //Arrange
            var id = await CadastrarConfiguracaoRetornarId(_testsFixture.Client);

            var dto = AtualizarConfiguracaoDtoStub.SemIdioma(id);

            //Act
            var response = await _testsFixture.Client.PutAsJsonAsync("api/configuracoes", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Obtém por ID existente")]
        [Trait("Categoria", "Consultar por Id")]
        public async Task QuandoConsultarPorId_DeveRetornar_UmRegistroDeConfiguracao()
        {
            //Arrange
            var id = 1;

            var dto = NovaConfiguracaoDtoStub.Completo();

            //Act
            await _testsFixture.Client.PostAsJsonAsync("api/configuracoes", dto);
            var response = await _testsFixture.Client.GetAsync($"api/configuracoes/{id}");

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Obtém por ID inexistente")]
        [Trait("Categoria", "Consultar por Id")]
        public async Task QuandoConsultarPorId_IdNaoEncontrado_DeveRetornar_NotFound()
        {
            //Arrange
            var id = 111;

            //Act
            var response = await _testsFixture.Client.GetAsync($"api/configuracoes/{id}");

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        private async Task<int> CadastrarConfiguracaoRetornarId(HttpClient client)
        {
            var response = await CadastrarConfiguracao(client);

            int id = ObterIdAposNovoCadastro(response);

            return id;
        }

        private async Task<HttpResponseMessage> CadastrarConfiguracao(HttpClient client)
        {
            var dtoNovo = NovaConfiguracaoDtoStub.Completo();

            var response = await client.PostAsJsonAsync("api/configuracoes", dtoNovo);

            response.EnsureSuccessStatusCode();

            return response;
        }

        private int ObterIdAposNovoCadastro(HttpResponseMessage postResponse)
        {
            return Convert.ToInt32(postResponse.Headers.Location.LocalPath.Split("/").Last());
        }
    }
}