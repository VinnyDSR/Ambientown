using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tests.Fixtures;
using Tests.Stubs.Personagem;
using Xunit;

namespace Tests.Controllers.Personagem
{
    [Collection(nameof(UsuarioAdministradorFixtureCollection))]
    public class PersonagemControllerTest
    {
        private readonly AdministradorFixture testsFixture;

        public PersonagemControllerTest(AdministradorFixture testsFixture)
        {
            this.testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Cadastro com sucesso")]
        [Trait("Categoria", "Novo Personagem")]
        public async Task QuandoCriarNovoPersonagem_DeveRetornar_Sucesso()
        {
            //Arrange
            var dto = NovoPersonagemDtoStub.Completo(1);

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/personagens", dto);

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Cadastro sem gênero")]
        [Trait("Categoria", "Novo Personagem")]
        public async Task QuandoCriarPersonagemSemGenero_DeveRetornar_BadRequest()
        {
            //Arrange
            var dto = NovoPersonagemDtoStub.SemGenero(1);

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/personagens", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Cadastro sem pele")]
        [Trait("Categoria", "Novo Personagem")]
        public async Task QuandoCriarPersonagemSemPele_DeveRetornar_BadRequest()
        {
            //Arrange
            var dto = NovoPersonagemDtoStub.SemPele(1);

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/personagens", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Cadastro sem roupa")]
        [Trait("Categoria", "Novo Personagem")]
        public async Task QuandoCriarPersonagemSemRoupa_DeveRetornar_BadRequest()
        {
            //Arrange
            var dto = NovoPersonagemDtoStub.SemRoupa();

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/personagens", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar com sucesso")]
        [Trait("Categoria", "Atualizar Personagem")]
        public async Task QuandoAtualizarPersonagemValido_DeveRetornar_Sucesso()
        {
            //Arrange
            var id = await CadastrarPersonagemRetornarId(testsFixture.Client);

            var dto = AtualizarPersonagemDtoStub.Completo(id, 1);

            //Act
            var response = await testsFixture.Client.PutAsJsonAsync("api/personagens", dto);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar informando Id inexistente")]
        [Trait("Categoria", "Atualizar Personagem")]
        public async Task QuandoAtualizarPersonagem_IdInexistente_DeveRetornar_NotFound()
        {
            //Act
            var response = await testsFixture.Client.PutAsJsonAsync("api/personagens", AtualizarPersonagemDtoStub.Completo(0, 1));

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar sem gênero")]
        [Trait("Categoria", "Atualizar Personagem")]
        public async Task QuandoAtualizarPersonagemSemGenero_DeveRetornar_BadRequest()
        {
            //Arrange
            var id = await CadastrarPersonagemRetornarId(testsFixture.Client);

            var dto = AtualizarPersonagemDtoStub.SemGenero(id, 1);

            //Act
            var response = await testsFixture.Client.PutAsJsonAsync("api/personagens", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar sem pele")]
        [Trait("Categoria", "Atualizar Personagem")]
        public async Task QuandoAtualizarPersonagemSemPele_DeveRetornar_BadRequest()
        {
            //Arrange
            var id = await CadastrarPersonagemRetornarId(testsFixture.Client);

            var dto = AtualizarPersonagemDtoStub.SemPele(id, 1);

            //Act
            var response = await testsFixture.Client.PutAsJsonAsync("api/personagens", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar sem roupa")]
        [Trait("Categoria", "Atualizar Personagem")]
        public async Task QuandoAtualizarPersonagemSemRoupa_DeveRetornar_BadRequest()
        {
            //Arrange
            var id = await CadastrarPersonagemRetornarId(testsFixture.Client);

            var dto = AtualizarPersonagemDtoStub.SemRoupa(id);

            //Act
            var response = await testsFixture.Client.PutAsJsonAsync("api/personagens", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Obtém por ID existente")]
        [Trait("Categoria", "Consultar por Id")]
        public async Task QuandoConsultarPorId_DeveRetornar_UmRegistroDePersonagem()
        {
            //Arrange
            var id = 1;

            var dto = NovoPersonagemDtoStub.Completo(1);

            //Act
            await testsFixture.Client.PostAsJsonAsync("api/personagens", dto);
            var response = await testsFixture.Client.GetAsync($"api/personagens/{id}");

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
            var response = await testsFixture.Client.GetAsync($"api/personagens/{id}");

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact(DisplayName = "Excluir com Id inexistente")]
        [Trait("Categoria", "Excluir Personagem")]
        public async Task QuandoRemoverPorId_PersonagemNaoEcontrado_DeveRetornar_NotFound()
        {
            //Arrange
            var id = 123;

            //Act
            var response = await testsFixture.Client.DeleteAsync($"api/personagens/{id}");

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact(DisplayName = "Excluir com Id existente")]
        [Trait("Categoria", "Excluir Personagem")]
        public async Task QuandoRemoverPorId_DeveRetornar_Sucesso_NoContent()
        {
            //Arrange
            var id = await CadastrarPersonagemRetornarId(testsFixture.Client);

            //Act
            var response = await testsFixture.Client.DeleteAsync($"api/personagens/{id}");

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        private async Task<int> CadastrarPersonagemRetornarId(HttpClient client)
        {
            var response = await CadastrarPersonagem(client);

            int id = ObterIdAposNovoCadastro(response);

            return id;
        }

        private async Task<int[]> CadastrarPersonagem(HttpClient client, int qtde)
        {
            var ids = new int[qtde];

            for (var i = 0; i < qtde; i++)
            {
                ids[i] = await CadastrarPersonagemRetornarId(client);
            }

            return ids;
        }

        private async Task<HttpResponseMessage> CadastrarPersonagem(HttpClient client)
        {
            var dtoNovo = NovoPersonagemDtoStub.Completo(1);

            var response = await client.PostAsJsonAsync("api/personagens", dtoNovo);

            response.EnsureSuccessStatusCode();

            return response;
        }

        private int ObterIdAposNovoCadastro(HttpResponseMessage postResponse)
        {
            return Convert.ToInt32(postResponse.Headers.Location.LocalPath.Split("/").Last());
        }
    }
}