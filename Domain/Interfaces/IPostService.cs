using BlogRepository.DataAccess.Collection;
using BlogRepository.Models.Post;
using System.Collections.Generic;

namespace BlogRepository.Domain.Interfaces
{
    public interface IPostService
    {
        List<PostViewModel> GetPostViewModelsByBlogId(int blogId);
        PostViewModel GetPostViewModelByPostId(int postId);
        List<Post> GetByTag(string tag);
        List<Post> GetBySearchString(string searchString);
        void PostComment(int postId, string content, int? userId);
        void UpdateViewsCount(int postId);
        void LikePost(int postId, int? userId);
        void LikeComment(int commentId, int? userId);
        int Create(string title, string content, string tags, int userId);
        void Update(PostEditViewModel post);
        void Delete(int postId);
    }
}