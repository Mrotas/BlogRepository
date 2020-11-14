using BlogRepository.Models.Blog;

namespace BlogRepository.Domain.Interfaces
{
    public interface IBlogService
    {
        BlogViewModel GetBlogViewModelByBlogId(int blogId);
        BlogViewModel GetBlogViewModelByUserId(int userId);
        void Update(BlogViewModel blogViewModel);
    }
}