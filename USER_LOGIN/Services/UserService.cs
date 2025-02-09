using System.Threading.Tasks;
using USER_LOGIN.Models;
using USER_LOGIN.Repository;

namespace USER_LOGIN.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    
        public async Task<Users> ValidateUserAsync(LoginModel userLogin)
        {
            Users user = await _userRepository.ValidateUser(userLogin);
            
            return user;
        }
    }
}