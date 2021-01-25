using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using BlogRepository.Domain.Interfaces;
using BlogRepository.Models;
using System;

namespace BlogRepository.Domain
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserDao _userDao;
        public UserAuthenticationService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public LoginResultModel LogIn(LoginModel loginModel)
        {
            User user = _userDao.GetByUsername(loginModel.Username);
            if (loginModel.Password == user?.Password)
            {
                return new LoginResultModel(user.Id, true);
            }

            return new LoginResultModel(0, false);
        }

        public RegisterResultModel Register(RegisterModel registerModel)
        {
            try
            {
                int userId = _userDao.Create(registerModel);
                return new RegisterResultModel(userId, true);
            }
            catch (Exception)
            {
                return new RegisterResultModel(0, false);
            }
        }
    }
}
