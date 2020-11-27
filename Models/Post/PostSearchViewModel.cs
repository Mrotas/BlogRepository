using System.Collections.Generic;
using PostDocument = BlogRepository.DataAccess.Collection.Post;

namespace BlogRepository.Models.Post
{
    public class PostSearchViewModel
    {
        public string SearchString { get; set; }
        public List<PostDocument> Posts { get; set; }
    }
}
