using MongoDB.Bson.Serialization.Attributes;

namespace BlogRepository.DataAccess.Collection
{
    public class PostLike
    {
        [BsonId]
        public int Id { get; set; }
        public int PostId { get; set; }
        public int? UserId { get; set; }
    }
}
