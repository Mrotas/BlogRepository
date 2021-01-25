using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using MongoDB.Bson;
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

        private List<PostLike> GetPostsLikes()
        {
            List<PostLike> postsLikes = _postLikeCollention.Find(new BsonDocument()).ToList();
            return postsLikes;
        }

        public List<PostLike> GetByPostId(int postId)
        {
            FilterDefinition<PostLike> filter = Builders<PostLike>.Filter.Eq("PostId", postId);
            List<PostLike> postLikes = _postLikeCollention.Find(filter).ToList();
            return postLikes;
        }

        public void Insert(int postId, int? userId)
        {
            List<PostLike> postLikes = GetPostsLikes();
            int lastId = postLikes.Any() ? postLikes.Max(x => x.Id) : 0;

            var postLike = new PostLike
            {
                Id = ++lastId,
                PostId = postId,
                UserId = userId
            };
            _postLikeCollention.InsertOne(postLike);
        }

        public void Delete(int postLikeId)
        {
            FilterDefinition<PostLike> filter = Builders<PostLike>.Filter.Eq("Id", postLikeId);
            _postLikeCollention.DeleteOne(filter);
        }
    }
}
