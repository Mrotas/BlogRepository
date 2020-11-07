using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using BlogRepository.Domain.Interfaces;
using BlogRepository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogRepository.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogDao _blogDao;
        private readonly IBlogService _blogService;

        public BlogController(IBlogDao blogDao, IBlogService blogService)
        {
            _blogDao = blogDao;
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            List<Blog> recentBlogs = _blogDao.GetRecent();
            return View(recentBlogs);
        }

        public IActionResult MyBlog()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            Blog blog = _blogDao.GetByUserId(userId.Value);
            return View(blog);
        }

        public IActionResult ViewBlog(int blogId)
        {
            BlogViewModel blogViewModel = _blogService.GetBlogViewModelByBlogId(blogId);
            return View(blogViewModel);
        }
    }
}