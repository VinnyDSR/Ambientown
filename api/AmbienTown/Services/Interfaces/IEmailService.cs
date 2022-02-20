using System.Net.Mail;
using System.Threading.Tasks;
using TimeSheet.Api.Utils.Result;

namespace AmbienTown.Services.Interfaces
{
    public interface IEmailService
    {
        Task<Result> Enviar(MailMessage message);
    }
}