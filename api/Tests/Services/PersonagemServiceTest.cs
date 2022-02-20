using AmbienTown.Dto.Personagem;
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
using Tests.Stubs.Personagem;
using TimeSheet.Api.Utils.Result;
using Xunit;

namespace Tests.Services
{
    public class PersonagemServiceTest
    {
        private readonly PersonagemService _service;
        private readonly Mock<IPersonagemRepository> _personagemRepositoryMock = new Mock<IPersonagemRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public PersonagemServiceTest()
        {
            _service = new PersonagemService(_personagemRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact(DisplayName = "Cadastro com sucesso")]
        [Trait("Categoria", "Novo Personagem")]
        public void QuandoCadastrar_DeveValidarEChamarMetodoDoRepositorio()
        {
            //Arrange
            var personagem = NovoPersonagemDtoStub.Completo(1);

            _mapperMock.Setup(x => x.Map<Personagem>(It.IsAny<NovoPersonagemDto>()))
                .Returns(new Personagem { Genero = Genero.MASCULINO, Pele = Pele.PARDO, RoupaId = (Clothes)1 });

            var dto = NovoPersonagemDtoStub.Completo(1);

            //Act            
            //var resultMock = _serviceMock.Object.Cadastrar(dto);
            var result = _service.Cadastrar(personagem);

            //Assert
            _personagemRepositoryMock.Verify(x => x.Create(It.IsAny<Personagem>()), Times.Once());
            Assert.True(result.Success);
            Assert.Equal(OperationResultType.Created, result.Type);
        }

        [Fact(DisplayName = "Cadastro sem informar o gênero")]
        [Trait("Categoria", "Novo Personagem")]
        public void QuandoCadastrarSemGenero_DeveRetornar_GeneroEhObrigatorio()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<Personagem>(It.IsAny<NovoPersonagemDto>())).Returns(new Personagem { Pele = Pele.PARDO, RoupaId = (Clothes)1 });

            var dto = NovoPersonagemDtoStub.SemGenero(1);

            //Act
            var result = _service.Cadastrar(dto);

            Assert.False(result.Success);
            Assert.Equal(OperationResultType.ValidationError, result.Type);
        }

        [Fact(DisplayName = "Cadastro sem informar a pele")]
        [Trait("Categoria", "Novo Personagem")]
        public void QuandoCadastrarSemPele_DeveRetornar_PeleEhObrigatoria()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<Personagem>(It.IsAny<NovoPersonagemDto>())).Returns(new Personagem { Genero = Genero.MASCULINO, RoupaId = (Clothes)1 });

            var dto = NovoPersonagemDtoStub.SemPele(1);

            //Act
            var result = _service.Cadastrar(dto);

            Assert.False(result.Success);
            Assert.Equal(OperationResultType.ValidationError, result.Type);
        }

        [Fact(DisplayName = "Cadastro sem informar a roupa")]
        [Trait("Categoria", "Novo Personagem")]
        public void QuandoCadastrarSemRoupa_DeveRetornar_RoupaEhObrigatoria()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<Personagem>(It.IsAny<NovoPersonagemDto>()))
                .Returns(new Personagem { Genero = Genero.MASCULINO, Pele = Pele.PARDO });

            var dto = NovoPersonagemDtoStub.SemRoupa();

            //Act
            var result = _service.Cadastrar(dto);

            Assert.False(result.Success);
            Assert.Equal(OperationResultType.ValidationError, result.Type);
        }

        [Fact(DisplayName = "Atualizar com sucesso")]
        [Trait("Categoria", "Atualizar Personagem")]
        public void QuandoAtualizar_DeveRetornar_Ok()
        {
            //Arrange
            _personagemRepositoryMock.SetupSequence(x => x.Exists(It.IsAny<Expression<Func<Personagem, bool>>>()))
                .Returns(true)
                .Returns(false);
            _personagemRepositoryMock.Setup(x => x.Update(It.IsAny<Personagem>()));
            _mapperMock.Setup(x => x.Map<Personagem>(It.IsAny<AtualizarPersonagemDto>()))
                .Returns(new Personagem { Id = 1, Genero = Genero.FEMININO, Pele = Pele.NEGRO, RoupaId = (Clothes)1 });

            var dto = AtualizarPersonagemDtoStub.Completo(1, 1);

            //Act
            var result = _service.Atualizar(dto);

            //Assert
            Assert.True(result.Success);
            Assert.Equal(OperationResultType.Ok, result.Type);
        }

        [Fact(DisplayName = "Atualizar com Id inexistente")]
        [Trait("Categoria", "Atualizar Personagem")]
        public void QuandoAtualizarComIdInexistente_DeveRetornar_OperationResultNotFound()
        {
            //Arrange
            var id = 1;
            var personagem = AtualizarPersonagemDtoStub.Completo(id, 1);

            _mapperMock.Setup(x => x.Map<Personagem>(It.IsAny<AtualizarPersonagemDto>()))
                .Returns(new Personagem { Id = id, Genero = personagem.Genero, Pele = personagem.Pele, RoupaId = (Clothes)personagem.RoupaId });

            _personagemRepositoryMock.Setup(s => s.Exists(It.IsAny<Expression<Func<Personagem, bool>>>()))
                .Returns(false);

            //Act
            var result = _service.Atualizar(personagem);

            //Assert
            _personagemRepositoryMock.Verify(x => x.Update(It.IsAny<Personagem>()), Times.Never());
            Assert.False(result.Success);
            Assert.Equal(OperationResultType.NotFound, result.Type);
        }

        [Fact(DisplayName = "Atualizar sem informar gênero")]
        [Trait("Categoria", "Atualizar Personagem")]
        public void QuandoAtualizarSemGenero_DeveRetornar_GeneroEhObrigatorio()
        {
            //Arrange
            var id = 1;
            var dto = AtualizarPersonagemDtoStub.SemGenero(id, 1);

            _mapperMock.Setup(x => x.Map<Personagem>(It.IsAny<AtualizarPersonagemDto>()))
                .Returns(new Personagem { Id = id, Genero = dto.Genero, Pele = dto.Pele, RoupaId = (Clothes)dto.RoupaId });

            _personagemRepositoryMock.SetupSequence(s => s.Exists(It.IsAny<Expression<Func<Personagem, bool>>>()))
                .Returns(true)
                .Returns(false);

            //Act
            var result = _service.Atualizar(dto);

            //Assert
            _personagemRepositoryMock.Verify(x => x.Update(It.IsAny<Personagem>()), Times.Never());
            Assert.False(result.Success);
            Assert.Equal(OperationResultType.ValidationError, result.Type);
        }

        [Fact(DisplayName = "Atualizar sem informar pele")]
        [Trait("Categoria", "Atualizar Personagem")]
        public void QuandoAtualizarSemPele_DeveRetornar_PeleEhObrigatoria()
        {
            //Arrange
            var id = 1;
            var dto = AtualizarPersonagemDtoStub.SemPele(id, 1);

            _mapperMock.Setup(x => x.Map<Personagem>(It.IsAny<AtualizarPersonagemDto>()))
                .Returns(new Personagem { Id = id, Genero = dto.Genero, RoupaId = (Clothes)dto.RoupaId });

            _personagemRepositoryMock.SetupSequence(s => s.Exists(It.IsAny<Expression<Func<Personagem, bool>>>()))
                .Returns(true)
                .Returns(false);

            //Act
            var result = _service.Atualizar(dto);

            //Assert
            _personagemRepositoryMock.Verify(x => x.Update(It.IsAny<Personagem>()), Times.Never());
            Assert.False(result.Success);
            Assert.Equal(OperationResultType.ValidationError, result.Type);
        }

        [Fact(DisplayName = "Atualizar sem informar roupa")]
        [Trait("Categoria", "Atualizar Personagem")]
        public void QuandoAtualizarSemRoupa_DeveRetornar_RoupaEhObrigatoria()
        {
            //Arrange
            var id = 1;
            var dto = AtualizarPersonagemDtoStub.SemRoupa(id);

            _mapperMock.Setup(x => x.Map<Personagem>(It.IsAny<AtualizarPersonagemDto>()))
                .Returns(new Personagem { Id = id, Genero = dto.Genero, Pele = dto.Pele });

            _personagemRepositoryMock.SetupSequence(s => s.Exists(It.IsAny<Expression<Func<Personagem, bool>>>()))
                .Returns(true)
                .Returns(false);

            //Act
            var result = _service.Atualizar(dto);

            //Assert
            _personagemRepositoryMock.Verify(x => x.Update(It.IsAny<Personagem>()), Times.Never());
            Assert.False(result.Success);
            Assert.Equal(OperationResultType.ValidationError, result.Type);
        }

        [Fact(DisplayName = "Obter por Id existente")]
        [Trait("Categoria", "Obter Personagem")]
        public void QuandoObterPorIdExiste_DeveRetornar_ObjetoPreenchido()
        {
            //Assert
            var data = new List<Personagem>();

            for (int i = 1; i <= 5; i++)
            {
                data.Add(new Personagem()
                {
                    Id = i,
                    Genero = Genero.MASCULINO,
                    Pele = Pele.PARDO,                    
                    RoupaId = (Clothes)1
                });
            }

            var id = 2;

            _personagemRepositoryMock.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((int i) => data.Find(x => x.Id == i));

            //Act
            var result = _service.ObterPorId(id);

            //Assert
            _personagemRepositoryMock.Verify(v => v.GetById(It.IsAny<int>()), Times.Once);

            Assert.True(result.Success);
            Assert.Equal(OperationResultType.Ok, result.Type);
        }

        [Fact(DisplayName = "Obter por Id inexistente")]
        [Trait("Categoria", "Obter Personagem")]
        public void QuandoObterPorIdInexistente_DeveRetornar_OperationResultNotFound()
        {
            //Assert
            var data = new List<Personagem>();

            for (int i = 1; i <= 5; i++)
            {
                data.Add(new Personagem()
                {
                    Id = i,
                    Genero = Genero.MASCULINO,
                    Pele = Pele.PARDO,
                    RoupaId = (Clothes)i
                });
            }

            var id = 6;

            _personagemRepositoryMock.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((int i) => data.Find(x => x.Id == i));

            //Act
            var result = _service.ObterPorId(id);

            //Assert
            Assert.False(result.Success);
            Assert.Null(result.Value);
            Assert.Equal(OperationResultType.NotFound, result.Type);
        }

        [Fact(DisplayName = "Remover por Id existente")]
        [Trait("Categoria", "Remover Personagem")]
        public void QuandoRemover_DeveRetornar_OperationResultNoContent()
        {
            //Assert
            var data = new List<Personagem>();

            for (int i = 1; i <= 5; i++)
            {
                data.Add(new Personagem()
                {
                    Id = i,
                    Genero = Genero.MASCULINO,
                    Pele = Pele.PARDO,
                    RoupaId = (Clothes)i
                });
            }

            var id = 3;

            _personagemRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int i) => data.Find(c => c.Id == i));
            _personagemRepositoryMock.Setup(x => x.Remove(It.IsAny<Personagem>()));

            //Act
            var result = _service.Remover(id);

            //Assert
            _personagemRepositoryMock.Verify(v => v.Remove(It.IsAny<Personagem>()), Times.Once);

            Assert.True(result.Success);
            Assert.Equal(OperationResultType.NoContent, result.Type);
        }

        [Fact(DisplayName = "Remover por Id inexistente")]
        [Trait("Categoria", "Remover Personagem")]
        public void QuandoRemoverIdInexistente_DeveRetornar_OperationResultNotFound()
        {
            //Assert
            var data = new List<Personagem>();

            for (int i = 1; i <= 5; i++)
            {
                data.Add(new Personagem()
                {
                    Id = i,
                    Genero = Genero.MASCULINO,
                    Pele = Pele.PARDO,
                    RoupaId = (Clothes)i
                });
            }

            var id = 6;

            _personagemRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int i) => data.Find(c => c.Id == i));

            //Act
            var result = _service.Remover(id);

            //Assert
            Assert.False(result.Success);
            Assert.Equal(OperationResultType.NotFound, result.Type);
        }

        [Fact(DisplayName = "Veficiar se existe por Id existente")]
        [Trait("Categoria", "Verificar existência de personagem")]
        public void QuandoVerificarSeExistePersonagemPorIdEAchar_DeveRetornar_True()
        {
            //Assert
            var data = new List<Personagem>();

            for (int i = 1; i <= 5; i++)
            {
                data.Add(new Personagem()
                {
                    Id = i,
                    Genero = Genero.MASCULINO,
                    Pele = Pele.PARDO,
                    RoupaId = (Clothes)i
                });
            }

            var id = 2;

            _personagemRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<Personagem, bool>>>())).Returns(data.Exists(c => c.Id == id));

            //Act
            var result = _service.Existe(id);

            //Assert
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Veficiar se existe por Id inexistente")]
        [Trait("Categoria", "Verificar existência de personagem")]
        public void QuandoVerificarSeExistePersonagemPorIdENãoAchar_DeveRetornar_False()
        {
            //Assert
            var data = new List<Personagem>();

            for (int i = 1; i <= 5; i++)
            {
                data.Add(new Personagem()
                {
                    Id = i,
                    Genero = Genero.MASCULINO,
                    Pele = Pele.PARDO,
                    RoupaId = (Clothes)i
                });
            }

            var id = 6;

            _personagemRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<Personagem, bool>>>())).Returns(data.Exists(c => c.Id == id));

            //Act
            var result = _service.Existe(id);

            //Assert
            Assert.False(result.Success);
        }
    }
}