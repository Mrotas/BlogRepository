using BlogRepository.DataAccess.Collection;
using BlogRepository.Models;
using System.Collections.Generic;

namespace BlogRepository.DataAccess.Dao.Interfaces
{
    public interface IUserDao
    {
        List<User> GetUsers();
        User GetById(int id);
        User GetByUsername(string username);
        bool Create(RegisterModel registerModel);
    }
}