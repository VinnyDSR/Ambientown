using AmbienTown.Dto.ProgressoMapa;
using AmbienTown.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using TimeSheet.Api.Utils.Result;

namespace AmbienTown.Controllers
{
    [Route("api/progressoMapa")]
    [ApiController]
    public class ProgressoMapaController : ControllerBase
    {
        private readonly IProgressoMapaService _progressoMapaService;
        readonly ILogger<ProgressoMapaController> logger;

        public ProgressoMapaController(IProgressoMapaService progressoMapaService, ILogger<ProgressoMapaController> log)
        {
            _progressoMapaService = progressoMapaService;
            logger = log;
        }

        /// <summary>
        /// Consulta todos os progressos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            var result = _progressoMapaService.Listar();

            return Ok(result);
        }

        /// <summary>
        /// Cadastra um progresso
        /// </summary>
        /// <param name="progressoMapa"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult Post([FromBody] NovoProgressoMapaDto progressoMapa)
        {
            var result = _progressoMapaService.Cadastrar(progressoMapa);

            if (result.Type == OperationResultType.ValidationError) return BadRequest(result);
            if (result.Type == OperationResultType.Conflict) return Conflict(result);

            logger.LogInformation($"Action Post :: ProgressoMapaController -> {progressoMapa.UsuarioId} executou em: " + DateTime.Now.ToString());

            return CreatedAtAction("GetById", new { id = result.Value }, null);
        }

        /// <summary>
        /// Consulta a leaderboard de um determinado mapa
        /// </summary>
        /// <param name="mapa"></param>
        /// <returns></returns>
        [HttpGet("{mapa}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetLeaderboard([BindRequired] int mapa)
        {
            var result = _progressoMapaService.ObterLeaderboard(mapa);

            if (result.Type == OperationResultType.NotFound) return NotFound(result);

            logger.LogInformation($"Action Post :: ProgressoMapaController -> executou GetLeaderboard em: " + DateTime.Now.ToString());

            return Ok(result);
        }
    }
}