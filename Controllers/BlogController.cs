using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using BlogRepository.Domain.Interfaces;
using BlogRepository.Models.Blog;
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
            BlogViewModel blogViewModel = _blogService.GetBlogViewModelByUserId(userId.Value);
            return View(blogViewModel);
        }

        public IActionResult ViewBlog(int blogId)
        {
            BlogViewModel blogViewModel = _blogService.GetBlogViewModelByBlogId(blogId);
            return View(blogViewModel);
        }

        [HttpPost]
        public IActionResult Update(BlogViewModel blogViewModel)
        {
            _blogService.Update(blogViewModel);
            return RedirectToAction("ViewBlog", routeValues: new { blogId = blogViewModel.Id });
        }
    }
}