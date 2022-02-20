namespace AmbienTown.Utils.Security
{
    public class TokenOptions
    {
        public string Audience { get; set; }
        public int ExpiresInMinutes { get; set; }
        public string Issuer { get; set; }
        public string Secret { get; set; }
    }
}