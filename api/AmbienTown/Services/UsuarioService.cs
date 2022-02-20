using AmbienTown.Dto.RecuperarSenha;
using AmbienTown.Enums;
using AmbienTown.Models;
using AmbienTown.Repositories.Interfaces;
using AmbienTown.Services.Interfaces;
using AmbienTown.Utils.Security;
using AmbienTown.Utils.Validators;
using AutoMapper;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeSheet.Api.Utils.Result;

namespace AmbienTown.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly ISecurityTokenService _securityTokenService;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, ISecurityTokenService securityTokenService)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _securityTokenService = securityTokenService;
        }

        public Result Atualizar(Usuario usuario)
        {
            var existsResult = Existe(usuario.Id);

            if (existsResult.Failure) return existsResult;            

            var validationResult = Validar(usuario);

            if (validationResult.Failure) return validationResult;

            _usuarioRepository.Update(usuario);

            return OperationResult.OK();
        }

        public Result<int> Cadastrar(Usuario dto)
        {
            var usuario = dto;

            var validar = Validar(usuario);

            if (validar.Failure) return new Result<int>(validar);

            var id = _usuarioRepository.Cadastrar(usuario);

            return OperationResult.Created<int>(id);
        }

        public Result Existe(int id)
        {
            var result = _usuarioRepository.Exists(x => x.Id == id);

            if (!result) return OperationResult.NotFound("Usuário não encontrado");

            return OperationResult.OK();
        }

        public IEnumerable<Usuario> Listar()
        {
            return _usuarioRepository.GetAll();
        }

        public Result<Usuario> ObterPorId(int id)
        {
            var usuario = _usuarioRepository.GetById(id);

            if (usuario == null)
                return OperationResult.NotFound<Usuario>();

            return OperationResult.OK(usuario);
        }

        public Result<List<Usuario>> ObterLeaderboard(string id)
        {
            int[] ids = Array.ConvertAll(id.Split(','), s => int.Parse(s));

            var usuarios = _usuarioRepository.ObterLeaderboard(ids);

            if (usuarios == null)
                return OperationResult.NotFound<List<Usuario>>();

            return OperationResult.OK(usuarios);
        }

        public Result Remover(int id)
        {
            var usuario = _usuarioRepository.GetById(id);

            if (usuario == null)
                return OperationResult.NotFound();

            _usuarioRepository.Remove(usuario);

            return OperationResult.NoContent();
        }

        public Result Autenticar(Login login)
        {
            var result = ValidarLogin(login);

            if (result.Failure) return result;

            var usuario = _usuarioRepository.ObterPorUsuarioSenha(login.Email, login.Senha);

            if (usuario == null)
                return OperationResult.Unauthorized("Usuário não encontrado");

            usuario.Progressos = GetBestProgress(usuario.Progressos);

            return OperationResult.OK(usuario);
        }

        public Result AlterarSenha(AlterarSenhaDto dto)
        {
            var token = _securityTokenService.FromToken(dto.Token);

            var result = ValidarToken(token);

            if (result.Failure) return result;

            var resultSenha = ValidarNovaSenha(dto.Senha, dto.ConfirmacaoSenha);

            if (resultSenha.Failure) return result;

            var usuario = _usuarioRepository.ObterPorId(token.UserId);

            usuario.Senha = PBKDF2PasswordHasher.HashPassword(dto.Senha);

            string[] propriedadesParaAtualizar = { nameof(usuario.Senha) };

            _usuarioRepository.PartialUpdate(usuario, propriedadesParaAtualizar);

            return OperationResult.OK();
        }

        private Result Validar(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Apelido))
                return OperationResult.ValidationError("Apelido é obrigatório");

            if (usuario.Apelido.Length > 30)
                return OperationResult.ValidationError("Apelido deve ter menos de 30 caracteres");            

            if (string.IsNullOrWhiteSpace(usuario.Email))
                return OperationResult.ValidationError("Email é obrigatório");

            if (!EmailValidator.Validate(usuario.Email))
                return OperationResult.ValidationError("Email inválido");

            if (usuario.Email.Length > 100)
                return OperationResult.ValidationError("Email deve ter no máximo 100 caracteres");

            if (string.IsNullOrWhiteSpace(usuario.Senha))
                return OperationResult.ValidationError("Senha é obrigatório");

            if (usuario.Senha.Length < 10)
                return OperationResult.ValidationError("Senha deve ter no mínimo 10 caracteres");

            if (usuario.Senha.Length > 100)
                return OperationResult.ValidationError("Senha deve ter no máximo 100 caracteres");

            if (usuario.ConfiguracaoId == 0)
                return OperationResult.ValidationError("Configuração é obrigatório");

            if (usuario.PersonagemId == 0)
                return OperationResult.ValidationError("Personagem é obrigatório");

            if (Existe(usuario.Id, usuario.Apelido))
                return OperationResult.Conflict("Usuário com este apelido já foi cadastrado");

            if (ExisteEmail(usuario.Id, usuario.Email))
                return OperationResult.Conflict("Usuário com este email já foi cadastrado");

            return OperationResult.OK();
        }

        private Result ValidarLogin(Login login)
        {
            if (string.IsNullOrEmpty(login.Email))
                return OperationResult.ValidationError("Email não informado");

            if (!EmailValidator.Validate(login.Email))
                return OperationResult.ValidationError("Email possui formato inválido");

            if (string.IsNullOrWhiteSpace(login.Senha))
                return OperationResult.ValidationError("Senha não informada");

            return OperationResult.OK();
        }

        private Result ValidarToken(TemporaryRequest token)
        {
            if (token.Type != TemporaryRequestType.RECOVER_PASSWORD)
                return OperationResult.ValidationError("O tipo do token é inválido");

            if (token.ExpirationDate <= DateTime.Now)
                return OperationResult.ValidationError("Este token já expirou");

            var exists = Existe(token.UserId);

            if (exists.Failure) return OperationResult.NotFound("Usuário não encontrado");

            return OperationResult.OK();
        }

        private ICollection<ProgressoMapa> GetBestProgress(ICollection<ProgressoMapa> progress)
        {
            var bestProgress = new List<ProgressoMapa>();

            var forestProgress = progress.Where(x => x.Mapa == Mapa.FOREST);
            bestProgress.Add(forestProgress.MaxBy(x => x.Pontuacao).FirstOrDefault());

            var cityProgress = progress.Where(x => x.Mapa == Mapa.CITY);
            bestProgress.Add(cityProgress.MaxBy(x => x.Pontuacao).FirstOrDefault());

            var riverProgress = progress.Where(x => x.Mapa == Mapa.RIVER);
            bestProgress.Add(riverProgress.MaxBy(x => x.Pontuacao).FirstOrDefault());

            progress = bestProgress;

            return progress;
        }

        private Result ValidarNovaSenha(string senha, string confirmacaoSenha)
        {
            if (senha != confirmacaoSenha)               
                return OperationResult.ValidationError("As senhas não coincidem");

            if (string.IsNullOrWhiteSpace(senha))
                return OperationResult.ValidationError("Senha é obrigatório");

            if (senha.Length < 10)
                return OperationResult.ValidationError("Senha deve ter no mínimo 10 caracteres");

            if (senha.Length > 100)
                return OperationResult.ValidationError("Senha deve ter no máximo 100 caracteres");

            return OperationResult.OK();
        }

        private bool Existe(int id, string apelido)
        {
            if (id == 0)
            {
                return _usuarioRepository.Exists(x => x.Apelido.Equals(apelido));
            }

            return _usuarioRepository.Exists(x => x.Apelido.Equals(apelido) && x.Id != id);
        }

        private bool ExisteEmail(int id, string email)
        {
            if (id == 0)
            {
                return _usuarioRepository.Exists(x => x.Email.Equals(email));
            }

            return _usuarioRepository.Exists(x => x.Email.Equals(email) && x.Id != id);
        }
    }
}