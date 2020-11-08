using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using BlogRepository.Domain.Interfaces;
using BlogRepository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogRepository.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostDao _postDao;
        private readonly IPostService _postService;

        public PostController(IPostDao postDao, IPostService postService)
        {
            _postDao = postDao;
            _postService = postService;
        }

        public IActionResult Index()
        {
            List<Post> recentPosts = _postDao.GetRecent();
            return View(recentPosts);
        }

        public IActionResult ViewPost(int postId)
        {
            PostViewModel postViewModel = _postService.GetPostViewModelByPostId(postId);
            Task.Run(() => _postService.UpdateViewsCount(postId));
            return View(postViewModel);
        }

        [HttpPost]
        public IActionResult PostComment(int postId, string content)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            _postService.PostComment(postId, content, userId);

            return RedirectToAction("ViewPost", "Post", routeValues: new { postId });
        }

        [HttpPost]
        public IActionResult LikePost(int postId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            _postService.LikePost(postId, userId);

            return RedirectToAction("ViewPost", "Post", routeValues: new { postId });
        }

        [HttpPost]
        public IActionResult LikeComment(int postId, int commentId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            _postService.LikeComment(commentId, userId);

            return RedirectToAction("ViewPost", "Post", routeValues: new { postId });
        }
    }
}