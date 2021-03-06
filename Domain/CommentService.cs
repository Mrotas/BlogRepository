﻿using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using BlogRepository.Domain.Interfaces;
using BlogRepository.Models.Comment;
using System.Collections.Generic;

namespace BlogRepository.Domain
{
    public class CommentService : ICommentService
    {
        private readonly ICommentDao _commentDao;
        private readonly ICommentLikeDao _commentLikeDao;
        private readonly IUserDao _userDao;

        public CommentService(ICommentDao commentDao, ICommentLikeDao commentLikeDao, IUserDao userDao)
        {
            _commentDao = commentDao;
            _commentLikeDao = commentLikeDao;
            _userDao = userDao;
        }

        public List<CommentViewModel> GetCommentViewModelsByPostId(int postId)
        {
            List<Comment> comments = _commentDao.GetByPostId(postId);

            var commentViewModels = new List<CommentViewModel>();
            foreach (Comment comment in comments)
            {
                CommentViewModel commentViewModel = GetCommentViewModel(comment);
                commentViewModels.Add(commentViewModel);
            }

            return commentViewModels;
        }

        private CommentViewModel GetCommentViewModel(Comment comment)
        {
            string username = "Gość";
            int? userId = null;
            if (comment.UserId.HasValue)
            {
                User user = _userDao.GetById(comment.UserId.Value);
                username = user.Username;
                userId = user.Id;
            }
            
            List<CommentLike> likes = _commentLikeDao.GetByCommentId(comment.Id);

            var commentViewModel = new CommentViewModel
            {
                Id = comment.Id,
                UserId = userId,
                Username = username,
                Content = comment.Content,
                Created = comment.Created,
                Likes = likes.Count
            };

            return commentViewModel;
        }

        public void Delete(int commentId)
        {
            List<CommentLike> commentLikes = _commentLikeDao.GetByCommentId(commentId);
            foreach (CommentLike commentLike in commentLikes)
            {
                _commentLikeDao.Delete(commentLike.Id);
            }

            _commentDao.Delete(commentId);
        }
    }
}
