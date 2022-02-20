using AmbienTown.Enums;
using AmbienTown.Models;
using AmbienTown.Repositories;
using Moq;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Tests.Repositories
{
    public class ConfiguracaoRepositoryTest : RepositoryTest
    {
        private readonly ConfiguracaoRepository _repository;
        private readonly Mock<ConfiguracaoRepository> _repositoryMock;

        public ConfiguracaoRepositoryTest()
        {
            _repository = new ConfiguracaoRepository(this.Entities);
            _repositoryMock = new Mock<ConfiguracaoRepository>(MockBehavior.Strict, new object[] { this.Entities });
        }

        [Fact]
        public void CreateConfiguracao_Test()
        {
            //Arrange
            _repositoryMock.Setup(s => s.Create(It.IsAny<Configuracao>()))
                .Returns(1);

            //Act
            var newModel = new Configuracao
            {
                Id = 0,
                Idioma = Idioma.PORTUGUES,
                Coleta = true,
                Fala = true,
                Som = true
            };

            var newModelMockId = _repositoryMock.Object.Create(newModel);
            newModel.Id = _repository.Create(newModel);

            //Assert
            Assert.Equal(newModel.Id, newModelMockId);
        }

        [Fact]
        public void UpdateConfiguracao_Test()
        {
            //Arrange
            var newModel = new Configuracao
            {
                Id = 0,
                Idioma = Idioma.PORTUGUES,
                Coleta = true,
                Som = true,
                Fala = true
            };

            newModel.Id = _repository.Create(newModel);

            //Act
            var updateModel = new Configuracao
            {
                Id = newModel.Id,
                Idioma = Idioma.INGLES,
                Fala = false,
                Som = false,
                Coleta = false
            };

            _repository.Update(updateModel);

            var getOneModel = _repository.GetById(updateModel.Id);

            //Assert
            Assert.NotEqual(getOneModel.Idioma, newModel.Idioma);
        }

        [Fact]
        public void RemoveConfiguracao_Test()
        {
            //Arrange
            _repositoryMock.Setup(s => s.Remove(It.IsAny<Configuracao>())).Verifiable();

            var model = new Configuracao
            {
                Id = 0,
                Idioma = Idioma.PORTUGUES,
                Coleta = true,
                Som = true,
                Fala = true
            };

            model.Id = _repository.Create(model);

            //Act
            _repository.Remove(model);

            var getOneModel = _repository.GetById(model.Id);

            var getExcluded = this.Context.Set<Configuracao>().Find(model.Id);

            Assert.Null(getOneModel);
        }

        [Fact]
        public void ExistsConfiguracao_Test()
        {
            //Arrange
            _repositoryMock.Setup(s => s.Exists(It.IsAny<Expression<Func<Configuracao, bool>>>())).Returns(true);

            var newModel = new Configuracao
            {
                Id = 0,
                Idioma = Idioma.PORTUGUES,
                Coleta = true,
                Som = true,
                Fala = true
            };

            _repository.Create(newModel);

            //Act
            var resultMock = _repositoryMock.Object.Exists(x => x.Idioma == Idioma.PORTUGUES);
            var result = _repository.Exists(x => x.Idioma == Idioma.PORTUGUES);

            //Assert
            Assert.Equal(result, resultMock);
        }

        [Fact]
        public void GetAllConfiguracoes_Test()
        {
            //Arrange
            for (int i = 1; i < 5; i++)
            {
                this.Context.Add(new Configuracao { Idioma = Idioma.PORTUGUES, Coleta = true, Som = true, Fala = true });
                this.Context.SaveChanges();
            }

            _repositoryMock.Setup(s => s.GetAll()).Returns(Context.Configuracoes.AsEnumerable());

            //Act
            var items = _repository.GetAll();
            var itemsMock = _repositoryMock.Object.GetAll();

            //Assert
            Assert.Equal(items.Count(), itemsMock.Count());
        }
    }
}