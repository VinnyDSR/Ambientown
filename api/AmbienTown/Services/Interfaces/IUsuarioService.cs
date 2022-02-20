using AmbienTown.Dto.RecuperarSenha;
using AmbienTown.Models;
using System.Collections.Generic;
using TimeSheet.Api.Utils.Result;

namespace AmbienTown.Services.Interfaces
{
    public interface IUsuarioService
    {
        Result<Usuario> ObterPorId(int id);
        IEnumerable<Usuario> Listar();
        Result<int> Cadastrar(Usuario usuario);
        Result Atualizar(Usuario usuario);
        Result Remover(int id);
        Result Existe(int id);
        //Result AlterarSenha(int usuarioId, string senha);
        Result AlterarSenha(AlterarSenhaDto dto);
        Result Autenticar(Login login);
        Result<List<Usuario>> ObterLeaderboard(string ids);
    }
}