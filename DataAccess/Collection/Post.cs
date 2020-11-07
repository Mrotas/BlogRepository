using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BlogRepository.DataAccess.Collection
{
    public class Post
    {
        [BsonId]
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public DateTime Created { get; set; }
        public int Views { get; set; }
    }
}
