using AmbienTown.Dto.RecuperarSenha;
using System.Threading.Tasks;
using TimeSheet.Api.Utils.Result;

namespace AmbienTown.Services.Interfaces
{
    public interface IRecuperacaoSenhaService
    {
        Task<Result> RecuperarSenha(RecuperarSenhaDto dto);
    }
}