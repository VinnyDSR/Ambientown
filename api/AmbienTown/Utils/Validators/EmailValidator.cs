using System.Text.RegularExpressions;

namespace AmbienTown.Utils.Validators
{
    public static class EmailValidator
    {
        public static bool Validate(string email)
        {
            string regex = @"^([\w\-]+\.)*[\w\- ]+@([\w\- ]+\.)+([\w\-]{2,3})$";

            return Regex.IsMatch(email, regex);
        }
    }
}