using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
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

        private List<CommentLike> GetCommentsLikes()
        {
            List<CommentLike> commentsLikes = _commentLikeCollention.Find(new BsonDocument()).ToList();
            return commentsLikes;
        }

        public List<CommentLike> GetByCommentId(int commentId)
        {
            FilterDefinition<CommentLike> filter = Builders<CommentLike>.Filter.Eq("CommentId", commentId);
            List<CommentLike> commentLikes = _commentLikeCollention.Find(filter).ToList();
            return commentLikes;
        }

        public void Insert(int commentId, int? userId)
        {
            List<CommentLike> commentLikes = GetCommentsLikes();
            int lastId = commentLikes.Max(x => x.Id);

            var commentLike = new CommentLike
            {
                Id = ++lastId,
                CommentId = commentId,
                UserId = userId
            };
            _commentLikeCollention.InsertOne(commentLike);
        }
    }
}
