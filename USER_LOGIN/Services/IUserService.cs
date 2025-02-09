using System.Threading.Tasks;
using USER_LOGIN.Models;
namespace USER_LOGIN.Services
{
    public interface IUserService
    {
        
        Task<Users> ValidateUserAsync(LoginModel userLogin);
    }
}