using AmbienTown.Enums;
using AmbienTown.Models;
using AmbienTown.Repositories;
using Bogus;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Tests.Repositories
{
    public class ProgressoMapaRepositoryTest : RepositoryTest
    {
        private readonly ProgressoMapaRepository _repository;
        private readonly Mock<ProgressoMapaRepository> _repositoryMock;
        private readonly UsuarioRepository _usuarioRepository;

        public ProgressoMapaRepositoryTest()
        {
            _repository = new ProgressoMapaRepository(this.Entities);
            _repositoryMock = new Mock<ProgressoMapaRepository>(MockBehavior.Strict, new object[] { this.Entities });
            _usuarioRepository = new UsuarioRepository(this.Entities);
        }

        [Fact]
        public void CreateProgressoMapa_Test()
        {
            //Arrange
            _repositoryMock.Setup(s => s.Create(It.IsAny<ProgressoMapa>())).Returns(1);

            //Act
            var newModel = new ProgressoMapa
            {
                Id = 0,
                Colecionavel = 15,
                ColecionavelEspecial = 1,
                DataRegistro = DateTime.Now,
                Mapa = Mapa.FOREST,
                Tempo = 500,
                UsuarioId = 1                
            };

            var newModelMockId = _repositoryMock.Object.Create(newModel);
            newModel.Id = _repository.Create(newModel);

            //Assert
            Assert.Equal(newModel.Id, newModelMockId);
        }

        [Fact]
        public void RemoveProgressoMapa_Test()
        {
            //Arrange
            _repositoryMock.Setup(s => s.Remove(It.IsAny<ProgressoMapa>())).Verifiable();

            var model = new ProgressoMapa
            {
                Id = 0,
                Colecionavel = 15,
                ColecionavelEspecial = 1,
                DataRegistro = DateTime.Now,
                Mapa = Mapa.FOREST,
                Tempo = 500,
                UsuarioId = 1
            };

            model.Id = _repository.Create(model);

            //Act
            _repository.Remove(model);

            var getOneModel = _repository.GetById(model.Id);

            var getExcluded = this.Context.Set<ProgressoMapa>().Find(model.Id);

            //Assert
            Assert.Null(getOneModel);
        }

        [Fact]
        public void ExistsProgressoMapa_Test()
        {
            //Arrange
            _repositoryMock.Setup(s => s.Exists(It.IsAny<Expression<Func<ProgressoMapa, bool>>>())).Returns(true);

            var newModel = new ProgressoMapa
            {
                Id = 0,
                Colecionavel = 15,
                ColecionavelEspecial = 1,
                DataRegistro = DateTime.Now,
                Mapa = Mapa.FOREST,
                Tempo = 500,
                UsuarioId = 1
            };

            _repository.Create(newModel);

            //Act
            var resultMock = _repositoryMock.Object.Exists(x => x.UsuarioId == 1);
            var result = _repository.Exists(x => x.UsuarioId == 1);

            //Assert
            Assert.Equal(result, resultMock);
        }

        [Fact]
        public void GetAll_Test()
        {
            //Arrange
            for (int i = 1; i < 5; i++)
            {
                this.Context.Add(new ProgressoMapa { Colecionavel = 15, ColecionavelEspecial = 1, DataRegistro = DateTime.Now, Mapa = Mapa.FOREST, Tempo = 500, UsuarioId = 1 });
                this.Context.SaveChanges();
            }

            _repositoryMock.Setup(s => s.GetAll()).Returns(Context.ProgressosMapa.AsEnumerable());

            //Act
            var items = _repository.GetAll();
            var itemsMock = _repositoryMock.Object.GetAll();

            //Assert
            Assert.Equal(items.Count(), itemsMock.Count());
        }

        private void CadastrarUsuarios(int quantidade)
        {
            for (int i = 0; i < quantidade; i++)
            {
                var usuario = new Usuario
                {
                    Id = 0,
                    Apelido = "apelidoTeste123",
                    Email = new Faker().Person.Email,
                    Senha = "@senha123",
                    ConfiguracaoId = 1,
                    PersonagemId = 1
                };
                this.Context.Add(usuario);
                this.Context.SaveChanges();
            }
        }

        private List<int> UsuariosCadastrados()
        {
            var usuarios = _usuarioRepository.GetAll();
            var ids = usuarios.Select(x => x.Id).ToList();

            return ids;
        }
    }
}