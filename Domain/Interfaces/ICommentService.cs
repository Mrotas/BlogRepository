using BlogRepository.Models.Comment;
using System.Collections.Generic;

namespace BlogRepository.Domain.Interfaces
{
    public interface ICommentService
    {
        List<CommentViewModel> GetCommentViewModelsByPostId(int postId);
        void Delete(int id);
    }
}