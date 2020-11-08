using BlogRepository.DataAccess.Collection;

namespace BlogRepository.Domain.Interfaces
{
    public interface IUserService
    {
        User GetByBlogId(int blogId);
        User GetByPostId(int postId);
    }
}
