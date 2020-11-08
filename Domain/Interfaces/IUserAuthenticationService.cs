using BlogRepository.Models;

namespace BlogRepository.Domain.Interfaces
{
    public interface IUserAuthenticationService
    {
        LoginResultModel LogIn(LoginModel loginModel);
        bool Register(RegisterModel registerModel);
    }
}