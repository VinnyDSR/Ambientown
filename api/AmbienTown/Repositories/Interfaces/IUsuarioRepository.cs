using AmbienTown.Models;
using System.Collections.Generic;

namespace AmbienTown.Repositories.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        int Cadastrar(Usuario usuario);
        Usuario ObterPorUsuarioSenha(string email, string senha);
        Usuario ObterPorEmail(string email);
        Usuario ObterPorId(int id);
        List<Usuario> ObterLeaderboard(int[] id);
        //Usuario ObterPorCodigo(int codigo);
    }
}