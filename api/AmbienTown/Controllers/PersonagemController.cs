using AmbienTown.Dto.Personagem;
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
    [Route("api/personagens")]
    [ApiController]
    public class PersonagemController : ControllerBase
    {
        private readonly IPersonagemService _personagemService;
        readonly ILogger<PersonagemController> logger;

        public PersonagemController(IPersonagemService personagemService, ILogger<PersonagemController> log)
        {
            _personagemService = personagemService;
            logger = log;
        }

        /// <summary>
        /// Consulta todos os personagens
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            var result = _personagemService.Listar();

            return Ok(result);
        }

        /// <summary>
        /// Consulta personagem por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetById([BindRequired] int id)
        {
            var result = _personagemService.ObterPorId(id);

            if (result.Type == OperationResultType.NotFound) return NotFound(result);

            logger.LogInformation("Action GetById :: PersonagemController -> executou em: " + DateTime.Now.ToString());

            return Ok(result);
        }

        /// <summary>
        /// Cadastra um personagem
        /// </summary>
        /// <param name="personagem"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult Post([FromBody] NovoPersonagemDto personagem)
        {
            var result = _personagemService.Cadastrar(personagem);

            if (result.Type == OperationResultType.ValidationError) return BadRequest(result);
            if (result.Type == OperationResultType.Conflict) return Conflict(result);

            logger.LogInformation($"Action Post :: PersonagemController -> {result.Value} executou em: " + DateTime.Now.ToString());

            return Ok(result);
        }

        /// <summary>
        /// Altera um personagem
        /// </summary>
        /// <param name="personagem"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Put([FromBody] AtualizarPersonagemDto personagem)
        {
            var result = _personagemService.Atualizar(personagem);

            if (result.Type == OperationResultType.ValidationError) return BadRequest(result);
            if (result.Type == OperationResultType.Conflict) return Conflict(result);
            if (result.Type == OperationResultType.NotFound) return NotFound(result);

            logger.LogInformation($"Action Put :: PersonagemController -> {personagem.Id} executou em: " + DateTime.Now.ToString());

            return Ok(result);
        }
    }
}