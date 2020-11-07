using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BlogRepository.DataAccess.Dao
{
    public class PostLikeDao : MongoDbClient, IPostLikeDao
    {
        private readonly IMongoCollection<PostLike> _postLikeCollention;
        public PostLikeDao(IMongoClient mongoClient) : base(mongoClient)
        {
            _postLikeCollention = Database.GetCollection<PostLike>("PostLikes");
        }

        public List<PostLike> GetByPostId(int postId)
        {
            FilterDefinition<PostLike> filter = Builders<PostLike>.Filter.Eq("PostId", postId);
            List<PostLike> postLikes = _postLikeCollention.Find(filter).ToList();
            return postLikes;
        }
    }
}
