//using AmbienTown.Dto.Usuario;
//using AmbienTown.Enums;
//using AmbienTown.Models;
//using AmbienTown.Repositories.Interfaces;
//using AmbienTown.Services;
//using AmbienTown.Services.Interfaces;
//using AutoMapper;
//using Bogus;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using Tests.Stubs.Usuario;
//using TimeSheet.Api.Utils.Result;
//using Xunit;

//namespace Tests.Services
//{
//    public class UsuarioServiceTest : ServicesTest
//    {
//        private readonly UsuarioService _service;
//        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
//        private readonly Mock<IUsuarioService> _usuarioServiceMock;
//        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
//        private readonly Mock<IPersonagemService> _personagemServiceMock;
//        private readonly Mock<IConfiguracaoService> _configuracaoServiceMock;
//        private readonly Faker faker;

//        public UsuarioServiceTest()
//        {
//            this._personagemServiceMock = new Mock<IPersonagemService>();
//            this._configuracaoServiceMock = new Mock<IConfiguracaoService>();
//            _service = new UsuarioService(_usuarioRepositoryMock.Object, _mapperMock.Object);
//            _usuarioServiceMock = new Mock<IUsuarioService>();
//            this.faker = new Faker("pt_BR");
//        }

//        [Fact(DisplayName = "Cadastro com sucesso")]
//        [Trait("Categoria", "Novo Usuário")]
//        public void QuandoCadastrar_DeveValidarEChamarMetodoDoRepositorio()
//        {
//            //Arrange
//            _configuracaoServiceMock.Setup(x => x.Existe(It.IsAny<int>())).Returns(OperationResult.OK());
//            _personagemServiceMock.Setup(x => x.Existe(It.IsAny<int>())).Returns(OperationResult.OK());
//            _usuarioRepositoryMock.Setup(x => x.Create(It.IsAny<Usuario>())).Returns(1);
//            _usuarioServiceMock.Setup(x => x.Cadastrar(It.IsAny<Usuario>())).Returns(OperationResult.Created(1));

//            //var dto = NovoUsuarioDtoStub.Completo(1, 1);
//            var dto = new Usuario()
//            {
//                Apelido = faker.Random.AlphaNumeric(20),
//                Ativo = true,
//                Email = faker.Person.Email,
//                Senha = faker.Random.AlphaNumeric(15),
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            _mapperMock.Setup(x => x.Map<Usuario>(It.IsAny<Usuario>()))
//                .Returns(new Usuario { Apelido = dto.Apelido, ConfiguracaoId = dto.ConfiguracaoId, Email = dto.Email, PersonagemId = dto.PersonagemId, Senha = dto.Senha });

//            //Act
//            //var resultMock = _usuarioServiceMock.Object.Cadastrar(dto);
//            var result = _service.Cadastrar(dto);

//            //Assert
//            _usuarioRepositoryMock.Verify(v => v.Create(It.IsAny<Usuario>()), Times.Once);

//            Assert.True(result.Success);
//            Assert.Equal(OperationResultType.Created, result.Type);
//        }

//        [Fact(DisplayName = "Cadastro sem informar apelido")]
//        [Trait("Categoria", "Novo Usuário")]
//        public void QuandoCadastrarSemApelido_DeveRetornar_ApelidoEhObrigatorio()
//        {
//            //Arrange
//            _configuracaoServiceMock.Setup(x => x.Existe(It.IsAny<int>())).Returns(OperationResult.OK());
//            _personagemServiceMock.Setup(x => x.Existe(It.IsAny<int>())).Returns(OperationResult.OK());
//            _usuarioRepositoryMock.Setup(x => x.Create(It.IsAny<Usuario>())).Returns(1);
//            _usuarioServiceMock.Setup(x => x.Cadastrar(It.IsAny<Usuario>())).Returns(OperationResult.Created(1));

//            //var dto = NovoUsuarioDtoStub.SemApelido(1, 1);
//            var dto = new Usuario()
//            {
//                Ativo = true,
//                Email = faker.Person.Email,
//                Senha = faker.Random.AlphaNumeric(15),
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            _mapperMock.Setup(x => x.Map<Usuario>(It.IsAny<Usuario>()))
//                .Returns(new Usuario { ConfiguracaoId = dto.ConfiguracaoId, Email = dto.Email, PersonagemId = dto.PersonagemId, Senha = dto.Senha });

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Cadastrar(dto);
//            var result = _service.Cadastrar(dto);

//            //Assert
//            Assert.False(result.Success);
//            Assert.Equal(OperationResultType.ValidationError, result.Type);
//        }

//        [Fact(DisplayName = "Cadastro informando apelido com mais de 50 caracteres")]
//        [Trait("Categoria", "Novo Usuário")]
//        public void QuandoCadastrarApelidoMaisDe50Caracteres_DeveRetornar_ApelidoDeveTerMenosDe50Caracteres()
//        {
//            //Arrange
//            _configuracaoServiceMock.Setup(x => x.Existe(It.IsAny<int>())).Returns(OperationResult.OK());
//            _personagemServiceMock.Setup(x => x.Existe(It.IsAny<int>())).Returns(OperationResult.OK());
//            _usuarioRepositoryMock.Setup(x => x.Create(It.IsAny<Usuario>())).Returns(1);
//            _usuarioServiceMock.Setup(x => x.Cadastrar(It.IsAny<Usuario>())).Returns(OperationResult.Created(1));

//            //var dto = NovoUsuarioDtoStub.ApelidoMaisDe50Caracteres(1, 1);
//            var dto = new Usuario()
//            {
//                Apelido = faker.Random.AlphaNumeric(51),
//                Ativo = true,
//                Email = faker.Person.Email,
//                Senha = faker.Random.AlphaNumeric(15),
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Cadastrar(dto);
//            var result = _service.Cadastrar(dto);

//            //Assert
//            Assert.False(result.Success);
//            Assert.Equal(OperationResultType.ValidationError, result.Type);
//        }

//        [Fact(DisplayName = "Cadastro sem informar email")]
//        [Trait("Categoria", "Novo Usuário")]
//        public void QuandoCadastrarSemEmail_DeveRetornar_EmailEhObrigatorio()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Create(It.IsAny<Usuario>())).Returns(1);
//            _usuarioServiceMock.Setup(x => x.Cadastrar(It.IsAny<Usuario>())).Returns(OperationResult.Created(1));

//            //var dto = NovoUsuarioDtoStub.SemEmail(1, 1);
//            var dto = new Usuario()
//            {
//                Apelido = faker.Random.AlphaNumeric(20),
//                Ativo = true,
//                Senha = faker.Random.AlphaNumeric(15),
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Cadastrar(dto);
//            var result = _service.Cadastrar(dto);

//            //Assert
//            Assert.False(result.Success);
//            Assert.Equal(OperationResultType.ValidationError, result.Type);
//        }

//        [Fact(DisplayName = "Cadastro informando email inválido")]
//        [Trait("Categoria", "Novo Usuário")]
//        public void QuandoCadastrarEmailInvalido_DeveRetornar_EmailInvalido()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Create(It.IsAny<Usuario>())).Returns(1);
//            _usuarioServiceMock.Setup(x => x.Cadastrar(It.IsAny<Usuario>())).Returns(OperationResult.Created(1));

//            //var dto = NovoUsuarioDtoStub.EmailInvalido(1, 1);
//            var dto = new Usuario()
//            {
//                Apelido = faker.Random.AlphaNumeric(20),
//                Ativo = true,
//                Email = faker.Random.String2(10),
//                Senha = faker.Random.AlphaNumeric(15),
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Cadastrar(dto);
//            var result = _service.Cadastrar(dto);

//            //Assert
//            Assert.False(result.Success);
//            Assert.Equal(OperationResultType.ValidationError, result.Type);
//        }

//        [Fact(DisplayName = "Cadastro informando email com mais de 100 caracteres")]
//        [Trait("Categoria", "Novo Usuário")]
//        public void QuandoCadastrarEmailMaisDe100Caracteres_DeveRetornar_EmailDeveTerNoMaximo100Caracteres()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Create(It.IsAny<Usuario>())).Returns(1);
//            _usuarioServiceMock.Setup(x => x.Cadastrar(It.IsAny<Usuario>())).Returns(OperationResult.Created(1));

//            //var dto = NovoUsuarioDtoStub.EmailMaisDe100Caracteres(1, 1);
//            var dto = new Usuario()
//            {
//                Apelido = faker.Random.AlphaNumeric(20),
//                Ativo = true,
//                Email = faker.Random.String2(92) + "@mail.com",
//                Senha = faker.Random.AlphaNumeric(15),
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Cadastrar(dto);
//            var result = _service.Cadastrar(dto);

//            //Assert
//            Assert.False(result.Success);
//            Assert.Equal(OperationResultType.ValidationError, result.Type);
//        }

//        [Fact(DisplayName = "Cadastro sem informar senha")]
//        [Trait("Categoria", "Novo Usuário")]
//        public void QuandoCadastrarUsuarioSemSenha_DeveRetornar_SenhaEhObrigatorio()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Create(It.IsAny<Usuario>())).Returns(1);
//            _usuarioServiceMock.Setup(x => x.Cadastrar(It.IsAny<Usuario>())).Returns(OperationResult.Created(1));

//            //var dto = NovoUsuarioDtoStub.SemSenha(1, 1);
//            var dto = new Usuario()
//            {
//                Apelido = faker.Random.AlphaNumeric(20),
//                Ativo = true,
//                Email = faker.Person.Email,
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Cadastrar(dto);
//            var result = _service.Cadastrar(dto);

//            //Assert
//            Assert.False(result.Success);
//            Assert.Equal(OperationResultType.ValidationError, result.Type);
//        }

//        [Fact(DisplayName = "Cadastro informando senha menor que 8 caracteres")]
//        [Trait("Categoria", "Novo Usuário")]
//        public void QuandoCadastrarSenhaMenorQue8Caracteres_DeveRetornar_SenhaDeveSerMaiorQue8Caracteres()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Create(It.IsAny<Usuario>())).Returns(1);
//            _usuarioServiceMock.Setup(x => x.Cadastrar(It.IsAny<Usuario>())).Returns(OperationResult.Created(1));

//            //var dto = NovoUsuarioDtoStub.SenhaMenorQue8Caracteres(1, 1);
//            var dto = new Usuario()
//            {
//                Apelido = faker.Random.AlphaNumeric(20),
//                Ativo = true,
//                Email = faker.Person.Email,
//                Senha = faker.Random.AlphaNumeric(7),
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Cadastrar(dto);
//            var result = _service.Cadastrar(dto);

//            //Assert
//            Assert.False(result.Success);
//            Assert.Equal(OperationResultType.ValidationError, result.Type);
//        }

//        [Fact(DisplayName = "Cadastro informando senha maior que 100 caracteres")]
//        [Trait("Categoria", "Novo Usuário")]
//        public void QuandoCadastrarSenhaMaiorQue100Caracteres_DeveRetornar_SenhaDeveSerMenorQue100Caracteres()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Create(It.IsAny<Usuario>())).Returns(1);
//            _usuarioServiceMock.Setup(x => x.Cadastrar(It.IsAny<Usuario>())).Returns(OperationResult.Created(1));

//            //var dto = NovoUsuarioDtoStub.SenhaMaiorQue100Caracteres(1, 1);
//            var dto = new Usuario()
//            {
//                Apelido = faker.Random.AlphaNumeric(20),
//                Ativo = true,
//                Email = faker.Person.Email,
//                Senha = faker.Random.AlphaNumeric(101),
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Cadastrar(dto);
//            var result = _service.Cadastrar(dto);

//            //Assert
//            Assert.False(result.Success);
//            Assert.Equal(OperationResultType.ValidationError, result.Type);
//        }

//        [Fact(DisplayName = "Cadastro sem informar configuração")]
//        [Trait("Categoria", "Novo Usuário")]
//        public void QuandoCadastrarUsuarioSemInformarConfiguracao_DeveRetornar_ConfiguracaoEhObrigatorio()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Create(It.IsAny<Usuario>())).Returns(1);
//            _usuarioServiceMock.Setup(x => x.Cadastrar(It.IsAny<Usuario>())).Returns(OperationResult.Created(1));

//            //var dto = NovoUsuarioDtoStub.SemConfiguracao(1);
//            var dto = new Usuario()
//            {
//                Apelido = faker.Random.AlphaNumeric(20),
//                Ativo = true,
//                Email = faker.Person.Email,
//                Senha = faker.Random.AlphaNumeric(15),
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Cadastrar(dto);
//            var result = _service.Cadastrar(dto);

//            //Assert
//            Assert.False(result.Success);
//            Assert.Equal(OperationResultType.ValidationError, result.Type);
//        }

//        [Fact(DisplayName = "Cadastro sem informar personagem")]
//        [Trait("Categoria", "Novo Usuário")]
//        public void QuandoCadastrarUsuarioSemInformarPersonagem_DeveRetornar_PersonagemEhObrigatorio()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Create(It.IsAny<Usuario>())).Returns(1);
//            _usuarioServiceMock.Setup(x => x.Cadastrar(It.IsAny<Usuario>())).Returns(OperationResult.Created(1));

//            //var dto = NovoUsuarioDtoStub.SemPersonagem(1);
//            var dto = new Usuario()
//            {
//                Apelido = faker.Random.AlphaNumeric(20),
//                Ativo = true,
//                Email = faker.Person.Email,
//                Senha = faker.Random.AlphaNumeric(15),
//                ConfiguracaoId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Cadastrar(dto);
//            var result = _service.Cadastrar(dto);

//            //Assert
//            Assert.False(result.Success);
//            Assert.Equal(OperationResultType.ValidationError, result.Type);
//        }

//        [Fact(DisplayName = "Atualizar com sucesso")]
//        [Trait("Categoria", "Atualizar Usuário")]
//        public void QuandoAtualizar_DeveRetornar_OperationResultNoContent()
//        {
//            //Arrange
//            _usuarioRepositoryMock.SetupSequence(x => x.Exists(It.IsAny<Expression<Func<Usuario, bool>>>()))
//                .Returns(true)
//                .Returns(false);
//            _configuracaoServiceMock.Setup(x => x.Existe(It.IsAny<int>())).Returns(OperationResult.OK());
//            _personagemServiceMock.Setup(x => x.Existe(It.IsAny<int>())).Returns(OperationResult.OK());
//            _usuarioRepositoryMock.Setup(x => x.Update(It.IsAny<Usuario>()));
//            _usuarioServiceMock.Setup(x => x.Atualizar(It.IsAny<Usuario>())).Returns(OperationResult.NoContent());

//            //var dto = AtualizarUsuarioDtoStub.Completo(1, 1, 1);
//            var dto = new Usuario()
//            {
//                Id = 1,
//                Apelido = faker.Random.AlphaNumeric(20),
//                Ativo = true,
//                Email = faker.Person.Email,
//                Senha = faker.Random.AlphaNumeric(15),
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Atualizar(dto);
//            var result = _service.Atualizar(dto);

//            //Assert
//            _usuarioRepositoryMock.Verify(v => v.Update(It.IsAny<Usuario>()), Times.Once);
//            Assert.True(result.Success);
//            Assert.Equal(OperationResultType.Ok, result.Type);
//        }

//        [Fact(DisplayName = "Atualizar com Id inexistente")]
//        [Trait("Categoria", "Atualizar Usuário")]
//        public void QuandoAtualizarComIdInexistente_DeveRetornar_OperationResultNotFound()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
//            _usuarioServiceMock.Setup(x => x.Atualizar(It.IsAny<Usuario>())).Returns(OperationResult.NotFound());

//            //var dto = AtualizarUsuarioDtoStub.Completo(0, 1, 1);
//            var dto = new Usuario()
//            {
//                Id = 0,
//                Apelido = faker.Random.AlphaNumeric(20),
//                Ativo = true,
//                Email = faker.Person.Email,
//                Senha = faker.Random.AlphaNumeric(15),
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Atualizar(dto);
//            var result = _service.Atualizar(dto);

//            //Assert
//            Assert.False(result.Success);
//            Assert.Equal(result.Type, resultMock.Type);
//        }

//        [Fact(DisplayName = "Atualizar sem informar apelido")]
//        [Trait("Categoria", "Atualizar Usuário")]
//        public void QuandoAtualizarSemApelido_DeveRetornar_ApelidoEhObrigatorio()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(true);
//            _usuarioServiceMock.Setup(x => x.Atualizar(It.IsAny<Usuario>())).Returns(OperationResult.ValidationError("Apelido é obrigatório"));

//            //var dto = AtualizarUsuarioDtoStub.SemApelido(1, 1, 1);
//            var dto = new Usuario()
//            {
//                Id = 1,
//                Ativo = true,
//                Email = faker.Person.Email,
//                Senha = faker.Random.AlphaNumeric(15),
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Atualizar(dto);
//            var result = _service.Atualizar(dto);

//            Assert.False(result.Success);
//            Assert.Equal(resultMock.Type, result.Type);
//            Assert.Equal(resultMock.Message, result.Message);
//        }

//        [Fact(DisplayName = "Atualizar informando apelido com mais de 50 caracteres")]
//        [Trait("Categoria", "Atualizar Usuário")]
//        public void QuandoAtualizarApelidoMaisDe50Caracteres_DeveRetornar_ApelidoDeveTerMenosDe50Caracteres()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(true);
//            _usuarioServiceMock.Setup(x => x.Atualizar(It.IsAny<Usuario>())).Returns(OperationResult.ValidationError("Apelido deve ter menos de 50 caracteres"));

//            //var dto = AtualizarUsuarioDtoStub.SemApelido(1, 1, 1);
//            var dto = new Usuario()
//            {
//                Id = 1,
//                Apelido = faker.Random.AlphaNumeric(51),
//                Ativo = true,
//                Email = faker.Person.Email,
//                Senha = faker.Random.AlphaNumeric(15),
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Atualizar(dto);
//            var result = _service.Atualizar(dto);

//            Assert.False(result.Success);
//            Assert.Equal(resultMock.Type, result.Type);
//            Assert.Equal(resultMock.Message, result.Message);
//        }

//        [Fact(DisplayName = "Atualizar informando apelido já cadastrado")]
//        [Trait("Categoria", "Atualizar Usuário")]
//        public void QuandoAtualizarApelidoJaCadastrado_DeveRetornar_ApelidoJaCadastrado()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(true);
//            _usuarioServiceMock.Setup(x => x.Atualizar(It.IsAny<Usuario>())).Returns(OperationResult.Conflict("Usuário com este apelido já foi cadastrado"));

//            //var dto = AtualizarUsuarioDtoStub.Completo(1, 1, 1);
//            var dto = new Usuario()
//            {
//                Id = 1,
//                Apelido = "jaCadastrado",
//                Ativo = true,
//                Email = faker.Person.Email,
//                Senha = faker.Random.AlphaNumeric(15),
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Atualizar(dto);
//            var result = _service.Atualizar(dto);

//            Assert.False(result.Success);
//            Assert.Equal(resultMock.Type, result.Type);
//            Assert.Equal(resultMock.Message, result.Message);
//        }

//        [Fact(DisplayName = "Atualizar sem informar email")]
//        [Trait("Categoria", "Atualizar Usuário")]
//        public void QuandoAtualizarSemEmail_DeveRetornar_EmailEhObrigatorio()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(true);
//            _usuarioServiceMock.Setup(x => x.Atualizar(It.IsAny<Usuario>())).Returns(OperationResult.ValidationError("Email é obrigatório"));

//            //var dto = AtualizarUsuarioDtoStub.SemEmail(1, 1, 1);
//            var dto = new Usuario()
//            {
//                Id = 1,
//                Apelido = faker.Random.AlphaNumeric(20),
//                Ativo = true,
//                Senha = faker.Random.AlphaNumeric(15),
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Atualizar(dto);
//            var result = _service.Atualizar(dto);

//            Assert.False(result.Success);
//            Assert.Equal(resultMock.Type, result.Type);
//            Assert.Equal(resultMock.Message, result.Message);
//        }

//        [Fact(DisplayName = "Atualizar informando email inválido")]
//        [Trait("Categoria", "Atualizar Usuário")]
//        public void QuandoAtualizarEmailInvalido_DeveRetornar_EmailInvalido()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(true);
//            _usuarioServiceMock.Setup(x => x.Atualizar(It.IsAny<Usuario>())).Returns(OperationResult.ValidationError("Email inválido"));

//            //var dto = AtualizarUsuarioDtoStub.EmailInvalido(1, 1, 1);
//            var dto = new Usuario()
//            {
//                Id = 1,
//                Apelido = faker.Random.AlphaNumeric(20),
//                Ativo = true,
//                Email = faker.Random.String2(9),
//                Senha = faker.Random.AlphaNumeric(15),
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Atualizar(dto);
//            var result = _service.Atualizar(dto);

//            Assert.False(result.Success);
//            Assert.Equal(resultMock.Type, result.Type);
//            Assert.Equal(resultMock.Message, result.Message);
//        }

//        [Fact(DisplayName = "Atualizar informando email acima de 100 caracteres")]
//        [Trait("Categoria", "Atualizar Usuário")]
//        public void QuandoAtualizarEmailMaisDe100Caracteres_DeveRetornar_EmailDeveTerNoMaximo100Caracteres()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(true);
//            _usuarioServiceMock.Setup(x => x.Atualizar(It.IsAny<Usuario>())).Returns(OperationResult.ValidationError("Email deve ter no máximo 100 caracteres"));

//            //var dto = AtualizarUsuarioDtoStub.EmailMaisDe100Caracteres(1, 1, 1);
//            var dto = new Usuario()
//            {
//                Id = 1,
//                Apelido = "Apelido teste maluco",
//                Ativo = true,
//                Email = faker.Random.String2(92) + "@mail.com",
//                Senha = faker.Random.AlphaNumeric(15),
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Atualizar(dto);
//            var result = _service.Atualizar(dto);

//            Assert.False(result.Success);
//            Assert.Equal(resultMock.Type, result.Type);
//            Assert.Equal(resultMock.Message, result.Message);
//        }

//        [Fact(DisplayName = "Atualizar sem informar senha")]
//        [Trait("Categoria", "Atualizar Usuário")]
//        public void QuandoAtualizarSemSenha_DeveRetornar_SenhaEhObrigatoria()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(true);
//            _usuarioServiceMock.Setup(x => x.Atualizar(It.IsAny<Usuario>())).Returns(OperationResult.ValidationError("Senha é obrigatório"));

//            //var dto = AtualizarUsuarioDtoStub.SemSenha(1, 1, 1);
//            var dto = new Usuario()
//            {
//                Id = 1,
//                Apelido = faker.Random.AlphaNumeric(20),
//                Ativo = true,
//                Email = faker.Person.Email,
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Atualizar(dto);
//            var result = _service.Atualizar(dto);

//            Assert.False(result.Success);
//            Assert.Equal(resultMock.Type, result.Type);
//            Assert.Equal(resultMock.Message, result.Message);
//        }

//        [Fact(DisplayName = "Atualizar informando senha menor que 8 caracteres")]
//        [Trait("Categoria", "Atualizar Usuário")]
//        public void QuandoAtualizarSenhaMenorQue8Caracteres_DeveRetornar_SenhaDeveSerMaiorQue8Caracteres()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(true);
//            _usuarioServiceMock.Setup(x => x.Atualizar(It.IsAny<Usuario>())).Returns(OperationResult.ValidationError("Senha deve ter no mínimo 8 caracteres"));

//            //var dto = AtualizarUsuarioDtoStub.SenhaMenorQue8Caracteres(1, 1, 1);
//            var dto = new Usuario()
//            {
//                Id = 1,
//                Apelido = faker.Random.AlphaNumeric(20),
//                Ativo = true,
//                Email = faker.Person.Email,
//                Senha = faker.Random.AlphaNumeric(7),
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Atualizar(dto);
//            var result = _service.Atualizar(dto);

//            Assert.False(result.Success);
//            Assert.Equal(resultMock.Type, result.Type);
//            Assert.Equal(resultMock.Message, result.Message);
//        }

//        [Fact(DisplayName = "Atualizar informando senha maior que 100 caracteres")]
//        [Trait("Categoria", "Atualizar Usuário")]
//        public void QuandoAtualizarSenhaMaiorQue100Caracteres_DeveRetornar_SenhaDeveSerMenorQue100Caracteres()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(true);
//            _usuarioServiceMock.Setup(x => x.Atualizar(It.IsAny<Usuario>())).Returns(OperationResult.ValidationError("Senha deve ter no máximo 100 caracteres"));

//            //var dto = AtualizarUsuarioDtoStub.SenhaMaiorQue100Caracteres(1, 1, 1);
//            var dto = new Usuario()
//            {
//                Id = 1,
//                Apelido = faker.Random.AlphaNumeric(20),
//                Ativo = true,
//                Email = faker.Person.Email,
//                Senha = faker.Random.AlphaNumeric(101),
//                ConfiguracaoId = 1,
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Atualizar(dto);
//            var result = _service.Atualizar(dto);

//            Assert.False(result.Success);
//            Assert.Equal(resultMock.Type, result.Type);
//            Assert.Equal(resultMock.Message, result.Message);
//        }

//        [Fact(DisplayName = "Atualizar usuário sem informar configuração")]
//        [Trait("Categoria", "Atualizar Usuário")]
//        public void QuandoAualizarSemInformarConfiguracao_DeveRetornar_ConfiguracaoEhObrigatoria()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(true);
//            _usuarioServiceMock.Setup(x => x.Atualizar(It.IsAny<Usuario>())).Returns(OperationResult.ValidationError("Configuração é obrigatório"));

//            //var dto = AtualizarUsuarioDtoStub.SemConfiguracao(1, 1);
//            var dto = new Usuario()
//            {
//                Id = 1,
//                Apelido = faker.Random.AlphaNumeric(20),
//                Ativo = true,
//                Email = faker.Person.Email,
//                Senha = faker.Random.AlphaNumeric(15),
//                PersonagemId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Atualizar(dto);
//            var result = _service.Atualizar(dto);

//            Assert.False(result.Success);
//            Assert.Equal(resultMock.Type, result.Type);
//            Assert.Equal(resultMock.Message, result.Message);
//        }

//        [Fact(DisplayName = "Atualizar usuário sem informar personagem")]
//        [Trait("Categoria", "Atualizar Usuário")]
//        public void QuandoAtualizarSemInformarPersonagem_DeveRetornar_PersonagemEhObrigatorio()
//        {
//            //Arrange
//            _usuarioRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(true);
//            _usuarioServiceMock.Setup(x => x.Atualizar(It.IsAny<Usuario>())).Returns(OperationResult.ValidationError("Personagem é obrigatório"));

//            //var dto = AtualizarUsuarioDtoStub.SemPersonagem(1, 1);
//            var dto = new Usuario()
//            {
//                Id = 1,
//                Apelido = faker.Random.AlphaNumeric(20),
//                Ativo = true,
//                Email = faker.Person.Email,
//                Senha = faker.Random.AlphaNumeric(15),
//                ConfiguracaoId = 1
//            };

//            //Act
//            var resultMock = _usuarioServiceMock.Object.Atualizar(dto);
//            var result = _service.Atualizar(dto);

//            Assert.False(result.Success);
//            Assert.Equal(resultMock.Type, result.Type);
//            Assert.Equal(resultMock.Message, result.Message);
//        }

//        [Fact(DisplayName = "Obter por Id inexistente")]
//        [Trait("Categoria", "Obter Usuário")]
//        public void QuandoObterPorIdInexistente_DeveRetornar_OperationResultNotFound()
//        {
//            //Assert
//            var data = new List<Usuario>();

//            for (int i = 1; i <= 5; i++)
//            {
//                data.Add(new Usuario()
//                {
//                    Id = i,
//                    Apelido = Faker.Random.String2(20),
//                    Email = Faker.Person.Email,
//                    Ativo = i % 2 == 0,
//                    Senha = Faker.Random.AlphaNumeric(20),
//                    ConfiguracaoId = i,
//                    Configuracao = new Configuracao
//                    {
//                        Id = i,
//                        Idioma = Idioma.PORTUGUES,
//                        Coleta = true,
//                        Fala = true,
//                        Som = true
//                    },
//                    PersonagemId = i,
//                    Personagem = new Personagem
//                    {
//                        Id = i,
//                        Genero = Genero.MASCULINO,
//                        Pele = Pele.PARDO,
//                        RoupaId = 1
//                    }
//                }); ;
//            }

//            var id = 6;

//            _usuarioRepositoryMock.Setup(x => x.GetById(It.IsAny<int>()))
//                .Returns((int i) => data.Find(x => x.Id == i));

//            //Act
//            var result = _service.ObterPorId(id);

//            //Assert
//            Assert.False(result.Success);
//            Assert.Null(result.Value);
//            Assert.Equal(OperationResultType.NotFound, result.Type);
//        }

//        [Fact(DisplayName = "Remover por Id existente")]
//        [Trait("Categoria", "Remover Usuário")]
//        public void QuandoRemover_DeveRetornar_OperationResultNoContent()
//        {
//            //Assert
//            var data = new List<Usuario>();

//            for (int i = 1; i <= 5; i++)
//            {
//                data.Add(new Usuario()
//                {
//                    Id = i,
//                    Apelido = Faker.Random.String2(20),
//                    Email = Faker.Person.Email,
//                    Ativo = i % 2 == 0,
//                    Senha = Faker.Random.AlphaNumeric(20),
//                    ConfiguracaoId = i,
//                    Configuracao = new Configuracao
//                    {
//                        Id = i,
//                        Idioma = Idioma.PORTUGUES,
//                        Coleta = true,
//                        Fala = true,
//                        Som = true
//                    },
//                    PersonagemId = i,
//                    Personagem = new Personagem
//                    {
//                        Id = i,
//                        Genero = Genero.MASCULINO,
//                        Pele = Pele.PARDO,
//                        RoupaId = 1
//                    }
//                }); ;
//            }

//            var id = 3;

//            _usuarioRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int i) => data.Find(c => c.Id == i));
//            _usuarioRepositoryMock.Setup(x => x.Remove(It.IsAny<Usuario>()));

//            //Act
//            var result = _service.Remover(id);

//            //Assert
//            _usuarioRepositoryMock.Verify(v => v.Remove(It.IsAny<Usuario>()), Times.Once);

//            Assert.True(result.Success);
//            Assert.Equal(OperationResultType.NoContent, result.Type);
//        }

//        [Fact(DisplayName = "Remover por Id inexistente")]
//        [Trait("Categoria", "Remover Usuário")]
//        public void QuandoRemoveridInexistente_DeveRetornar_OperationResultNotFound()
//        {
//            //Assert
//            var data = new List<Usuario>();

//            for (int i = 1; i <= 5; i++)
//            {
//                data.Add(new Usuario()
//                {
//                    Id = i,
//                    Apelido = Faker.Random.String2(20),
//                    Email = Faker.Person.Email,
//                    Ativo = i % 2 == 0,
//                    Senha = Faker.Random.AlphaNumeric(20),
//                    ConfiguracaoId = i,
//                    Configuracao = new Configuracao
//                    {
//                        Id = i,
//                        Idioma = Idioma.PORTUGUES,
//                        Coleta = true,
//                        Fala = true,
//                        Som = true
//                    },
//                    PersonagemId = i,
//                    Personagem = new Personagem
//                    {
//                        Id = i,
//                        Genero = Genero.MASCULINO,
//                        Pele = Pele.PARDO,
//                        RoupaId = 1
//                    }
//                }); ;
//            }

//            var id = 6;

//            _usuarioRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int i) => data.Find(c => c.Id == i));

//            //Act
//            var result = _service.Remover(id);

//            //Assert
//            Assert.False(result.Success);
//            Assert.Equal(OperationResultType.NotFound, result.Type);
//        }

//        [Fact(DisplayName = "Verificar se existe por id existente")]
//        [Trait("Categoria", "Verificar existência de usuário")]
//        public void QuandoVerificarSeExisteUsuarioPorIdEAchar_DeveRetornar_True()
//        {
//            //Assert
//            var data = new List<Usuario>();

//            for (int i = 1; i <= 5; i++)
//            {
//                data.Add(new Usuario()
//                {
//                    Id = i,
//                    Apelido = Faker.Random.String2(20),
//                    Email = Faker.Person.Email,
//                    Ativo = i % 2 == 0,
//                    Senha = Faker.Random.AlphaNumeric(20),
//                    ConfiguracaoId = i,
//                    Configuracao = new Configuracao
//                    {
//                        Id = i,
//                        Idioma = Idioma.PORTUGUES,
//                        Coleta = true,
//                        Fala = true,
//                        Som = true
//                    },
//                    PersonagemId = i,
//                    Personagem = new Personagem
//                    {
//                        Id = i,
//                        Genero = Genero.MASCULINO,
//                        Pele = Pele.PARDO,
//                        RoupaId = 1
//                    }
//                }); ;
//            }

//            var id = 2;

//            _usuarioRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(data.Exists(c => c.Id == id));

//            //Act
//            var result = _service.Existe(id);

//            //Assert
//            Assert.True(result.Success);
//        }

//        [Fact(DisplayName = "Verificar se existe por id inexistente")]
//        [Trait("Categoria", "Verificar existência de usuário")]
//        public void QuandoVerificarSeExisteUsuarioPorIdENãoAchar_DeveRetornar_False()
//        {
//            //Assert
//            var data = new List<Usuario>();

//            for (int i = 1; i <= 5; i++)
//            {
//                data.Add(new Usuario()
//                {
//                    Id = i,
//                    Apelido = Faker.Random.String2(20),
//                    Email = Faker.Person.Email,
//                    Ativo = i % 2 == 0,
//                    Senha = Faker.Random.AlphaNumeric(20),
//                    ConfiguracaoId = i,
//                    Configuracao = new Configuracao
//                    {
//                        Id = i,
//                        Idioma = Idioma.PORTUGUES,
//                        Coleta = true,
//                        Fala = true,
//                        Som = true
//                    },
//                    PersonagemId = i,
//                    Personagem = new Personagem
//                    {
//                        Id = i,
//                        Genero = Genero.MASCULINO,
//                        Pele = Pele.PARDO,
//                        RoupaId = 1
//                    }
//                }); ;
//            }

//            var id = 6;

//            _usuarioRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(data.Exists(c => c.Id == id));

//            //Act
//            var result = _service.Existe(id);

//            //Assert
//            Assert.False(result.Success);
//        }
//    }
//}