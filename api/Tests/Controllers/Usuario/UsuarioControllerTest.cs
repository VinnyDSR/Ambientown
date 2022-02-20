using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tests.Fixtures;
using Tests.Stubs.Configuracao;
using Tests.Stubs.Personagem;
using Tests.Stubs.Usuario;
using Xunit;

namespace Tests.Controllers.Usuario
{
    [Collection(nameof(UsuarioAdministradorFixtureCollection))]
    public class UsuarioControllerTest
    {
        private readonly AdministradorFixture testsFixture;

        public UsuarioControllerTest(AdministradorFixture testsFixture)
        {
            this.testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Cadastro com sucesso")]
        [Trait("Categoria", "Novo Usuário")]
        public async Task QuandoCriarNovoUsuario_DeveRetornar_Sucesso()
        {
            //Arrange
            var personagemId = CadastrarPersonagemRetornarId(testsFixture.Client).Result;
            var configuracaoId = CadastrarConfiguracaoRetornarId(testsFixture.Client).Result;
            var dto = NovoUsuarioDtoStub.Completo(configuracaoId, personagemId);            

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/usuarios", dto);

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Cadastro sem apelido")]
        [Trait("Categoria", "Novo Usuário")]
        public async Task QuandoCriarUsuarioSemApelido_DeveRetornar_BadRequest()
        {
            //Arrange
            var personagemId = CadastrarPersonagemRetornarId(testsFixture.Client).Result;
            var configuracaoId = CadastrarConfiguracaoRetornarId(testsFixture.Client).Result;
            var dto = NovoUsuarioDtoStub.SemApelido(configuracaoId, personagemId);

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Cadastro informando apelido com mais de 50 caracteres")]
        [Trait("Categoria", "Novo Usuário")]
        public async Task QuandoCriarUsuarioApelidoMaisDe50Caracteres_DeveRetornar_BadRequest()
        {
            //Arrange
            var personagemId = CadastrarPersonagemRetornarId(testsFixture.Client).Result;
            var configuracaoId = CadastrarConfiguracaoRetornarId(testsFixture.Client).Result;
            var dto = NovoUsuarioDtoStub.ApelidoMaisDe50Caracteres(configuracaoId, personagemId);

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Cadastro informando apelido já cadastrado")]
        [Trait("Categoria", "Novo Usuário")]
        public async Task QuandoCriarUsuarioApelidoJaCadastrado_DeveRetornar_BadRequest()
        {
            //Arrange
            var personagemId = CadastrarPersonagemRetornarId(testsFixture.Client).Result;
            var configuracaoId = CadastrarConfiguracaoRetornarId(testsFixture.Client).Result;
            var dto = NovoUsuarioDtoStub.ApelidoJaCadastrado("jaCadastrado", configuracaoId, personagemId);
            var dtoRepetido = NovoUsuarioDtoStub.ApelidoJaCadastrado("jaCadastrado", configuracaoId, personagemId);

            //Act
            await testsFixture.Client.PostAsJsonAsync("api/usuarios", dto);
            var response2 = await testsFixture.Client.PostAsJsonAsync("api/usuarios", dtoRepetido);

            //Assert
            Assert.False(response2.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Conflict, response2.StatusCode);
        }

        [Fact(DisplayName = "Cadastro sem email")]
        [Trait("Categoria", "Novo Usuário")]
        public async Task QuandoCriarUsuarioSemEmail_DeveRetornar_BadRequest()
        {
            //Arrange
            var personagemId = CadastrarPersonagemRetornarId(testsFixture.Client).Result;
            var configuracaoId = CadastrarConfiguracaoRetornarId(testsFixture.Client).Result;
            var dto = NovoUsuarioDtoStub.SemEmail(configuracaoId, personagemId);

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Cadastro informando email inválido")]
        [Trait("Categoria", "Novo Usuário")]
        public async Task QuandoCriarUsuarioEmailInvalido_DeveRetornar_BadRequest()
        {
            //Arrange
            var personagemId = CadastrarPersonagemRetornarId(testsFixture.Client).Result;
            var configuracaoId = CadastrarConfiguracaoRetornarId(testsFixture.Client).Result;
            var dto = NovoUsuarioDtoStub.EmailInvalido(configuracaoId, personagemId);

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Cadastro informando email com mais de 100 caracteres")]
        [Trait("Categoria", "Novo Usuário")]
        public async Task QuandoCriarUsuarioEmailMaisDe100Caracteres_DeveRetornar_BadRequest()
        {
            //Arrange
            var personagemId = CadastrarPersonagemRetornarId(testsFixture.Client).Result;
            var configuracaoId = CadastrarConfiguracaoRetornarId(testsFixture.Client).Result;
            var dto = NovoUsuarioDtoStub.EmailMaisDe100Caracteres(configuracaoId, personagemId);

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Cadastro sem senha")]
        [Trait("Categoria", "Novo Usuário")]
        public async Task QuandoCriarUsuarioSemSenha_DeveRetornar_BadRequest()
        {
            //Arrange
            var personagemId = CadastrarPersonagemRetornarId(testsFixture.Client).Result;
            var configuracaoId = CadastrarConfiguracaoRetornarId(testsFixture.Client).Result;
            var dto = NovoUsuarioDtoStub.SemSenha(configuracaoId, personagemId);

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Cadastro informando senha menor que 8 caracteres")]
        [Trait("Categoria", "Novo Usuário")]
        public async Task QuandoCriarUsuarioInformandoSenhaMenorQue8Caracteres_DeveRetornar_BadRequest()
        {
            //Arrange
            var personagemId = CadastrarPersonagemRetornarId(testsFixture.Client).Result;
            var configuracaoId = CadastrarConfiguracaoRetornarId(testsFixture.Client).Result;
            var dto = NovoUsuarioDtoStub.SenhaMenorQue8Caracteres(configuracaoId, personagemId);

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Cadastro informando senha maior que 100 caracteres")]
        [Trait("Categoria", "Novo Usuário")]
        public async Task QuandoCriarUsuarioInformandoSenhaMaiorQue100Caracteres_DeveRetornar_BadRequest()
        {
            //Arrange
            var personagemId = CadastrarPersonagemRetornarId(testsFixture.Client).Result;
            var configuracaoId = CadastrarConfiguracaoRetornarId(testsFixture.Client).Result;
            var dto = NovoUsuarioDtoStub.SenhaMaiorQue100Caracteres(configuracaoId, personagemId);

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Cadastro sem configuração")]
        [Trait("Categoria", "Novo Usuário")]
        public async Task QuandoCriarUsuarioSemConfiguracao_DeveRetornar_BadRequest()
        {
            //Arrange
            var personagemId = CadastrarPersonagemRetornarId(testsFixture.Client).Result;
            var dto = NovoUsuarioDtoStub.SemConfiguracao(personagemId);

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Cadastro sem personagem")]
        [Trait("Categoria", "Novo Usuário")]
        public async Task QuandoCriarUsuarioSemPersonagem_DeveRetornar_BadRequest()
        {
            //Arrange
            var configuracaoId = CadastrarConfiguracaoRetornarId(testsFixture.Client).Result;
            var dto = NovoUsuarioDtoStub.SemPersonagem(configuracaoId);

            //Act
            var response = await testsFixture.Client.PostAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar com sucesso")]
        [Trait("Categoria", "Atualizar Usuário")]
        public async Task QuandoAtualizarPersonagemValido_DeveRetornar_Sucesso()
        {
            //Arrange
            var personagemId = await CadastrarPersonagemRetornarId(testsFixture.Client);
            var configuracaoId = await CadastrarConfiguracaoRetornarId(testsFixture.Client);

            var id = await CadastrarUsuarioRetornarId(testsFixture.Client, configuracaoId, personagemId);

            var dto = AtualizarUsuarioDtoStub.Completo(id, configuracaoId, personagemId);

            //Act
            var response = await testsFixture.Client.PutAsJsonAsync("api/usuarios", dto);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar informando Id inexistente")]
        [Trait("Categoria", "Atualizar Usuário")]
        public async Task QuandoAtualizarPersonagem_IdInexistente_DeveRetornar_NotFound()
        {
            //Act
            var response = await testsFixture.Client.PutAsJsonAsync("api/usuarios", AtualizarUsuarioDtoStub.Completo(0, 1, 1));

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar sem apelido")]
        [Trait("Categoria", "Atualizar Usuário")]
        public async Task QuandoAtualizarUsuarioSemApelido_DeveRetornar_BadRequest()
        {
            //Arrange
            var configuracaoId = await CadastrarConfiguracaoRetornarId(testsFixture.Client);
            var personagemId = await CadastrarPersonagemRetornarId(testsFixture.Client);
            var id = await CadastrarUsuarioRetornarId(testsFixture.Client, configuracaoId, personagemId);

            var dto = AtualizarUsuarioDtoStub.SemApelido(id, configuracaoId, personagemId);

            //Act
            var response = await testsFixture.Client.PutAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar informando apelido com mais de 50 caracteres")]
        [Trait("Categoria", "Atualizar Usuário")]
        public async Task QuandoAtualizarApelidoMaisDe50Caracteres_DeveRetornar_BadRequest()
        {
            //Arrange
            var configuracaoId = await CadastrarConfiguracaoRetornarId(testsFixture.Client);
            var personagemId = await CadastrarPersonagemRetornarId(testsFixture.Client);
            var id = await CadastrarUsuarioRetornarId(testsFixture.Client, configuracaoId, personagemId);

            var dto = AtualizarUsuarioDtoStub.ApelidoMaisDe50Caracteres(id, configuracaoId, personagemId);

            //Act
            var response = await testsFixture.Client.PutAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar informando apelido já cadastrado")]
        [Trait("Categoria", "Atualizar Usuário")]
        public async Task QuandoAtualizarApelidoJaCadastrado_DeveRetornar_Conflict()
        {
            //Arrange
            var configuracaoId = await CadastrarConfiguracaoRetornarId(testsFixture.Client);
            var personagemId = await CadastrarPersonagemRetornarId(testsFixture.Client);
            var id = await CadastrarUsuarioRetornarId(testsFixture.Client, configuracaoId, personagemId);

            var dto = NovoUsuarioDtoStub.ApelidoJaCadastrado("jaCadastrado", configuracaoId, personagemId);
            await testsFixture.Client.PostAsJsonAsync("api/usuarios", dto);

            var dtoRepetido = AtualizarUsuarioDtoStub.ApelidoJaCadastrado(id, "jaCadastrado", configuracaoId, personagemId);

            //Act
            var response = await testsFixture.Client.PutAsJsonAsync("api/usuarios", dtoRepetido);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar sem email")]
        [Trait("Categoria", "Atualizar Usuário")]
        public async Task QuandoAtualizarSemEmail_DeveRetornar_BadRequest()
        {
            //Arrange
            var configuracaoId = await CadastrarConfiguracaoRetornarId(testsFixture.Client);
            var personagemId = await CadastrarPersonagemRetornarId(testsFixture.Client);
            var id = await CadastrarUsuarioRetornarId(testsFixture.Client, configuracaoId, personagemId);

            var dto = AtualizarUsuarioDtoStub.SemEmail(id, configuracaoId, personagemId);

            //Act
            var response = await testsFixture.Client.PutAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar informando email inválido")]
        [Trait("Categoria", "Atualizar Usuário")]
        public async Task QuandoAtualizarEmailInvalido_DeveRetornar_BadRequest()
        {
            //Arrange
            var configuracaoId = await CadastrarConfiguracaoRetornarId(testsFixture.Client);
            var personagemId = await CadastrarPersonagemRetornarId(testsFixture.Client);
            var id = await CadastrarUsuarioRetornarId(testsFixture.Client, configuracaoId, personagemId);

            var dto = AtualizarUsuarioDtoStub.EmailInvalido(id, configuracaoId, personagemId);

            //Act
            var response = await testsFixture.Client.PutAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar informando email com mais de 100 caracteres")]
        [Trait("Categoria", "Atualizar Usuário")]
        public async Task QuandoAtualizarEmailMaisDe100Caracteres_DeveRetornar_BadRequest()
        {
            //Arrange
            var configuracaoId = await CadastrarConfiguracaoRetornarId(testsFixture.Client);
            var personagemId = await CadastrarPersonagemRetornarId(testsFixture.Client);
            var id = await CadastrarUsuarioRetornarId(testsFixture.Client, configuracaoId, personagemId);

            var dto = AtualizarUsuarioDtoStub.EmailMaisDe100Caracteres(id, configuracaoId, personagemId);

            //Act
            var response = await testsFixture.Client.PutAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar sem senha")]
        [Trait("Categoria", "Atualizar Usuário")]
        public async Task QuandoAtualizarSemSenha_DeveRetornar_BadRequest()
        {
            //Arrange
            var configuracaoId = await CadastrarConfiguracaoRetornarId(testsFixture.Client);
            var personagemId = await CadastrarPersonagemRetornarId(testsFixture.Client);
            var id = await CadastrarUsuarioRetornarId(testsFixture.Client, configuracaoId, personagemId);

            var dto = AtualizarUsuarioDtoStub.SemSenha(id, configuracaoId, personagemId);

            //Act
            var response = await testsFixture.Client.PutAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar informando senha menor que 8 caracteres")]
        [Trait("Categoria", "Atualizar Usuário")]
        public async Task QuandoAtualizarSenhaMenorQue8Caracteres_DeveRetornar_BadRequest()
        {
            //Arrange
            var configuracaoId = await CadastrarConfiguracaoRetornarId(testsFixture.Client);
            var personagemId = await CadastrarPersonagemRetornarId(testsFixture.Client);
            var id = await CadastrarUsuarioRetornarId(testsFixture.Client, configuracaoId, personagemId);

            var dto = AtualizarUsuarioDtoStub.SenhaMenorQue8Caracteres(id, configuracaoId, personagemId);

            //Act
            var response = await testsFixture.Client.PutAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar informando senha maior que 100 caracteres")]
        [Trait("Categoria", "Atualizar Usuário")]
        public async Task QuandoAtualizarSenhaMaiorQue100Caracteres_DeveRetornar_BadRequest()
        {
            //Arrange
            var configuracaoId = await CadastrarConfiguracaoRetornarId(testsFixture.Client);
            var personagemId = await CadastrarPersonagemRetornarId(testsFixture.Client);
            var id = await CadastrarUsuarioRetornarId(testsFixture.Client, configuracaoId, personagemId);

            var dto = AtualizarUsuarioDtoStub.SenhaMaiorQue100Caracteres(id, configuracaoId, personagemId);

            //Act
            var response = await testsFixture.Client.PutAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar sem configuração")]
        [Trait("Categoria", "Atualizar Usuário")]
        public async Task QuandoAtualizarSemConfiguracao_DeveRetornar_BadRequest()
        {
            //Arrange
            var configuracaoId = await CadastrarConfiguracaoRetornarId(testsFixture.Client);
            var personagemId = await CadastrarPersonagemRetornarId(testsFixture.Client);
            var id = await CadastrarUsuarioRetornarId(testsFixture.Client, configuracaoId, personagemId);

            var dto = AtualizarUsuarioDtoStub.SemConfiguracao(id, personagemId);

            //Act
            var response = await testsFixture.Client.PutAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Atualizar sem personagem")]
        [Trait("Categoria", "Atualizar Usuário")]
        public async Task QuandoAtualizarSemPersonagem_DeveRetornar_BadRequest()
        {
            //Arrange
            var configuracaoId = await CadastrarConfiguracaoRetornarId(testsFixture.Client);
            var personagemId = await CadastrarPersonagemRetornarId(testsFixture.Client);
            var id = await CadastrarUsuarioRetornarId(testsFixture.Client, configuracaoId, personagemId);

            var dto = AtualizarUsuarioDtoStub.SemPersonagem(id, configuracaoId);

            //Act
            var response = await testsFixture.Client.PutAsJsonAsync("api/usuarios", dto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Obtém por ID existente")]
        [Trait("Categoria", "Consultar por Id")]
        public async Task QuandoConsultarPorId_DeveRetornar_UmRegistroDeUsuario()
        {
            //Arrange
            var id = 1;

            var configuracaoId = await CadastrarConfiguracaoRetornarId(testsFixture.Client);
            var personagemId = await CadastrarPersonagemRetornarId(testsFixture.Client);
            var dto = NovoUsuarioDtoStub.Completo(configuracaoId, personagemId);

            //Act
            await testsFixture.Client.PostAsJsonAsync("api/usuarios", dto);
            var response = await testsFixture.Client.GetAsync($"api/usuarios/{id}");

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
            var response = await testsFixture.Client.GetAsync($"api/usuarios/{id}");

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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

        private async Task<int> CadastrarConfiguracaoRetornarId(HttpClient client)
        {
            var response = await CadastrarConfiguracao(client);

            int id = ObterIdAposNovoCadastro(response);

            return id;
        }

        private async Task<int> CadastrarUsuarioRetornarId(HttpClient client, int configuracaoId, int personagemId)
        {
            var response = await CadastrarUsuario(client, configuracaoId, personagemId);

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