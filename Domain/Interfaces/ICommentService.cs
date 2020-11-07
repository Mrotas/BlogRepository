using BlogRepository.Models;
using System.Collections.Generic;

namespace BlogRepository.Domain.Interfaces
{
    public interface ICommentService
    {
        List<CommentViewModel> GetCommentViewModelsByPostId(int postId);
    }
}