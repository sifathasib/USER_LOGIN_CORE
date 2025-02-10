using System.Security.Claims;

namespace USER_LOGIN.Services
{
    public interface ITokenService
    {
        ClaimsPrincipal ValidateToken(string token);
    }
}