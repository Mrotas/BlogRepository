using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using BlogRepository.Domain.Interfaces;

namespace BlogRepository.Domain
{
    public class UserService : IUserService
    {
        private readonly IUserDao _userDao;
        private readonly IBlogDao _blogDao;
        private readonly IPostDao _postDao;

        public UserService(IUserDao userDao, IBlogDao blogDao, IPostDao postDao)
        {
            _userDao = userDao;
            _blogDao = blogDao;
            _postDao = postDao;
        }

        public User GetByBlogId(int blogId)
        {
            Blog blog = _blogDao.GetById(blogId);
            User user = _userDao.GetById(blog.UserId);
            return user;
        }

        public User GetByPostId(int postId)
        {
            Post post = _postDao.GetById(postId);
            return GetByBlogId(post.BlogId);
        }
    }
}
