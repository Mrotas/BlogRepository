using BlogRepository.Models;

namespace BlogRepository.Domain.Interfaces
{
    public interface IUserService
    {
        LoginResultModel LogIn(LoginModel loginModel);
        bool Register(RegisterModel registerModel);
    }
}