using AmbienTown.Services.Interfaces;
using AmbienTown.Utils.Email;
using AmbienTown.Utils.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TimeSheet.Api.Utils.Result;

namespace AmbienTown.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpOptions options;

        public EmailService(IOptions<AppSettings> options)
        {
            this.options = options.Value.SmtpOptions;
        }

        public async Task<Result> Enviar(MailMessage message)
        {
            try
            {
                using (var client = new SmtpClient(options.Host, options.Port))
                {
                    message.From = new MailAddress(options.From);

                    if (options.UseSpecifiedPickupDirectory == true)
                    {
                        client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        client.PickupDirectoryLocation = options.PickupDirectoryLocation;
                        client.EnableSsl = false;
                    }
                    else
                    {
                        if (client.Port == 587 || client.Port == 465)
                        {
                            client.EnableSsl = true;
                        }

                        if (options.Relay == false)
                        {
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;                            
                            client.Credentials = new NetworkCredential(options.User, options.Password);
                        }
                    }

                    client.Timeout = 30000;

                    await client.SendMailAsync(message);

                    return OperationResult.OK();
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Error(ex);
            }
        }
    }
}