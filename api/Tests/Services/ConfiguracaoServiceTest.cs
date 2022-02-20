using AmbienTown.Dto.Configuracao;
using AmbienTown.Enums;
using AmbienTown.Models;
using AmbienTown.Repositories.Interfaces;
using AmbienTown.Services;
using AutoMapper;
using Moq;
using System;
using System.Linq.Expressions;
using Tests.Stubs.Configuracao;
using TimeSheet.Api.Utils.Result;
using Xunit;

namespace Tests.Services
{
    public class ConfiguracaoServiceTest
    {
        private readonly ConfiguracaoService _service;
        private readonly Mock<IConfiguracaoRepository> _configuracaoRepositoryMock = new Mock<IConfiguracaoRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public ConfiguracaoServiceTest()
        {
            _service = new ConfiguracaoService(_configuracaoRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact(DisplayName = "Cadastro com sucesso")]
        [Trait("Categoria", "Nova Configuração")]
        public void QuandoCadastrar_DeveValidarEChamarMetodoDoRepositorio()
        {
            //Arrange
            var configuracao = NovaConfiguracaoDtoStub.Completo();

            _mapperMock.Setup(x => x.Map<Configuracao>(It.IsAny<NovaConfiguracaoDto>()))
                .Returns(new Configuracao { Idioma = Idioma.PORTUGUES, Coleta = true, Fala = true, Som = true });

            //Act
            var result = _service.Cadastrar(configuracao);

            //Assert
            _configuracaoRepositoryMock.Verify(x => x.Create(It.IsAny<Configuracao>()), Times.Once());
            Assert.True(result.Success);
            Assert.Equal(OperationResultType.Created, result.Type);
        }

        [Fact(DisplayName = "Cadastro sem idioma")]
        [Trait("Categoria", "Nova Configuração")]
        public void QuandoCadastrarSemIdioma_DeveRetornar_ConfiguracaoNaoPossuiIdioma()
        {
            //Arrange
            var configuracao = NovaConfiguracaoDtoStub.SemIdioma();

            _mapperMock.Setup(x => x.Map<Configuracao>(It.IsAny<NovaConfiguracaoDto>()))
                .Returns(new Configuracao { Fala = true, Coleta = true, Som = true });

            //Act
            var result = _service.Cadastrar(configuracao);

            //Assert
            _configuracaoRepositoryMock.Verify(x => x.Create(It.IsAny<Configuracao>()), Times.Never());
            Assert.False(result.Success);
            Assert.Equal(OperationResultType.ValidationError, result.Type);
        }

        [Fact(DisplayName = "Atualizar com Sucesso")]
        [Trait("Categoria", "Atualizar Configuração")]
        public void QuandoAtualizar_DeveValidarEChamarMetodoDoRepositorio()
        {
            //Arrange
            var id = 1;
            var configuracao = AtualizarConfiguracaoDtoStub.Completo(id);

            _mapperMock.Setup(x => x.Map<Configuracao>(It.IsAny<AtualizarConfiguracaoDto>()))
                .Returns(new Configuracao { Id = id, Idioma = configuracao.Idioma, Coleta = configuracao.Coleta, Fala = configuracao.Fala, Som = configuracao.Som });

            _configuracaoRepositoryMock.SetupSequence(s => s.Exists(It.IsAny<Expression<Func<Configuracao, bool>>>()))
                .Returns(true)
                .Returns(false);

            //Act
            var result = _service.Atualizar(configuracao);

            //Assert
            _configuracaoRepositoryMock.Verify(x => x.Update(It.IsAny<Configuracao>()), Times.Once());
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Atualizar configuração nao encontrada")]
        [Trait("Categoria", "Atualizar Configuração")]
        public void QuandoAtualizar_DeveValidarOIdEParar_ConfiguracaoNaoEncontrada()
        {
            //Arrange
            var id = 1;
            var configuracao = AtualizarConfiguracaoDtoStub.Completo(id);

            _mapperMock.Setup(x => x.Map<Configuracao>(It.IsAny<AtualizarConfiguracaoDto>()))
                .Returns(new Configuracao { Id = id, Idioma = configuracao.Idioma, Coleta = configuracao.Coleta, Fala = configuracao.Fala, Som = configuracao.Som });

            _configuracaoRepositoryMock.Setup(s => s.Exists(It.IsAny<Expression<Func<Configuracao, bool>>>()))
                .Returns(false);

            //Act
            var result = _service.Atualizar(configuracao);

            //Assert
            _configuracaoRepositoryMock.Verify(x => x.Update(It.IsAny<Configuracao>()), Times.Never());
            Assert.False(result.Success);
            Assert.Equal(OperationResultType.NotFound, result.Type);
        }

        [Fact(DisplayName = "Atualizar configuração sem idioma")]
        [Trait("Categoria", "Atualizar Configuração")]
        public void QuandoAtualizarSemIdioma_DeveRetornar_ConfiguracaoNaoPossuiIdioma()
        {
            //Arrange
            var id = 1;
            var configuracao = AtualizarConfiguracaoDtoStub.SemIdioma(id);

            _mapperMock.Setup(x => x.Map<Configuracao>(It.IsAny<AtualizarConfiguracaoDto>()))
                .Returns(new Configuracao { Id = id, Idioma = configuracao.Idioma, Coleta = configuracao.Coleta, Som = configuracao.Som, Fala = configuracao.Fala });

            _configuracaoRepositoryMock.SetupSequence(s => s.Exists(It.IsAny<Expression<Func<Configuracao, bool>>>()))
                .Returns(true)
                .Returns(false);

            //Act
            var result = _service.Atualizar(configuracao);

            //Assert
            _configuracaoRepositoryMock.Verify(x => x.Update(It.IsAny<Configuracao>()), Times.Never());
            Assert.False(result.Success);
            Assert.Equal(OperationResultType.ValidationError, result.Type);
        }

        [Fact(DisplayName = "Obtem configuração por Id existente")]
        [Trait("Categoria", "Obtem por Id")]
        public void QuandoObterPorId_DeveChamarMetodoDoRepositorioConfiguracaoExistente()
        {
            //Arrange
            var id = 1;
            var configuracao = new Configuracao
            {
                Id = id,
                Idioma = Idioma.PORTUGUES,
                Fala = true,
                Som = true,
                Coleta = true
            };

            _mapperMock.Setup(x => x.Map<ObterConfiguracaoDto>(It.IsAny<Configuracao>()))
                .Returns(new ObterConfiguracaoDto { Id = id, Idioma = Idioma.PORTUGUES, Fala = true, Som = true, Coleta = true });

            _configuracaoRepositoryMock.Setup(x => x.GetById(id)).Returns(configuracao);

            //Act
            var result = _service.ObterPorId(id);

            //Assert
            _configuracaoRepositoryMock.Verify(x => x.GetById(1), Times.Once());
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Obtem configuração por Id inexistente")]
        [Trait("Categoria", "Obtem por Id")]
        public void QuandoObterPorId_DeveChamarMetodoDoRepositorioConfiguracaoInexistente()
        {
            //Arrange
            var id = 1;

            //Act
            var result = _service.ObterPorId(id);

            //Assert
            _configuracaoRepositoryMock.Verify(x => x.GetById(id), Times.Once());
            Assert.False(result.Success);
            Assert.Equal(OperationResultType.NotFound, result.Type);
            Assert.Null(result.Value);
        }

        [Fact(DisplayName = "Excluir por Id existente")]
        [Trait("Categoria", "Excluir Configuração")]
        public void QuandoRemover_DeveObterPorIdEChamarMetodoDoRepositorio()
        {
            //Arrange
            var id = 1;
            var configuracao = new Configuracao
            {
                Id = id,
                Idioma = Idioma.PORTUGUES,
                Fala = true,
                Som = true,
                Coleta = true
            };

            _configuracaoRepositoryMock.Setup(x => x.GetById(id))
                .Returns(configuracao);

            _mapperMock.Setup(x => x.Map<Configuracao>(It.IsAny<ObterConfiguracaoDto>()))
                 .Returns(new Configuracao { Id = id, Idioma = Idioma.PORTUGUES, Fala = true, Som = true, Coleta = true });

            //Act
            var result = _service.Remover(id);

            //Assert
            _configuracaoRepositoryMock.Verify(x => x.Remove(It.IsAny<Configuracao>()), Times.Once());
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Excluir por Id inexistente")]
        [Trait("Categoria", "Excluir Configuração")]
        public void QuandoRemover_DeveObterPorIdE_ValidarQueConfiguracaoNaoExiste()
        {
            //Arrange
            var id = 1;

            //Act
            var result = _service.Remover(id);

            //Assert
            _configuracaoRepositoryMock.Verify(x => x.Remove(It.IsAny<Configuracao>()), Times.Never());
            Assert.False(result.Success);
        }
    }
}