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

        public BlogService(IBlogDao blogDao, IPostService postService, IUserDao userDao)
        {
            _blogDao = blogDao;
            _postService = postService;
            _userDao = userDao;
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

        public void Update(BlogViewModel blogViewModel)
        {
            _blogDao.Update(blogViewModel);
        }
    }
}
