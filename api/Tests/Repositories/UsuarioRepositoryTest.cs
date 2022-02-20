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
    public class UsuarioRepositoryTest : RepositoryTest
    {
        private readonly UsuarioRepository _repository;
        private readonly Mock<UsuarioRepository> _repositoryMock;
        private readonly ConfiguracaoRepository _configuracaoRepository;
        private readonly PersonagemRepository _personagemRepository;

        public UsuarioRepositoryTest()
        {
            _repository = new UsuarioRepository(this.Entities);
            _repositoryMock = new Mock<UsuarioRepository>(MockBehavior.Strict, new object[] { this.Entities });
            _configuracaoRepository = new ConfiguracaoRepository(this.Entities);
            _personagemRepository = new PersonagemRepository(this.Entities);
        }

        [Fact]
        public void CreateUsuario_Test()
        {
            //Arrange
            _repositoryMock.Setup(s => s.Create(It.IsAny<Usuario>())).Returns(1);

            //Act
            var newModel = new Usuario
            {
                Id = 0,
                Apelido = "apelidoTeste123",
                Email = new Faker().Person.Email,
                Senha = "@senha123",
                ConfiguracaoId = 1,
                PersonagemId = 1                
            };

            var newModelMockId = _repositoryMock.Object.Create(newModel);
            newModel.Id = _repository.Create(newModel);

            //Assert
            Assert.Equal(newModel.Id, newModelMockId);
        }

        [Fact]
        public void UpdateUsuario_Test()
        {
            //Arrange
            var newModel = new Usuario
            {
                Id = 0,
                Apelido = "apelidoTeste123",
                Email = new Faker().Person.Email,
                Senha = "@senha123",
                ConfiguracaoId = 1,
                PersonagemId = 1
            };

            newModel.Id = _repository.Create(newModel);

            //Act
            var updateModel = new Usuario
            {
                Id = newModel.Id,
                Apelido = "apelidoAtualizado456",
                Email = new Faker().Person.Email,
                Senha = "@senha123",
                ConfiguracaoId = 1,
                PersonagemId = 1
            };

            _repository.Update(updateModel);

            var getOneModel = _repository.GetById(updateModel.Id);

            //Assert
            Assert.NotEqual(getOneModel.Apelido, newModel.Apelido);
        }

        [Fact]
        public void RemoveUsuario_Test()
        {
            //Arrange
            _repositoryMock.Setup(s => s.Remove(It.IsAny<Usuario>())).Verifiable();

            var model = new Usuario
            {
                Id = 0,
                Apelido = "apelidoTeste123",
                Email = new Faker().Person.Email,
                Senha = "@senha123",
                ConfiguracaoId = 1,
                PersonagemId = 1
            };

            model.Id = _repository.Create(model);

            //Act
            _repository.Remove(model);

            var getOneModel = _repository.GetById(model.Id);

            var getExcluded = this.Context.Set<Usuario>().Find(model.Id);

            //Assert
            Assert.Null(getOneModel);
        }

        [Fact]
        public void ExistsUsuario_Test()
        {
            //Arrange
            _repositoryMock.Setup(s => s.Exists(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(true);

            var newModel = new Usuario
            {
                Id = 0,
                Apelido = "apelidoTeste123",
                Email = new Faker().Person.Email,
                Senha = "@senha123",
                ConfiguracaoId = 1,
                PersonagemId = 1
            };

            _repository.Create(newModel);

            //Act
            var resultMock = _repositoryMock.Object.Exists(x => x.Apelido == "apelidoTeste123");
            var result = _repository.Exists(x => x.Apelido == "apelidoTeste123");

            //Assert
            Assert.Equal(result, resultMock);
        }

        [Fact]
        public void GetAll_Test()
        {
            //Arrange
            for (int i = 1; i < 5; i++)
            {
                this.Context.Add(new Usuario { Apelido = "apelidoTeste123", Email = new Faker().Person.Email, Senha = "@senha123", ConfiguracaoId = 1, PersonagemId = 1 });
                this.Context.SaveChanges();
            }

            _repositoryMock.Setup(s => s.GetAll()).Returns(Context.Usuarios.AsEnumerable());

            //Act
            var items = _repository.GetAll();
            var itemsMock = _repositoryMock.Object.GetAll();

            //Assert
            Assert.Equal(items.Count(), itemsMock.Count());
        }

        private void CadastrarConfiguracoes(int quantidade)
        {
            for (int i = 0; i < quantidade; i++)
            {
                var configuracao = new Configuracao
                {
                    Id = 0,
                    Idioma = Idioma.PORTUGUES,
                    Coleta = true,
                    Fala = true,
                    Som = true
                };
                this.Context.Add(configuracao);
                this.Context.SaveChanges();
            }
        }

        private void CadastrarPersonagens(int quantidade)
        {
            for (int i = 0; i < quantidade; i++)
            {
                var personagem = new Personagem
                {
                    Id = 0,
                    Genero = Genero.MASCULINO,
                    Pele = Pele.PARDO,
                    RoupaId = (Clothes)1
                };
                this.Context.Add(personagem);
                this.Context.SaveChanges();
            }
        }

        private List<int> ConfiguracoesCadastradas()
        {
            var configuracoes = _configuracaoRepository.GetAll();
            var ids = configuracoes.Select(x => x.Id).ToList();

            return ids;
        }

        private List<int> PersonagensCadastrados()
        {
            var personagens = _personagemRepository.GetAll();
            var ids = personagens.Select(x => x.Id).ToList();

            return ids;
        }
    }
}