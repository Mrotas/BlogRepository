using MongoDB.Bson.Serialization.Attributes;

namespace BlogRepository.DataAccess.Collection
{
    public class CommentLike
    {
        [BsonId]
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int? UserId { get; set; }
    }
}
