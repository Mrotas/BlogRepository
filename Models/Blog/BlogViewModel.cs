using BlogRepository.Models.Post;
using System.Collections.Generic;

namespace BlogRepository.Models.Blog
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public List<PostViewModel> Posts { get; set; }
    }
}
