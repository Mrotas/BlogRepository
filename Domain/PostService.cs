﻿using BlogRepository.DataAccess.Collection;
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
        private readonly IUserService _userService;
        private readonly ICommentDao _commentDao;

        public PostService(IPostDao postDao, ICommentService commentService, IPostLikeDao postLikeDao, IUserService userService, ICommentDao commentDao)
        {
            _postDao = postDao;
            _commentService = commentService;
            _postLikeDao = postLikeDao;
            _userService = userService;
            _commentDao = commentDao;
        }

        public PostViewModel GetPostViewModelByPostId(int postId)
        {
            Post post = _postDao.GetById(postId);
            User user = _userService.GetByPostId(postId);

            PostViewModel postViewModel = GetPostViewModel(post, user.Username);
            return postViewModel;
        }

        public List<PostViewModel> GetPostViewModelsByBlogId(int blogId)
        {
            List<Post> posts = _postDao.GetByBlogId(blogId);
            User user = _userService.GetByBlogId(blogId);

            var postViewModels = new List<PostViewModel>();
            foreach (Post post in posts)
            {
                PostViewModel postViewModel = GetPostViewModel(post, user.Username);
                postViewModels.Add(postViewModel);
            }

            return postViewModels;
        }

        public void PostComment(int postId, string content, int? userId)
        {
            _commentDao.Insert(postId, content, userId);
        }

        private PostViewModel GetPostViewModel(Post post, string username)
        {
            List<CommentViewModel> comments = _commentService.GetCommentViewModelsByPostId(post.Id);
            List<PostLike> likes = _postLikeDao.GetByPostId(post.Id);

            var postViewModel = new PostViewModel
            {
                Id = post.Id,
                Username = username,
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
