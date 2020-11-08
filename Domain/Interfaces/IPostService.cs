using BlogRepository.Models;
using System.Collections.Generic;

namespace BlogRepository.Domain.Interfaces
{
    public interface IPostService
    {
        List<PostViewModel> GetPostViewModelsByBlogId(int blogId);
        PostViewModel GetPostViewModelByPostId(int postId);
        void PostComment(int postId, string content, int? userId);
        void UpdateViewsCount(int postId);
        void LikePost(int postId, int? userId);
        void LikeComment(int commentId, int? userId);
    }
}