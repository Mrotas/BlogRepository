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
        int Insert(int userId);
        void Update(BlogEditViewModel blog);
        void UpdateViews(int blogId, int viewsCount);
        void Delete(int id);
    }
}