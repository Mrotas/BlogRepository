using BlogRepository.Models.Blog;

namespace BlogRepository.Domain.Interfaces
{
    public interface IBlogService
    {
        BlogViewModel GetBlogViewModelByBlogId(int blogId);
        BlogViewModel GetBlogViewModelByUserId(int userId);
        int Create(int userId);
        void Update(BlogEditViewModel blog);
        void UpdateViewsCount(int blogId);
        bool Delete(int userId, string blogName);
    }
}