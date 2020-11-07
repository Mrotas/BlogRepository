using BlogRepository.Models;

namespace BlogRepository.Domain.Interfaces
{
    public interface IBlogService
    {
        BlogViewModel GetBlogViewModelByBlogId(int blogId);
    }
}