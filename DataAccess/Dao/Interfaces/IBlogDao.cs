using BlogRepository.DataAccess.Collection;
using System.Collections.Generic;

namespace BlogRepository.DataAccess.Dao.Interfaces
{
    public interface IBlogDao
    {
        List<Blog> GetRecent();
        Blog GetById(int id);
        Blog GetByUserId(int userId);
    }
}