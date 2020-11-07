using System.Collections.Generic;

namespace BlogRepository.Models
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
