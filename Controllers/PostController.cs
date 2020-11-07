using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using BlogRepository.Domain.Interfaces;
using BlogRepository.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            return View(postViewModel);
        }
    }
}