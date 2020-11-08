using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BlogRepository.DataAccess.Collection
{
    public class Comment
    {
        [BsonId]
        public int Id { get; set; }
        public int PostId { get; set; }
        public int? UserId { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}
