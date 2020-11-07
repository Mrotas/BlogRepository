using BlogRepository.DataAccess.Collection;
using System.Collections.Generic;

namespace BlogRepository.DataAccess.Dao.Interfaces
{
    public interface ICommentDao
    {
        List<Comment> GetByPostId(int postId);
    }
}