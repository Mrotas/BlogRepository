using BlogRepository.Models;

namespace BlogRepository.Domain.Interfaces
{
    public interface IUserAuthenticationService
    {
        LoginResultModel LogIn(LoginModel loginModel);
        RegisterResultModel Register(RegisterModel registerModel);
    }
}