namespace USER_LOGIN.Models
{
    public class Users
    {
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool? isSuccess { get; set; }
        public string? Message { get; set; }
    }
}