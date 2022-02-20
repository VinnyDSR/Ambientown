using AmbienTown.Dto.RecuperarSenha;
using AmbienTown.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TimeSheet.Api.Utils.Result;

namespace AmbienTown.Controllers
{
    [Route("api/alteracaoSenha")]
    //[ApiController]
    public class AlteracaoSenhaController : Controller
    {
        private readonly IRecuperacaoSenhaService _recuperacaoSenhaService;
        private readonly IUsuarioService _usuarioService;
        readonly ILogger<AlteracaoSenhaController> logger;

        public AlteracaoSenhaController(IRecuperacaoSenhaService service, IUsuarioService usuarioService, ILogger<AlteracaoSenhaController> log)
        {
            this._recuperacaoSenhaService = service;
            this._usuarioService = usuarioService;
            logger = log;
        }

        /// <summary>
        /// Envia o e-mail para recuperação de senha
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("recuperar")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> RecuperarSenha([FromBody] RecuperarSenhaDto dto)
        {
            var result = await _recuperacaoSenhaService.RecuperarSenha(dto);

            if (result.Type == OperationResultType.ValidationError) return BadRequest(result);
            if (result.Type == OperationResultType.Error) return StatusCode(StatusCodes.Status500InternalServerError, result);
            if (result.Type == OperationResultType.NotFound) return NotFound(result);

            logger.LogInformation("Action GetById :: PersonagemController -> executou em: " + DateTime.Now.ToString());

            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Recupera o token que será validado para alterar a senha
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("alterar")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AlterarSenha(string token)
        {
            AlterarSenhaDto alterar = new AlterarSenhaDto
            {
                Token = token
            };
            return View(alterar);
        }

        /// <summary>
        /// Realiza a alteração da senha do usuário
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("alterar")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = _usuarioService.AlterarSenha(dto);

                if (result.Type == OperationResultType.NotFound) return NotFound(result);
                if (result.Type == OperationResultType.ValidationError) return BadRequest(result);
                if (result.Type == OperationResultType.Conflict) return Conflict(result);

                dto.IsSuccess = true;
            }
            
            return View(dto);
        }
    }
}