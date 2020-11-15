using BlogRepository.DataAccess.Collection;
using System.Collections.Generic;

namespace BlogRepository.DataAccess.Dao.Interfaces
{
    public interface IPostLikeDao
    {
        List<PostLike> GetByPostId(int postId);
        void Insert(int postId, int? userId);
        void Delete(int postLikeId);
    }
}