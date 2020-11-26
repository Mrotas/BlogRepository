using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using BlogRepository.Domain.Interfaces;
using BlogRepository.Models.Blog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            if (blogViewModel == null)
            {
                return View("Create");
            }
            return View(blogViewModel);
        }

        public IActionResult ViewBlog(int blogId)
        {
            BlogViewModel blogViewModel = _blogService.GetBlogViewModelByBlogId(blogId);
            Task.Run(() => _blogService.UpdateViewsCount(blogId));
            return View(blogViewModel);
        }

        [HttpPost]
        public IActionResult Create()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            int blogId = _blogService.Create(userId.Value);
            return RedirectToAction("MyBlog", routeValues: new { blogId });
        }

        [HttpPost]
        public IActionResult Update(BlogEditViewModel blog)
        {
            _blogService.Update(blog);
            return RedirectToAction("ViewBlog", routeValues: new { blogId = blog.Id });
        }

        public IActionResult Delete()
        {
            return View("Delete");
        }

        [HttpPost]
        public IActionResult Delete(string blogName)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            bool deleted = _blogService.Delete(userId.Value, blogName);
            if (deleted)
            {
                return RedirectToAction("Index");                    
            }
            return RedirectToAction("Error", "Home", routeValues: new { errorMessage = "Wystąpił błąd podczas usuwania bloga. Upewnij się, że podano poprawną nazwę bloga." });
        }
    }
}