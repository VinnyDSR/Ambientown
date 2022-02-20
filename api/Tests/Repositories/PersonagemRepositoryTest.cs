using AmbienTown.Enums;
using AmbienTown.Models;
using AmbienTown.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Tests.Repositories
{
    public class PersonagemRepositoryTest : RepositoryTest
    {
        private readonly PersonagemRepository _repository;
        private readonly Mock<PersonagemRepository> _repositoryMock;

        public PersonagemRepositoryTest()
        {
            _repository = new PersonagemRepository(this.Entities);
            _repositoryMock = new Mock<PersonagemRepository>(MockBehavior.Strict, new object[] { this.Entities });
        }

        [Fact]
        public void CreatePersonagem_Test()
        {
            //Arrange
            _repositoryMock.Setup(s => s.Create(It.IsAny<Personagem>())).Returns(1);

            //Act
            var newModel = new Personagem
            {
                Id = 0,
                Genero = Genero.MASCULINO,
                Pele = Pele.PARDO,
                RoupaId = (Clothes)1                
            };

            var newModelMockId = _repositoryMock.Object.Create(newModel);
            newModel.Id = _repository.Create(newModel);

            //Assert
            Assert.Equal(newModel.Id, newModelMockId);
        }

        [Fact]
        public void UpdatePersonagem_Test()
        {
            //Arrange
            var newModel = new Personagem
            {
                Id = 0,
                Genero = Genero.MASCULINO,
                Pele = Pele.PARDO,
                RoupaId = (Clothes)1
            };

            newModel.Id = _repository.Create(newModel);

            //Act
            var updateModel = new Personagem
            {
                Id = newModel.Id,
                Genero = Genero.FEMININO,
                Pele = Pele.NEGRO,
                RoupaId = (Clothes)1
            };

            _repository.Update(updateModel);

            var getOneModel = _repository.GetById(updateModel.Id);

            //Assert
            Assert.NotEqual(getOneModel.Genero, newModel.Genero);
        }

        [Fact]
        public void RemovePersonagem_Test()
        {
            //Arrange
            _repositoryMock.Setup(s => s.Remove(It.IsAny<Personagem>())).Verifiable();

            var model = new Personagem
            {
                Id = 0,
                Genero = Genero.MASCULINO,
                Pele = Pele.PARDO,
                RoupaId = (Clothes)1
            };

            model.Id = _repository.Create(model);

            //Act
            _repository.Remove(model);

            var getOneModel = _repository.GetById(model.Id);

            var getExcluded = this.Context.Set<Personagem>().Find(model.Id);

            //Assert
            Assert.Null(getOneModel);
        }

        [Fact]
        public void ExistsPersonagem_Test()
        {
            //Arrange
            _repositoryMock.Setup(s => s.Exists(It.IsAny<Expression<Func<Personagem, bool>>>())).Returns(true);

            var newModel = new Personagem
            {
                Id = 0,
                Genero = Genero.MASCULINO,
                Pele = Pele.PARDO,
                RoupaId = (Clothes)1
            };

            _repository.Create(newModel);

            //Act
            var resultMock = _repositoryMock.Object.Exists(x => x.Pele == Pele.PARDO);
            var result = _repository.Exists(x => x.Pele == Pele.PARDO);

            //Assert
            Assert.Equal(result, resultMock);
        }

        [Fact]
        public void GetAll_Test()
        {
            //Arrange
            for (int i = 1; i < 5; i++)
            {
                this.Context.Add(new Personagem { Genero = Genero.MASCULINO, Pele = Pele.PARDO, RoupaId = (Clothes)1 });
                this.Context.SaveChanges();
            }

            _repositoryMock.Setup(s => s.GetAll()).Returns(Context.Personagens.AsEnumerable());

            //Act
            var items = _repository.GetAll();
            var itemsMock = _repositoryMock.Object.GetAll();

            //Assert
            Assert.Equal(items.Count(), itemsMock.Count());
        }
    }
}