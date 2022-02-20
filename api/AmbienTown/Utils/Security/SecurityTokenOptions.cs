using System.Security;
using System.Text;

namespace AmbienTown.Utils.Security
{
    public class SecurityTokenOptions
    {
        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                Validate(value);

                _value = value;
            }
        }

        public void Validate(string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(salt);

            if (bytes.Length < 128)
            {
                throw new SecurityException("O tamanho do salt é inválido");
            }
        }
    }
}