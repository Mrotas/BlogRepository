using BlogRepository.DataAccess.Collection;
using BlogRepository.Models.Post;
using System.Collections.Generic;

namespace BlogRepository.DataAccess.Dao.Interfaces
{
    public interface IPostDao
    {
        List<Post> GetRecent();
        Post GetById(int id);
        List<Post> GetByBlogId(int blogId);
        void UpdateViews(int postId, int viewsCount);
        int Insert(string title, string content, string tags, int blogId);
        void Update(PostEditViewModel post);
        void Delete(int postId);
    }
}