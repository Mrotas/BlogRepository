using BlogRepository.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogRepository.Models.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public List<string> TagList => Tags.Split(new char[] { ',', ';' }).Where(tag => !String.IsNullOrWhiteSpace(tag)).Select(x => x.Trim()).ToList();
        public DateTime Created { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}