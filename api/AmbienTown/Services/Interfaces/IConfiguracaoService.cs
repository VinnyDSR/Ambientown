using AmbienTown.Dto.Configuracao;
using System.Collections.Generic;
using TimeSheet.Api.Utils.Result;

namespace AmbienTown.Services.Interfaces
{
    public interface IConfiguracaoService
    {
        Result<ObterConfiguracaoDto> ObterPorId(int id);
        IEnumerable<ObterConfiguracaoDto> Listar();
        Result<int> Cadastrar(NovaConfiguracaoDto roupa);
        Result Atualizar(AtualizarConfiguracaoDto roupa);
        Result Remover(int id);
        Result Existe(int id);
    }
}