using BlogRepository.DataAccess.Collection;
using BlogRepository.Models.Blog;
using System.Collections.Generic;

namespace BlogRepository.DataAccess.Dao.Interfaces
{
    public interface IBlogDao
    {
        List<Blog> GetRecent();
        Blog GetById(int id);
        Blog GetByUserId(int userId);
        void Update(BlogViewModel blogViewModel);
    }
}