using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BlogRepository.DataAccess.Dao
{
    public class PostDao : MongoDbClient, IPostDao
    {
        private readonly IMongoCollection<Post> _postCollection;
        public PostDao(IMongoClient mongoClient) : base(mongoClient)
        {
            _postCollection = Database.GetCollection<Post>("Posts");
        }

        public List<Post> GetRecent()
        {
            List<Post> posts = _postCollection.Find(new BsonDocument()).ToList().OrderByDescending(x => x.Created).Take(5).ToList();
            return posts;
        }

        public Post GetById(int id)
        {
            FilterDefinition<Post> filter = Builders<Post>.Filter.Eq("Id", id);
            Post post = _postCollection.Find(filter).First();
            return post;
        }

        public List<Post> GetByBlogId(int blogId)
        {
            FilterDefinition<Post> filter = Builders<Post>.Filter.Eq("BlogId", blogId);
            List<Post> posts = _postCollection.Find(filter).ToList();
            return posts;
        }

        public void UpdateViews(int postId, int viewsCount)
        {
            FilterDefinition<Post> filter = Builders<Post>.Filter.Eq("Id", postId);
            UpdateDefinition<Post> update = Builders<Post>.Update.Set("Views", viewsCount);
            _postCollection.UpdateOne(filter, update);
        }
    }
}
