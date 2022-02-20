using AmbienTown.Utils.Security;
using Xunit;

namespace AmbienTown.Tests.Utils
{
    public class PasswordHasherTest
    {
        [Trait("Categoria", "Segurança")]
        [Theory(DisplayName = "Encriptar e validar senha encriptada")]
        [InlineData("12345678")]
        [InlineData("abcdefghij")]
        [InlineData("<WMK';YD+#v]3}B")]
        public void EncriptarSenhaEValidarSenhaEncriptada(string senha)
        {
            var passwordHashed = PBKDF2PasswordHasher.HashPassword(senha);

            var result = PBKDF2PasswordHasher.ValidatePassword(senha, passwordHashed);

            Assert.True(result);
        }
    }
}