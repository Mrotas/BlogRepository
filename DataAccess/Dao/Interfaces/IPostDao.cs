using BlogRepository.DataAccess.Collection;
using System.Collections.Generic;

namespace BlogRepository.DataAccess.Dao.Interfaces
{
    public interface IPostDao
    {
        List<Post> GetRecent();
        Post GetById(int id);
        List<Post> GetByBlogId(int blogId);
        void UpdateViews(int postId, int viewsCount);
        void Delete(int postId);
    }
}