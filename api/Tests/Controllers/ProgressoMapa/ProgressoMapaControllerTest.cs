using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tests.Fixtures;
using Tests.Stubs.Configuracao;
using Tests.Stubs.Personagem;
using Tests.Stubs.ProgressoMapa;
using Tests.Stubs.Usuario;
using Xunit;

namespace Tests.Controllers.ProgressoMapa
{
    [Collection(nameof(UsuarioAdministradorFixtureCollection))]
    public class ProgressoMapaControllerTest
    {
        private readonly AdministradorFixture testsFixture;

        public ProgressoMapaControllerTest(AdministradorFixture testsFixture)
        {
            this.testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Cadastro com sucesso")]
        [Trait("Categoria", "Novo ProgressoMapa")]
        public async Task QuandoCriarNovoProgressoMapa_DeveRetornar_Sucesso()
        {
            //Arrange
            var configuracaoId = CadastrarConfiguracaoRetornarId(testsFixture.Client).Result;
            var personagemId = CadastrarPersonagemRetornarId(testsFixture.Client).Result;
            var usuarioId = CadastrarUsuarioRetornarId(testsFixture.Client, configuracaoId, personagemId).Result;
            var dto = NovoProgressoMapaDtoStub.Completo(usuarioId);

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/progressoMapa", dto);

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Cadastro sem mapa")]
        [Trait("Categoria", "Novo ProgressoMapa")]
        public async Task QuandoCriarProgressoMapaSemMapa_DeveRetornar_BadRequest()
        {
            //Arrange
            var personagemId = CadastrarPersonagemRetornarId(testsFixture.Client).Result;
            var configuracaoId = CadastrarConfiguracaoRetornarId(testsFixture.Client).Result;
            var usuarioId = CadastrarUsuarioRetornarId(testsFixture.Client, configuracaoId, personagemId).Result;
            var dto = NovoProgressoMapaDtoStub.SemMapa(usuarioId);

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/progressoMapa", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Cadastro sem tempo")]
        [Trait("Categoria", "Novo ProgressoMapa")]
        public async Task QuandoCriarProgressoMapaSemTempo_DeveRetornar_BadRequest()
        {
            //Arrange
            var personagemId = CadastrarPersonagemRetornarId(testsFixture.Client).Result;
            var configuracaoId = CadastrarConfiguracaoRetornarId(testsFixture.Client).Result;
            var usuarioId = CadastrarUsuarioRetornarId(testsFixture.Client, configuracaoId, personagemId).Result;
            var dto = NovoProgressoMapaDtoStub.SemTempo(usuarioId);

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/progressoMapa", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Cadastro sem data de registro")]
        [Trait("Categoria", "Novo ProgressoMapa")]
        public async Task QuandoCriarProgressoMapaSemDataRegistro_DeveRetornar_BadRequest()
        {
            //Arrange
            var personagemId = CadastrarPersonagemRetornarId(testsFixture.Client).Result;
            var configuracaoId = CadastrarConfiguracaoRetornarId(testsFixture.Client).Result;
            var usuarioId = CadastrarUsuarioRetornarId(testsFixture.Client, configuracaoId, personagemId).Result;
            var dto = NovoProgressoMapaDtoStub.SemDataRegistro(usuarioId);

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/progressoMapa", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Cadastro sem usuário")]
        [Trait("Categoria", "Novo ProgressoMapa")]
        public async Task QuandoCriarProgressoMapaSemUsuario_DeveRetornar_BadRequest()
        {
            //Arrange
            var dto = NovoProgressoMapaDtoStub.SemUsuario();

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/progressoMapa", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Obtém por ID existente")]
        [Trait("Categoria", "Consultar por Id")]
        public async Task QuandoConsultarPorId_DeveRetornar_UmRegistroDeProgressoMapa()
        {
            //Arrange
            var id = 1;

            var configuracaoId = await CadastrarConfiguracaoRetornarId(testsFixture.Client);
            var personagemId = await CadastrarPersonagemRetornarId(testsFixture.Client);
            var usuarioId = await CadastrarUsuarioRetornarId(testsFixture.Client, configuracaoId, personagemId);
            var dto = NovoProgressoMapaDtoStub.Completo(usuarioId);

            //Act
            await testsFixture.Client.PostAsJsonAsync("api/progressoMapa", dto);
            var response = await testsFixture.Client.GetAsync($"api/progressoMapa/{id}");

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
            var response = await testsFixture.Client.GetAsync($"api/progressoMapa/{id}");

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        private async Task<int> CadastrarProgressoMapaRetornarId(HttpClient client)
        {
            var response = await CadastrarProgressoMapa(client);

            int id = ObterIdAposNovoCadastro(response);

            return id;
        }

        private async Task<HttpResponseMessage> CadastrarProgressoMapa(HttpClient client)
        {
            var configuracaoId = CadastrarConfiguracaoRetornarId(testsFixture.Client).Result;
            var personagemId = CadastrarPersonagemRetornarId(testsFixture.Client).Result;
            var usuarioId = CadastrarUsuarioRetornarId(testsFixture.Client, configuracaoId, personagemId).Result;

            var dtoNovo = NovoProgressoMapaDtoStub.Completo(usuarioId);

            var response = await client.PostAsJsonAsync("api/progressoMapa", dtoNovo);

            response.EnsureSuccessStatusCode();

            return response;
        }

        private async Task<int> CadastrarPersonagemRetornarId(HttpClient client)
        {
            var response = await CadastrarPersonagem(client);

            int id = ObterIdAposNovoCadastro(response);

            return id;
        }

        private async Task<HttpResponseMessage> CadastrarPersonagem(HttpClient client)
        {
            var dtoNovo = NovoPersonagemDtoStub.Completo(1);

            var response = await client.PostAsJsonAsync("api/personagens", dtoNovo);

            response.EnsureSuccessStatusCode();

            return response;
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

        private async Task<int> CadastrarUsuarioRetornarId(HttpClient client, int configuracaoId, int personagemId)
        {
            var response = await CadastrarUsuario(client, configuracaoId, personagemId);

            int id = ObterIdAposNovoCadastro(response);

            return id;
        }

        private async Task<HttpResponseMessage> CadastrarUsuario(HttpClient client, int configuracaoId, int personagemId)
        {
            var dtoNovo = NovoUsuarioDtoStub.Completo(configuracaoId, personagemId);

            var response = await client.PostAsJsonAsync("api/usuarios", dtoNovo);

            response.EnsureSuccessStatusCode();

            return response;
        }

        private int ObterIdAposNovoCadastro(HttpResponseMessage postResponse)
        {
            return Convert.ToInt32(postResponse.Headers.Location.LocalPath.Split("/").Last());
        }
    }
}