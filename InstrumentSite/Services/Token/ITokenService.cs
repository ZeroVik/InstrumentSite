using InstrumentSite.Models;

namespace InstrumentSite.Services.Token
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
    }
}
