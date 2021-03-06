﻿using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using BlogRepository.Domain.Interfaces;
using BlogRepository.Models.Comment;
using BlogRepository.Models.Post;
using System.Collections.Generic;
using System.Linq;

namespace BlogRepository.Domain
{
    public class PostService : IPostService
    {
        private readonly IPostDao _postDao;
        private readonly ICommentService _commentService;
        private readonly IPostLikeDao _postLikeDao;
        private readonly IUserService _userService;
        private readonly ICommentDao _commentDao;
        private readonly ICommentLikeDao _commentLikeDao;
        private readonly IBlogDao _blogDao;

        public PostService(IPostDao postDao, 
            ICommentService commentService, 
            IPostLikeDao postLikeDao, 
            IUserService userService, 
            ICommentDao commentDao, 
            ICommentLikeDao commentLikeDao,
            IBlogDao blogDao)
        {
            _postDao = postDao;
            _commentService = commentService;
            _postLikeDao = postLikeDao;
            _userService = userService;
            _commentDao = commentDao;
            _commentLikeDao = commentLikeDao;
            _blogDao = blogDao;
        }

        public PostViewModel GetPostViewModelByPostId(int postId)
        {
            Post post = _postDao.GetById(postId);
            User user = _userService.GetByPostId(postId);

            PostViewModel postViewModel = GetPostViewModel(post, user);
            return postViewModel;
        }

        public List<PostViewModel> GetPostViewModelsByBlogId(int blogId)
        {
            List<Post> posts = _postDao.GetByBlogId(blogId);
            User user = _userService.GetByBlogId(blogId);

            var postViewModels = new List<PostViewModel>();
            foreach (Post post in posts)
            {
                PostViewModel postViewModel = GetPostViewModel(post, user);
                postViewModels.Add(postViewModel);
            }

            return postViewModels;
        }

        public List<Post> GetByTag(string tag)
        {
            List<Post> posts = _postDao.GetPosts();
            List<Post> postsWithTag = posts.Where(x => x.Tags.Contains(tag)).ToList();
            return postsWithTag;
        }

        public List<Post> GetBySearchString(string searchString)
        {
            List<Post> posts = _postDao.GetPosts();
            List<Post> postsWithSearchString = posts.Where(x => x.Title.Contains(searchString) || x.Content.Contains(searchString) || x.Tags.Contains(searchString)).ToList();
            return postsWithSearchString;
        }

        public void PostComment(int postId, string content, int? userId)
        {
            _commentDao.Insert(postId, content, userId);
        }

        public void UpdateViewsCount(int postId)
        {
            Post post = _postDao.GetById(postId);
            int currentViewsCount = post.Views;
            _postDao.UpdateViews(postId, ++currentViewsCount);
        }

        public void LikePost(int postId, int? userId)
        {
            _postLikeDao.Insert(postId, userId);
        }

        public void LikeComment(int commentId, int? userId)
        {
            _commentLikeDao.Insert(commentId, userId);
        }

        public int Create(string title, string content, string tags, int userId)
        {
            Blog blog = _blogDao.GetByUserId(userId);
            int id = _postDao.Insert(title, content, tags, blog.Id);
            return id;
        }

        public void Update(PostEditViewModel post)
        {
            _postDao.Update(post);
        }

        public void Delete(int postId)
        {
            List<Comment> comments = _commentDao.GetByPostId(postId);
            foreach (Comment comment in comments)
            {
                _commentService.Delete(comment.Id);
            }

            List<PostLike> postLikes = _postLikeDao.GetByPostId(postId);
            foreach (PostLike postLike in postLikes)
            {
                _postLikeDao.Delete(postLike.Id);
            }

            _postDao.Delete(postId);
        }

        public void DeleteComment(int commentId)
        {
            _commentService.Delete(commentId);
        }

        private PostViewModel GetPostViewModel(Post post, User user)
        {
            List<CommentViewModel> comments = _commentService.GetCommentViewModelsByPostId(post.Id);
            List<PostLike> likes = _postLikeDao.GetByPostId(post.Id);

            var postViewModel = new PostViewModel
            {
                Id = post.Id,
                BlogId = post.BlogId,
                UserId = user.Id,
                Username = user.Username,
                Title = post.Title,
                Content = post.Content,
                Tags = post.Tags,
                Created = post.Created,
                Views = post.Views,
                Likes = likes.Count,
                Comments = comments
            };

            return postViewModel;
        }
    }
}
