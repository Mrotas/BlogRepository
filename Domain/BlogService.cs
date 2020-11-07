using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using BlogRepository.Domain.Interfaces;
using BlogRepository.Models;
using System.Collections.Generic;

namespace BlogRepository.Domain
{
    public class BlogService : IBlogService
    {
        private readonly IBlogDao _blogDao;
        private readonly IPostService _postService;

        public BlogService(IBlogDao blogDao, IPostService postService)
        {
            _blogDao = blogDao;
            _postService = postService;
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
    }
}
