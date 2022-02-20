using AmbienTown.Models;
using AmbienTown.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using TimeSheet.Api.Controllers;
using TimeSheet.Api.Utils.Result;

namespace AmbienTown.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
        readonly ILogger<UsuarioController> logger;

        public UsuarioController(IUsuarioService usuarioService, ILogger<UsuarioController> log)
        {
            _usuarioService = usuarioService;
            logger = log;
        }

        /// <summary>
        /// Consulta um usuário por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetById([BindRequired] int id)
        {
            var result = _usuarioService.ObterPorId(id);

            if (result.Type == OperationResultType.NotFound) return NotFound(result);

            logger.LogInformation("Action GetById :: UsuarioController -> executou em: " + DateTime.Now.ToString());

            return Ok(result);
        }

        /// <summary>
        /// Cadastra um usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult Post([FromBody] Usuario usuario)
        {
            var result = _usuarioService.Cadastrar(usuario);

            if (result.Type == OperationResultType.ValidationError) return BadRequest(result);
            if (result.Type == OperationResultType.Conflict) return Conflict(result);

            logger.LogInformation("Action Post :: UsuarioController -> executou em: " + DateTime.Now.ToString());

            return Ok(result);
        }

        /// <summary>
        /// Altera um usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Put([FromBody] Usuario usuario)
        {
            var result = _usuarioService.Atualizar(usuario);

            if (result.Type == OperationResultType.ValidationError) return BadRequest(result);
            if (result.Type == OperationResultType.Conflict) return Conflict(result);
            if (result.Type == OperationResultType.NotFound) return NotFound(result);

            logger.LogInformation("Action Put :: UsuarioController -> executou em: " + DateTime.Now.ToString());

            return Ok(result);
        }

        /// <summary>
        /// Efetua o login de um usuário
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(Login login)
        {
            var result = _usuarioService.Autenticar(login);

            if (result.Failure) return ErrorResponse(result);

            logger.LogInformation($"Action Login :: UsuarioController -> {login.Email} efetuou login em: " + DateTime.Now.ToString());

            return Ok(result);
        }        
    }
}