using AmbienTown.Dto.Personagem;
using System.Collections.Generic;
using TimeSheet.Api.Utils.Result;

namespace AmbienTown.Services.Interfaces
{
    public interface IPersonagemService
    {
        Result<ObterPersonagemDto> ObterPorId(int id);
        IEnumerable<ObterPersonagemDto> Listar();
        Result<int> Cadastrar(NovoPersonagemDto personagem);
        Result Atualizar(AtualizarPersonagemDto personagem);
        Result Remover(int id);
        Result Existe(int id);
    }
}