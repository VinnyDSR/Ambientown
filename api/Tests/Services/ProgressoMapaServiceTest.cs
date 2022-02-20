using AmbienTown.Dto.ProgressoMapa;
using AmbienTown.Enums;
using AmbienTown.Models;
using AmbienTown.Repositories.Interfaces;
using AmbienTown.Services;
using AmbienTown.Services.Interfaces;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Tests.Stubs.ProgressoMapa;
using TimeSheet.Api.Utils.Result;
using Xunit;

namespace Tests.Services
{
    public class ProgressoMapaServiceTest : ServicesTest
    {
        private readonly Mock<IProgressoMapaRepository> _progressoMapaRepositoryMock;
        private readonly Mock<IUsuarioService> _usuarioServiceMock;
        private readonly Mock<IProgressoMapaService> _serviceMock;
        private readonly ProgressoMapaService _service;
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public ProgressoMapaServiceTest()
        {
            _progressoMapaRepositoryMock = new Mock<IProgressoMapaRepository>();
            _usuarioServiceMock = new Mock<IUsuarioService>();
            _serviceMock = new Mock<IProgressoMapaService>();
            _service = new ProgressoMapaService(_progressoMapaRepositoryMock.Object, this.Mapper);
        }

        [Fact(DisplayName = "Cadastro com sucesso")]
        [Trait("Categoria", "Novo ProgressoMapa")]
        public void QuandoCadastrar_DeveValidarEChamarMetodoDoRepositorio()
        {
            //Arrange
            _usuarioServiceMock.Setup(x => x.Existe(It.IsAny<int>())).Returns(OperationResult.OK());
            _progressoMapaRepositoryMock.Setup(x => x.Create(It.IsAny<ProgressoMapa>())).Returns(1);
            _serviceMock.Setup(x => x.Cadastrar(It.IsAny<NovoProgressoMapaDto>())).Returns(OperationResult.Created(1));

            var dto = NovoProgressoMapaDtoStub.Completo(1);

            _mapperMock.Setup(x => x.Map<ProgressoMapa>(It.IsAny<NovoProgressoMapaDto>()))
                .Returns(new ProgressoMapa { Colecionavel = dto.Colecionavel, ColecionavelEspecial = 1, DataRegistro = DateTime.Now, Mapa = Mapa.FOREST, Tempo = 500, UsuarioId = 1 });

            //Act
            var resultMock = _serviceMock.Object.Cadastrar(dto);
            var result = _service.Cadastrar(dto);

            //Assert
            _progressoMapaRepositoryMock.Verify(v => v.Create(It.IsAny<ProgressoMapa>()), Times.Once);

            Assert.True(result.Success);
            Assert.Equal(OperationResultType.Created, result.Type);
            Assert.Equal(result.Value, resultMock.Value);
        }

        [Fact(DisplayName = "Cadastro sem informar mapa")]
        [Trait("Categoria", "Novo ProgressoMapa")]
        public void QuandoCadastrarSemMapa_DeveRetornar_MapaEhObrigatorio()
        {
            //Arrange
            _serviceMock.Setup(x => x.Cadastrar(It.IsAny<NovoProgressoMapaDto>())).Returns(OperationResult.ValidationError<int>("Mapa é obrigatório"));

            var dto = NovoProgressoMapaDtoStub.SemMapa(1);

            //Act
            var resultMock = _serviceMock.Object.Cadastrar(dto);
            var result = _service.Cadastrar(dto);

            Assert.False(result.Success);
            Assert.Equal(resultMock.Type, result.Type);
            Assert.Equal(resultMock.Message, result.Message);
        }

        [Fact(DisplayName = "Cadastro sem informar tempo")]
        [Trait("Categoria", "Novo ProgressoMapa")]
        public void QuandoCadastrarSemTempo_DeveRetornar_TempoEhObrigatorio()
        {
            //Arrange
            _serviceMock.Setup(x => x.Cadastrar(It.IsAny<NovoProgressoMapaDto>())).Returns(OperationResult.ValidationError<int>("Tempo é obrigatório"));

            var dto = NovoProgressoMapaDtoStub.SemTempo(1);

            //Act
            var resultMock = _serviceMock.Object.Cadastrar(dto);
            var result = _service.Cadastrar(dto);

            Assert.False(result.Success);
            Assert.Equal(resultMock.Type, result.Type);
            Assert.Equal(resultMock.Message, result.Message);
        }

        [Fact(DisplayName = "Cadastro sem informar data de registro")]
        [Trait("Categoria", "Novo ProgressoMapa")]
        public void QuandoCadastrarSemDataRegistro_DeveRetornar_DataRegistroEhObrigatorio()
        {
            //Arrange
            _serviceMock.Setup(x => x.Cadastrar(It.IsAny<NovoProgressoMapaDto>())).Returns(OperationResult.ValidationError<int>("Data de registro é obrigatório"));

            var dto = NovoProgressoMapaDtoStub.SemDataRegistro(1);

            //Act
            var resultMock = _serviceMock.Object.Cadastrar(dto);
            var result = _service.Cadastrar(dto);

            Assert.False(result.Success);
            Assert.Equal(resultMock.Type, result.Type);
            Assert.Equal(resultMock.Message, result.Message);
        }

        [Fact(DisplayName = "Cadastro sem informar usuário")]
        [Trait("Categoria", "Novo ProgressoMapa")]
        public void QuandoCadastrarSemUsuario_DeveRetornar_UsuarioEhObrigatorio()
        {
            //Arrange
            _serviceMock.Setup(x => x.Cadastrar(It.IsAny<NovoProgressoMapaDto>())).Returns(OperationResult.ValidationError<int>("Usuário é obrigatório"));

            var dto = NovoProgressoMapaDtoStub.SemUsuario();

            //Act
            var resultMock = _serviceMock.Object.Cadastrar(dto);
            var result = _service.Cadastrar(dto);

            Assert.False(result.Success);
            Assert.Equal(resultMock.Type, result.Type);
            Assert.Equal(resultMock.Message, result.Message);
        }

        [Fact(DisplayName = "Obter por Id existente")]
        [Trait("Categoria", "Obter ProgressoMapa")]
        public void QuandoObterPorIdExiste_DeveRetornar_ObjetoPreenchido()
        {
            //Assert
            var data = new List<ProgressoMapa>();

            for (int i = 1; i <= 5; i++)
            {
                data.Add(new ProgressoMapa()
                {
                    Id = i,
                    Colecionavel = Faker.Random.Number(0, 20),
                    ColecionavelEspecial = Faker.Random.Number(0, 3),
                    DataRegistro = DateTime.Now,
                    Mapa = Mapa.FOREST,
                    Tempo = Faker.Random.Number(30, 5000),                    
                    UsuarioId = i,
                    Usuario = new Usuario
                    {
                        Id = i,
                        Apelido = Faker.Random.String2(20),
                        Email = Faker.Person.Email,
                        Senha = Faker.Random.AlphaNumeric(20),
                        ConfiguracaoId = i,
                        Configuracao = new Configuracao
                        {
                            Id = i,
                            Idioma = Idioma.PORTUGUES,
                            Coleta = true,
                            Fala = true,
                            Som = true
                        }
                    },
                });
            }

            var id = 2;

            _progressoMapaRepositoryMock.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((int i) => data.Find(x => x.Id == i));

            //Act
            var result = _service.ObterPorId(id);

            //Assert
            _progressoMapaRepositoryMock.Verify(v => v.GetById(It.IsAny<int>()), Times.Once);

            Assert.True(result.Success);
            Assert.Equal(OperationResultType.Ok, result.Type);
            Assert.Equal(id, result.Value.Id);
        }

        [Fact(DisplayName = "Obter por Id inexistente")]
        [Trait("Categoria", "Obter ProgressoMapa")]
        public void QuandoObterPorIdInexistente_DeveRetornar_OperationResultNotFound()
        {
            //Assert
            var data = new List<ProgressoMapa>();

            for (int i = 1; i <= 5; i++)
            {
                data.Add(new ProgressoMapa()
                {
                    Id = i,
                    Colecionavel = Faker.Random.Number(0, 20),
                    ColecionavelEspecial = Faker.Random.Number(0, 3),
                    DataRegistro = DateTime.Now,
                    Mapa = Mapa.FOREST,
                    Tempo = Faker.Random.Number(30, 5000),
                    UsuarioId = i,
                    Usuario = new Usuario
                    {
                        Id = i,
                        Apelido = Faker.Random.String2(20),
                        Email = Faker.Person.Email,
                        Senha = Faker.Random.AlphaNumeric(20),
                        ConfiguracaoId = i,
                        Configuracao = new Configuracao
                        {
                            Id = i,
                            Idioma = Idioma.PORTUGUES,
                            Coleta = true,
                            Fala = true,
                            Som = true
                        }
                    },
                });
            }

            var id = 6;

            _progressoMapaRepositoryMock.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((int i) => data.Find(x => x.Id == i));

            //Act
            var result = _service.ObterPorId(id);

            //Assert
            Assert.False(result.Success);
            Assert.Null(result.Value);
            Assert.Equal(OperationResultType.NotFound, result.Type);
        }

        [Fact(DisplayName = "Remover por Id existente")]
        [Trait("Categoria", "Remover ProgressoMapa")]
        public void QuandoRemover_DeveRetornar_OperationResultNoContent()
        {
            //Assert
            var data = new List<ProgressoMapa>();

            for (int i = 1; i <= 5; i++)
            {
                data.Add(new ProgressoMapa()
                {
                    Id = i,
                    Colecionavel = Faker.Random.Number(0, 20),
                    ColecionavelEspecial = Faker.Random.Number(0, 3),
                    DataRegistro = DateTime.Now,
                    Mapa = Mapa.FOREST,
                    Tempo = Faker.Random.Number(30, 5000),
                    UsuarioId = i,
                    Usuario = new Usuario
                    {
                        Id = i,
                        Apelido = Faker.Random.String2(20),
                        Email = Faker.Person.Email,
                        Senha = Faker.Random.AlphaNumeric(20),
                        ConfiguracaoId = i,
                        Configuracao = new Configuracao
                        {
                            Id = i,
                            Idioma = Idioma.PORTUGUES,
                            Coleta = true,
                            Fala = true,
                            Som = true
                        }
                    },
                });
            }

            var id = 3;

            _progressoMapaRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int i) => data.Find(c => c.Id == i));
            _progressoMapaRepositoryMock.Setup(x => x.Remove(It.IsAny<ProgressoMapa>()));

            //Act
            var result = _service.Remover(id);

            //Assert
            _progressoMapaRepositoryMock.Verify(v => v.Remove(It.IsAny<ProgressoMapa>()), Times.Once);

            Assert.True(result.Success);
            Assert.Equal(OperationResultType.NoContent, result.Type);
        }

        [Fact(DisplayName = "Remover por Id inexistente")]
        [Trait("Categoria", "Atualizar ProgressoMapa")]
        public void QuandoRemoveridInexistente_DeveRetornar_OperationResultNotFound()
        {
            //Assert
            var data = new List<ProgressoMapa>();

            for (int i = 1; i <= 5; i++)
            {
                data.Add(new ProgressoMapa()
                {
                    Id = i,
                    Colecionavel = Faker.Random.Number(0, 20),
                    ColecionavelEspecial = Faker.Random.Number(0, 3),
                    DataRegistro = DateTime.Now,
                    Mapa = Mapa.FOREST,
                    Tempo = Faker.Random.Number(30, 5000),
                    UsuarioId = i,
                    Usuario = new Usuario
                    {
                        Id = i,
                        Apelido = Faker.Random.String2(20),
                        Email = Faker.Person.Email,
                        Senha = Faker.Random.AlphaNumeric(20),
                        ConfiguracaoId = i,
                        Configuracao = new Configuracao
                        {
                            Id = i,
                            Idioma = Idioma.PORTUGUES,
                            Coleta = true,
                            Fala = true,
                            Som = true
                        }
                    },
                });
            }

            var id = 6;

            _progressoMapaRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int i) => data.Find(c => c.Id == i));

            //Act
            var result = _service.Remover(id);

            //Assert
            Assert.False(result.Success);
            Assert.Equal(OperationResultType.NotFound, result.Type);
        }

        [Fact(DisplayName = "Verificar se existe por Id existente")]
        [Trait("Categoria", "Verificar existência de progressoMapa")]
        public void QuandoVerificarSeExisteProgressoMapaPorIdEAchar_DeveRetornar_True()
        {
            //Assert
            var data = new List<ProgressoMapa>();

            for (int i = 1; i <= 5; i++)
            {
                data.Add(new ProgressoMapa()
                {
                    Id = i,
                    Colecionavel = Faker.Random.Number(0, 20),
                    ColecionavelEspecial = Faker.Random.Number(0, 3),
                    DataRegistro = DateTime.Now,
                    Mapa = Mapa.FOREST,
                    Tempo = Faker.Random.Number(30, 5000),
                    UsuarioId = i,
                    Usuario = new Usuario
                    {
                        Id = i,
                        Apelido = Faker.Random.String2(20),
                        Email = Faker.Person.Email,
                        Senha = Faker.Random.AlphaNumeric(20),
                        ConfiguracaoId = i,
                        Configuracao = new Configuracao
                        {
                            Id = i,
                            Idioma = Idioma.PORTUGUES,
                            Coleta = true,
                            Fala = true,
                            Som = true
                        }
                    },
                });
            }

            var id = 2;

            _progressoMapaRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<ProgressoMapa, bool>>>())).Returns(data.Exists(c => c.Id == id));

            //Act
            var result = _service.Existe(id);

            //Assert
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Verificar se existe por Id inexistente")]
        [Trait("Categoria", "Verificar existência de progressoMapa")]
        public void QuandoVerificarSeExisteProgressoMapaPorIdENãoAchar_DeveRetornar_False()
        {
            //Assert
            var data = new List<ProgressoMapa>();

            for (int i = 1; i <= 5; i++)
            {
                data.Add(new ProgressoMapa()
                {
                    Id = i,
                    Colecionavel = Faker.Random.Number(0, 20),
                    ColecionavelEspecial = Faker.Random.Number(0, 3),
                    DataRegistro = DateTime.Now,
                    Mapa = Mapa.FOREST,
                    Tempo = Faker.Random.Number(30, 5000),
                    UsuarioId = i,
                    Usuario = new Usuario
                    {
                        Id = i,
                        Apelido = Faker.Random.String2(20),
                        Email = Faker.Person.Email,
                        Senha = Faker.Random.AlphaNumeric(20),
                        ConfiguracaoId = i,
                        Configuracao = new Configuracao
                        {
                            Id = i,
                            Idioma = Idioma.PORTUGUES,
                            Coleta = true,
                            Fala = true,
                            Som = true
                        }
                    },
                });
            }

            var id = 6;

            _progressoMapaRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<ProgressoMapa, bool>>>())).Returns(data.Exists(c => c.Id == id));

            //Act
            var result = _service.Existe(id);

            //Assert
            Assert.False(result.Success);
        }
    }
}