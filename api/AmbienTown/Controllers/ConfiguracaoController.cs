using AmbienTown.Dto.Configuracao;
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
    [Route("api/configuracoes")]
    [ApiController]
    public class ConfiguracaoController : ControllerBase
    {
        private readonly IConfiguracaoService _configuracaoService;
        readonly ILogger<ConfiguracaoController> logger;

        public ConfiguracaoController(IConfiguracaoService configuracaoService, ILogger<ConfiguracaoController> log)
        {
            _configuracaoService = configuracaoService;
            logger = log;
        }

        /// <summary>
        /// Consulta todas as configurações
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            var result = _configuracaoService.Listar();

            return Ok(result);
        }

        /// <summary>
        /// Consulta configuração por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetById([BindRequired] int id)
        {
            var result = _configuracaoService.ObterPorId(id);

            if (result.Type == OperationResultType.NotFound) return NotFound(result);

            logger.LogInformation("Action GetById :: ConfiguracaoController -> executou em: " + DateTime.Now.ToString());

            return Ok(result);
        }

        /// <summary>
        /// Cadastra uma configuração
        /// </summary>
        /// <param name="configuracao"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult Post([FromBody] NovaConfiguracaoDto configuracao)
        {
            var result = _configuracaoService.Cadastrar(configuracao);

            if (result.Type == OperationResultType.ValidationError) return BadRequest(result);                
            if (result.Type == OperationResultType.Conflict) return Conflict(result);

            logger.LogInformation($"Action Post :: ConfiguracaoController -> {result.Value} executou em: " + DateTime.Now.ToString());

            return Ok(result);
        }

        /// <summary>
        /// Altera uma configuração
        /// </summary>
        /// <param name="configuracao"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Put([FromBody] AtualizarConfiguracaoDto configuracao)
        {
            var result = _configuracaoService.Atualizar(configuracao);

            if (result.Type == OperationResultType.ValidationError) return BadRequest(result);
            if (result.Type == OperationResultType.Conflict) return Conflict(result);
            if (result.Type == OperationResultType.NotFound) return NotFound(result);

            logger.LogInformation($"Action Put :: PersonagemController -> {configuracao.Id} executou em: " + DateTime.Now.ToString());

            return Ok(result);
        }
    }
}