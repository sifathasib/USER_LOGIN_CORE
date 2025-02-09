using System.Threading.Tasks;
using USER_LOGIN.Models;

namespace USER_LOGIN.Repository
{
    public interface IUserRepository
    {
        Task<Users> ValidateUser(LoginModel userLogin);
    }
}