using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogRepository.DataAccess.Dao
{
    public class CommentDao : MongoDbClient, ICommentDao
    {
        private readonly IMongoCollection<Comment> _commentCollention;
        public CommentDao(IMongoClient mongoClient) : base(mongoClient)
        {
            _commentCollention = Database.GetCollection<Comment>("Comments");
        }

        public List<Comment> GetComments()
        {
            List<Comment> comments = _commentCollention.Find(new BsonDocument()).ToList();
            return comments;
        }

        public List<Comment> GetByPostId(int postId)
        {
            FilterDefinition<Comment> filter = Builders<Comment>.Filter.Eq("PostId", postId);
            List<Comment> comments = _commentCollention.Find(filter).ToList();
            return comments;
        }

        public void Insert(int postId, string content, int? userId)
        {
            List<Comment> comments = GetComments();
            int lastId = comments.Any() ? comments.Max(x => x.Id) : 0;

            var comment = new Comment
            {
                Id = ++lastId,
                PostId = postId,
                UserId = userId,
                Content = content,
                Created = DateTime.Now
            };
            _commentCollention.InsertOne(comment);
        }

        public void Delete(int commentId)
        {
            FilterDefinition<Comment> filter = Builders<Comment>.Filter.Eq("Id", commentId);
            _commentCollention.DeleteOne(filter);
        }
    }
}
