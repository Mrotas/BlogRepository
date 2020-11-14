using BlogRepository.Models.Comment;
using System;
using System.Collections.Generic;

namespace BlogRepository.Models.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}