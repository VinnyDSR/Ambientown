using AmbienTown.Dto.Configuracao;
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
    public class ConfiguracaoService : IConfiguracaoService
    {
        private readonly IConfiguracaoRepository _configuracaoRepository;
        private readonly IMapper _mapper;

        public ConfiguracaoService(IConfiguracaoRepository configuracaoRepository, IMapper mapper)
        {
            _configuracaoRepository = configuracaoRepository ?? throw new ArgumentNullException(nameof(configuracaoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Result Atualizar(AtualizarConfiguracaoDto dto)
        {
            var existsResult = Existe(dto.Id);

            if (existsResult.Failure) return existsResult;

            var configuracao = _mapper.Map<Configuracao>(dto);

            var validationResult = Validar(configuracao);

            if (validationResult.Failure) return validationResult;

            _configuracaoRepository.Update(configuracao);

            return OperationResult.OK();
        }

        public Result<int> Cadastrar(NovaConfiguracaoDto dto)
        {
            var configuracao = _mapper.Map<Configuracao>(dto);

            var validar = Validar(configuracao);

            if (validar.Failure) return new Result<int>(validar);

            var id = _configuracaoRepository.Create(configuracao);

            return OperationResult.Created<int>(id);
        }

        public Result Existe(int id)
        {
            var result = _configuracaoRepository.Exists(x => x.Id == id);

            if (!result) return OperationResult.NotFound("Configuração não encontrada");

            return OperationResult.OK();
        }

        public IEnumerable<ObterConfiguracaoDto> Listar()
        {
            var configuracoes = _configuracaoRepository.GetAll();

            return _mapper.Map<IEnumerable<ObterConfiguracaoDto>>(configuracoes);
        }

        public Result<ObterConfiguracaoDto> ObterPorId(int id)
        {
            var configuracao = _configuracaoRepository.GetById(id);

            if (configuracao == null)
                return OperationResult.NotFound<ObterConfiguracaoDto>();

            var dto = _mapper.Map<ObterConfiguracaoDto>(configuracao);

            return OperationResult.OK(dto);
        }

        public Result Remover(int id)
        {
            var configuracao = _configuracaoRepository.GetById(id);

            if (configuracao == null)
                return OperationResult.NotFound();

            _configuracaoRepository.Remove(configuracao);

            return OperationResult.NoContent();
        }

        private Result Validar(Configuracao configuracao)
        {
            if (configuracao.Idioma != Idioma.PORTUGUES && configuracao.Idioma != Idioma.INGLES)
                return OperationResult.ValidationError("A configuração não possui um idioma");

            return OperationResult.OK();
        }
    }
}