using AmbienTown.Utils.Security;

namespace AmbienTown.Services.Interfaces
{
    public interface ISecurityTokenService
    {
        TemporaryRequest FromToken(string encriptedToken);
        string Generate(TemporaryRequest temporaryRequest);
    }
}