using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using BlogRepository.Domain.Interfaces;
using BlogRepository.Models.Blog;
using BlogRepository.Models.Post;
using System.Collections.Generic;

namespace BlogRepository.Domain
{
    public class BlogService : IBlogService
    {
        private readonly IBlogDao _blogDao;
        private readonly IPostService _postService;
        private readonly IUserDao _userDao;
        private readonly IPostDao _postDao;

        public BlogService(IBlogDao blogDao, IPostService postService, IUserDao userDao, IPostDao postDao)
        {
            _blogDao = blogDao;
            _postService = postService;
            _userDao = userDao;
            _postDao = postDao;
        }

        public BlogViewModel GetBlogViewModelByBlogId(int blogId)
        {
            Blog blog = _blogDao.GetById(blogId);
            List<PostViewModel> posts = _postService.GetPostViewModelsByBlogId(blogId);

            var blogViewModel = new BlogViewModel
            {
                Id = blog.Id,
                Name = blog.Name,
                Description = blog.Description,
                Views = blog.Views,
                Posts = posts
            };

            return blogViewModel;
        }

        public BlogViewModel GetBlogViewModelByUserId(int userId)
        {
            User user = _userDao.GetById(userId);
            Blog blog = _blogDao.GetByUserId(user.Id);
            if (blog == null)
            {
                return null;
            }

            List<PostViewModel> posts = _postService.GetPostViewModelsByBlogId(blog.Id);

            var blogViewModel = new BlogViewModel
            {
                Id = blog.Id,
                Name = blog.Name,
                Description = blog.Description,
                Views = blog.Views,
                Posts = posts
            };

            return blogViewModel;
        }

        public int Create(int userId)
        {
            int blogId = _blogDao.Insert(userId);
            return blogId;
        }

        public void Update(BlogEditViewModel blog)
        {
            _blogDao.Update(blog);
        }

        public void UpdateViewsCount(int blogId)
        {
            Blog blog = _blogDao.GetById(blogId);
            int currentViewsCount = blog.Views;
            _blogDao.UpdateViews(blogId, ++currentViewsCount);
        }

        public bool Delete(int userId, string blogName)
        {
            Blog blog = _blogDao.GetByUserId(userId);
            if (blog.Name != blogName)
            {
                return false;
            }

            List<Post> posts = _postDao.GetByBlogId(blog.Id);
            foreach (Post post in posts)
            {
                _postService.Delete(post.Id);
            }

            _blogDao.Delete(blog.Id);

            return true;
        }
    }
}
