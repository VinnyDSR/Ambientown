using AmbienTown.Dto.Leaderboard;
using AmbienTown.Dto.ProgressoMapa;
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
    public class ProgressoMapaService : IProgressoMapaService
    {
        private readonly IProgressoMapaRepository _progressoMapaRepository;
        private readonly IMapper _mapper;

        public ProgressoMapaService(IProgressoMapaRepository progressoMapaRepository, IMapper mapper)
        {
            _progressoMapaRepository = progressoMapaRepository ?? throw new ArgumentNullException(nameof(progressoMapaRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Result<int> Cadastrar(NovoProgressoMapaDto dto)
        {
            var progressoMapa = _mapper.Map<ProgressoMapa>(dto);

            progressoMapa.Pontuacao = CalcularPontuacao(progressoMapa);

            progressoMapa.DataRegistro = DateTime.Now;

            var validar = Validar(progressoMapa);

            if (validar.Failure) return new Result<int>(validar);

            var id = _progressoMapaRepository.Create(progressoMapa);            

            return OperationResult.Created<int>(id);
        }

        public Result Existe(int id)
        {
            var result = _progressoMapaRepository.Exists(x => x.Id == id);

            if (!result) return OperationResult.NotFound("ProgressoMapa não encontrado");

            return OperationResult.OK();
        }

        public IEnumerable<ObterProgressoMapaDto> Listar()
        {
            var progressos = _progressoMapaRepository.GetAll();

            return _mapper.Map<IEnumerable<ObterProgressoMapaDto>>(progressos);
        }

        public Result<ObterProgressoMapaDto> ObterPorId(int id)
        {
            var progressoMapa = _progressoMapaRepository.GetById(id);

            if (progressoMapa == null)
                return OperationResult.NotFound<ObterProgressoMapaDto>();

            var dto = _mapper.Map<ObterProgressoMapaDto>(progressoMapa);

            return OperationResult.OK(dto);
        }

        public Result<List<ObterLeaderboardDto>> ObterLeaderboard(int mapa)
        {
            var leaderboard = _progressoMapaRepository.ObterLeaderboard(mapa);

            if (leaderboard == null)
                return OperationResult.NotFound<List<ObterLeaderboardDto>>();

            return OperationResult.OK(leaderboard);
        }

        public Result Remover(int id)
        {
            var progressoMapa = _progressoMapaRepository.GetById(id);

            if (progressoMapa == null)
                return OperationResult.NotFound();

            _progressoMapaRepository.Remove(progressoMapa);

            return OperationResult.NoContent();
        }

        private Result Validar(ProgressoMapa progressoMapa)
        {
            if (progressoMapa.Mapa != Mapa.FOREST && progressoMapa.Mapa != Mapa.RIVER && progressoMapa.Mapa != Mapa.CITY)
                return OperationResult.ValidationError("Mapa é obrigatório");

            if (progressoMapa.Tempo == 0)
                return OperationResult.ValidationError("Tempo é obrigatório");

            if (progressoMapa.DataRegistro == null || progressoMapa.DataRegistro == DateTime.MinValue)
                return OperationResult.ValidationError("Data de registro é obrigatório");

            if (progressoMapa.UsuarioId == 0)
                return OperationResult.ValidationError("Usuário é obrigatório");

            if (progressoMapa.Pontuacao < 0)
                return OperationResult.ValidationError("Pontuação não pode ser negativa");

            return OperationResult.OK();
        }

        private bool Existe(int id, Mapa mapa)
        {
            return _progressoMapaRepository.Exists(x => x.Usuario.Id == id && x.Mapa == mapa);
        }

        private int CalcularPontuacao(ProgressoMapa progressoMapa)
        {
            int pontuacaoColecionaveis = (progressoMapa.Colecionavel + (progressoMapa.ColecionavelEspecial * 3)) * 100;
            int pontuacao = pontuacaoColecionaveis - progressoMapa.Tempo;

            if (pontuacao >= 0)
                return pontuacao;

            else
                return 0;
        }
    }
}