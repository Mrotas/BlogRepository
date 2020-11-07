using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BlogRepository.DataAccess.Collection
{
    public class Blog
    {
        [BsonId]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public int Views { get; set; }
    }
}
