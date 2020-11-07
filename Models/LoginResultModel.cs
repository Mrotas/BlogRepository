namespace BlogRepository.Models
{
    public class LoginResultModel
    {
        public LoginResultModel(int userId, bool logInSuccess)
        {
            UserId = userId;
            LogInSuccess = logInSuccess;
        }

        public int UserId { get; set; }
        public bool LogInSuccess { get; set; }
    }
}
