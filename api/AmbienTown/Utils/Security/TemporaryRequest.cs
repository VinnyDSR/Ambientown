using System;

namespace AmbienTown.Utils.Security
{
    public enum TemporaryRequestType
    {
        ACCOUNT_ACTIVATION,
        ACCOUNT_CREATED,
        RECOVER_PASSWORD,
    }

    public class TemporaryRequest
    {
        public int UserId { get; set; }
        public TemporaryRequestType Type { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}