using BlogRepository.DataAccess.Collection;
using System.Collections.Generic;

namespace BlogRepository.DataAccess.Dao.Interfaces
{
    public interface ICommentDao
    {
        List<Comment> GetComments();
        List<Comment> GetByPostId(int postId);
        void Insert(int postId, string content, int? userId);
        void Delete(int commentId);
    }
}