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

        public bool Register(RegisterModel registerModel)
        {
            try
            {
                bool success = _userDao.Create(registerModel);
                return success;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
