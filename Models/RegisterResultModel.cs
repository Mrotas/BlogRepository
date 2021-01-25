namespace BlogRepository.Models
{
    public class RegisterResultModel
    {
        public RegisterResultModel(int userId, bool registerSuccess)
        {
            UserId = userId;
            RegisterSuccess = registerSuccess;
        }

        public int UserId { get; set; }
        public bool RegisterSuccess { get; set; }
    }
}
