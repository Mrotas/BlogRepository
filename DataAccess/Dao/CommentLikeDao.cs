using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BlogRepository.DataAccess.Dao
{
    public class CommentLikeDao : MongoDbClient, ICommentLikeDao
    {
        private readonly IMongoCollection<CommentLike> _commentLikeCollention;
        public CommentLikeDao(IMongoClient mongoClient) : base(mongoClient)
        {
            _commentLikeCollention = Database.GetCollection<CommentLike>("CommentLikes");
        }

        public List<CommentLike> GetByCommentId(int commentId)
        {
            FilterDefinition<CommentLike> filter = Builders<CommentLike>.Filter.Eq("CommentId", commentId);
            List<CommentLike> commentLikes = _commentLikeCollention.Find(filter).ToList();
            return commentLikes;
        }
    }
}
