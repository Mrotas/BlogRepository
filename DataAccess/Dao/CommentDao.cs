using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using MongoDB.Driver;
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

        public List<Comment> GetByPostId(int postId)
        {
            FilterDefinition<Comment> filter = Builders<Comment>.Filter.Eq("PostId", postId);
            List<Comment> comments = _commentCollention.Find(filter).ToList();
            return comments;
        }
    }
}
