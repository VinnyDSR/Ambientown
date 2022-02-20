using AmbienTown.Utils.Email;
using AmbienTown.Utils.Security;

namespace AmbienTown.Utils.Settings
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public PasswordOptions PasswordOptions { get; set; }
        public SmtpOptions SmtpOptions { get; set; }
        public SecurityTokenOptions SecurityToken { get; set; }
        public TokenOptions TokenOptions { get; set; }
    }
}