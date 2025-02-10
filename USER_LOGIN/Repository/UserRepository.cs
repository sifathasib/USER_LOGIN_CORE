using Oracle.ManagedDataAccess.Client;
using System.Data;
using USER_LOGIN.Models;
using System.Threading.Tasks;
using USER_LOGIN.Data;

namespace USER_LOGIN.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataAccess _dataAccess;
        public UserRepository(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<Users> ValidateUser(LoginModel userLogin)
        {
            Users user = new Users();

            // Define your query with parameter placeholders (using the colon syntax for Oracle)
            string query = @"SELECT * FROM USERS WHERE USER_ID = :userid AND PASSWORD = :password";

            // Create an array of OracleParameter objects
            OracleParameter[] parameters = new OracleParameter[]
            {
                new OracleParameter("userid", userLogin.userId),
                new OracleParameter("password",  userLogin.userPass)
            };

            // Now pass the array to the method
            DataTable result = await _dataAccess.ExecuteQueryAsync(query, parameters);

            // If the query returned any rows, populate the user object
            if(result.Rows.Count > 0)
            {
                user.UserId = result.Rows[0]["USER_ID"].ToString();
                user.Password = result.Rows[0]["PASSWORD"].ToString();
                user.Username = result.Rows[0]["USERNAME"].ToString();
                user.isSuccess = true;
                user.Message = "Login successful";
            }else{
                user.isSuccess = false;
                user.Message = "Invalid userid or password";
            }


            return user;
        }
    }
}