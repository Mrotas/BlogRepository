using System.Collections.Generic;
using PostDocument = BlogRepository.DataAccess.Collection.Post;

namespace BlogRepository.Models.Post
{
    public class PostTagViewModel
    {
        public string Tag { get; set; }
        public List<PostDocument> Posts { get; set; }
    }
}
