﻿using System;

namespace BlogRepository.Models.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public int Likes { get; set; }
    }
}