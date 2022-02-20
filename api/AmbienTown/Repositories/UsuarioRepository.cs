using AmbienTown.Models;
using AmbienTown.Repositories.Interfaces;
using AmbienTown.Utils.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AmbienTown.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly IQueryable<Usuario> defaultQuery;

        public UsuarioRepository(Entities entities) : base(entities)
        {
            DefaultQuery = DefaultQuery.Select(SelectFields);
            this.defaultQuery = this.entities.Query<Usuario>();
        }                        

        public int Cadastrar(Usuario usuario)
        {
            usuario.Senha = PBKDF2PasswordHasher.HashPassword(usuario.Senha);

            this.entities.Add(usuario);

            return usuario.Id;
        }

        public Usuario ObterPorEmail(string email)
        {
            return this.entities.Query<Usuario>()
                .Where(x => x.Email == email)
                .FirstOrDefault();
        }

        //public Usuario ObterPorCodigo(int codigo)
        //{
        //    return this.entities.Query<Usuario>()
        //        .Where(x => x.Codigo == codigo)
        //        .FirstOrDefault();
        //}

        public List<Usuario> ObterLeaderboard(int[] id)
        {
            return defaultQuery
                .Select(x => new Usuario
                {
                    Id = x.Id,
                    Email = x.Email,
                    Senha = x.Senha
                })
                .Where(x => id.Contains(x.Id)).ToList();
        }

        public Usuario ObterPorId(int id)
        {
            return defaultQuery
                .Select(x => new Usuario
                {
                    Id = x.Id,
                    Email = x.Email,
                    Senha = x.Senha
                })
                .FirstOrDefault(x => x.Id == id);
        }

        public Usuario ObterPorUsuarioSenha(string email, string senha)
        {
            var usuario = this.entities.Query<Usuario>()                
                .Where(x => x.Email == email)
                .Select(x => new Usuario
                {
                    Id = x.Id,
                    Apelido = x.Apelido,
                    Email = x.Email,
                    Senha = x.Senha,
                    Configuracao = x.Configuracao,
                    ConfiguracaoId = x.ConfiguracaoId,
                    Personagem = x.Personagem,
                    PersonagemId = x.PersonagemId,
                    Progressos = x.Progressos
                })
                .FirstOrDefault();

            if (PBKDF2PasswordHasher.ValidatePassword(senha, usuario.Senha))
                return new Usuario
                {
                    Id = usuario.Id,
                    Apelido = usuario.Apelido,
                    Email = usuario.Email,
                    Senha = usuario.Senha,
                    Configuracao = usuario.Configuracao,
                    ConfiguracaoId = usuario.ConfiguracaoId,
                    Personagem = usuario.Personagem,
                    PersonagemId = usuario.PersonagemId,
                    Progressos = usuario.Progressos
                };

            return null;
        }

        private Expression<Func<Usuario, Usuario>> SelectFields => x => new Usuario
        {
            Id = x.Id,
            Apelido = x.Apelido,
            Configuracao = x.Configuracao,
            Email = x.Email,
            Personagem = x.Personagem,
            Senha = x.Senha
        };
    }
}