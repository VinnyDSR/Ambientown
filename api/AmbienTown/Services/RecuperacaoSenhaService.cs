using AmbienTown.Dto.RecuperarSenha;
using AmbienTown.Models;
using AmbienTown.Repositories.Interfaces;
using AmbienTown.Services.Interfaces;
using AmbienTown.Utils.Security;
using AmbienTown.Utils.Settings;
using AmbienTown.Utils.Validators;
using Microsoft.Extensions.Options;
using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TimeSheet.Api.Utils.Result;

namespace AmbienTown.Services
{
    public class RecuperacaoSenhaService : IRecuperacaoSenhaService
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ISecurityTokenService securityTokenService;
        private readonly IEmailService emailService;
        private readonly TokenOptions options;

        public RecuperacaoSenhaService(
            IUsuarioRepository usuarioRepository,
            ISecurityTokenService securityTokenService,
            IEmailService emailService,
            IOptions<AppSettings> options)
        {
            this.usuarioRepository = usuarioRepository;
            this.securityTokenService = securityTokenService;
            this.emailService = emailService;
            this.options = options.Value.TokenOptions;
        }

        public async Task<Result> RecuperarSenha(RecuperarSenhaDto dto)
        {
            var result = ObterUsuarioPorEmail(dto);

            if (result.Failure) return result;            

            return await EnviarEmailDeRecuperacaoDeSenha(result.Value);

            //if (resultEmail.Failure) return resultEmail;

            //return AtualizarCodigoDeRecuperacaoSenha(result.Value);
        }

        private Result<Usuario> ObterUsuarioPorEmail(RecuperarSenhaDto dto)
        {
            var result = ValidarEmail(dto.Email);

            if (result.Failure) return new Result<Usuario>(result);

            var usuario = usuarioRepository.ObterPorEmail(dto.Email);

            if (usuario == null)
                return OperationResult.NotFound<Usuario>("E-mail ou senha incorretos");

            return OperationResult.OK(usuario);
        }

        private Result ValidarEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return OperationResult.ValidationError("E-mail não informado");

            if (!EmailValidator.Validate(email))
                return OperationResult.ValidationError("O e-mail informado é inválido");

            return OperationResult.OK();
        }

        private async Task<Result> EnviarEmailDeRecuperacaoDeSenha(Usuario usuario)
        {
            var temporaryRequest = new TemporaryRequest
            {
                UserId = usuario.Id,
                Type = TemporaryRequestType.RECOVER_PASSWORD,
                ExpirationDate = DateTime.Now.AddMinutes(options.ExpiresInMinutes)
            };

            var token = securityTokenService.Generate(temporaryRequest);

            var message = new MailMessage();
            message.To.Add(new MailAddress(usuario.Email, usuario.Apelido));
            message.Subject = "Recuperação de Senha";
            message.Body = $"Acesse o <a href=\"https://ambientown.ddns.net/api/alteracaoSenha/alterar?token={token}\">link</a> para cadastrar uma nova senha.";
            //message.Body = $"O token para redefinir a senha é {token}. \nO prazo de validade deste token é de <b>{options.ExpiresInMinutes} minutos</b>. Não compartilhe este código com ninguém!";
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;

            return await emailService.Enviar(message);
        }
    }
}