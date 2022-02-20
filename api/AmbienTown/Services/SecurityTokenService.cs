using AmbienTown.Services.Interfaces;
using AmbienTown.Utils.Security;
using AmbienTown.Utils.Settings;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AmbienTown.Services
{
    public class SecurityTokenService : ISecurityTokenService
    {
        private readonly SecurityTokenOptions salt;

        public SecurityTokenService(IOptions<AppSettings> options)
        {
            this.salt = options.Value.SecurityToken;
        }

        public string Generate(TemporaryRequest temporaryRequest)
        {
            var token = $"{temporaryRequest.UserId}|{temporaryRequest.Type}|{temporaryRequest.ExpirationDate:yyyy/MM/dd HH:mm:ss}";

            var bytesToBeEncrypted = Encoding.ASCII.GetBytes(token);
            var textBytes = Encoding.ASCII.GetBytes(this.salt.Value);

            textBytes = SHA256.Create().ComputeHash(textBytes);

            var bytesEncrypted = Encrypt(bytesToBeEncrypted, textBytes, this.salt.Value);

            return Convert.ToBase64String(bytesEncrypted);
        }

        public TemporaryRequest FromToken(string encriptedToken)
        {
            encriptedToken = encriptedToken.Replace(' ', '+');

            var bytesToBeDecrypted = Convert.FromBase64String(encriptedToken);

            var textBytes = Encoding.ASCII.GetBytes(this.salt.Value);

            textBytes = SHA256.Create().ComputeHash(textBytes);

            var bytesDecrypted = Decrypt(bytesToBeDecrypted, textBytes, this.salt.Value);

            var token = Encoding.ASCII.GetString(bytesDecrypted).Split("|");

            if (!Enum.TryParse(token[1], false, out TemporaryRequestType type))
            {
                throw new InvalidCastException($"Não foi possível realizar o parse de {token[1]} para o tipo correto");
            }

            return new TemporaryRequest
            {
                UserId = int.Parse(token[0]),
                Type = type,
                ExpirationDate = DateTime.Parse(token[2])
            };
        }

        private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] textBytes, string salt)
        {
            byte[] encryptedBytes = null;

            var saltBytes = Encoding.ASCII.GetBytes(salt);

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(textBytes, saltBytes, 1000); //-V3114

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] textBytes, string salt)
        {
            byte[] decryptedBytes = null;

            var saltBytes = Encoding.UTF8.GetBytes(salt);

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(textBytes, saltBytes, 1000); //-V3114

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }

                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
    }
}