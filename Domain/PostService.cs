using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using BlogRepository.Domain.Interfaces;
using BlogRepository.Models;
using System.Collections.Generic;

namespace BlogRepository.Domain
{
    public class PostService : IPostService
    {
        private readonly IPostDao _postDao;
        private readonly ICommentService _commentService;
        private readonly IPostLikeDao _postLikeDao;

        public PostService(IPostDao postDao, ICommentService commentService, IPostLikeDao postLikeDao)
        {
            _postDao = postDao;
            _commentService = commentService;
            _postLikeDao = postLikeDao;
        }

        public PostViewModel GetPostViewModelByPostId(int postId)
        {
            Post post = _postDao.GetById(postId);
            PostViewModel postViewModel = GetPostViewModel(post);
            return postViewModel;
        }

        public List<PostViewModel> GetPostViewModelsByBlogId(int blogId)
        {
            List<Post> posts = _postDao.GetByBlogId(blogId);

            var postViewModels = new List<PostViewModel>();
            foreach (Post post in posts)
            {
                PostViewModel postViewModel = GetPostViewModel(post);
                postViewModels.Add(postViewModel);
            }

            return postViewModels;
        }

        private PostViewModel GetPostViewModel(Post post)
        {
            List<CommentViewModel> comments = _commentService.GetCommentViewModelsByPostId(post.Id);
            List<PostLike> likes = _postLikeDao.GetByPostId(post.Id);

            var postViewModel = new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Created = post.Created,
                Views = post.Views,
                Likes = likes.Count,
                Comments = comments
            };

            return postViewModel;
        }
    }
}
