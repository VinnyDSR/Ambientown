using AmbienTown.Dto.Personagem;
using AmbienTown.Enums;
using AmbienTown.Models;
using AmbienTown.Repositories.Interfaces;
using AmbienTown.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using TimeSheet.Api.Utils.Result;

namespace AmbienTown.Services
{
    public class PersonagemService : IPersonagemService
    {
        private readonly IPersonagemRepository _personagemRepository;
        private readonly IMapper _mapper;

        public PersonagemService(IPersonagemRepository personagemRepository, IMapper mapper)
        {
            _personagemRepository = personagemRepository ?? throw new ArgumentNullException(nameof(personagemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Result Atualizar(AtualizarPersonagemDto dto)
        {
            var existsResult = Existe(dto.Id);

            if (existsResult.Failure) return existsResult;

            var personagem = _mapper.Map<Personagem>(dto);

            var validationResult = Validar(personagem);

            if (validationResult.Failure) return validationResult;

            _personagemRepository.Update(personagem);

            return OperationResult.OK();
        }

        public Result<int> Cadastrar(NovoPersonagemDto dto)
        {
            var personagem = _mapper.Map<Personagem>(dto);

            var validar = Validar(personagem);

            if (validar.Failure) return new Result<int>(validar);

            var id = _personagemRepository.Create(personagem);

            return OperationResult.Created<int>(id);
        }

        public Result Existe(int id)
        {
            var result = _personagemRepository.Exists(x => x.Id == id);

            if (!result) return OperationResult.NotFound("Personagem não encontrado");

            return OperationResult.OK();
        }

        public IEnumerable<ObterPersonagemDto> Listar()
        {
            var personagens = _personagemRepository.GetAll();

            return _mapper.Map<IEnumerable<ObterPersonagemDto>>(personagens);
        }

        public Result<ObterPersonagemDto> ObterPorId(int id)
        {
            var personagem = _personagemRepository.GetById(id);

            if (personagem == null)
                return OperationResult.NotFound<ObterPersonagemDto>();

            var dto = _mapper.Map<ObterPersonagemDto>(personagem);

            return OperationResult.OK(dto);
        }

        public Result Remover(int id)
        {
            var personagem = _personagemRepository.GetById(id);

            if (personagem == null)
                return OperationResult.NotFound();

            _personagemRepository.Remove(personagem);

            return OperationResult.NoContent();
        }

        private Result Validar(Personagem personagem)
        {
            if (personagem.Genero != Genero.MASCULINO && personagem.Genero != Genero.FEMININO)
                return OperationResult.ValidationError("Gênero é obrigatório");

            if (personagem.Pele != Pele.BRANCO && personagem.Pele != Pele.NEGRO && personagem.Pele != Pele.PARDO)
                return OperationResult.ValidationError("Pele é obrigatória");

            if (personagem.RoupaId == 0)
                return OperationResult.ValidationError("Roupa é obrigatória");

            return OperationResult.OK();
        }
    }
}