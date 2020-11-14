using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using BlogRepository.Models.Blog;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BlogRepository.DataAccess.Dao
{
    public class BlogDao : MongoDbClient, IBlogDao
    {
        private readonly IMongoCollection<Blog> _blogCollection;
        public BlogDao(IMongoClient mongoClient) : base(mongoClient)
        {
            _blogCollection = Database.GetCollection<Blog>("Blogs");
        }

        public List<Blog> GetRecent()
        {
            List<Blog> blogs = _blogCollection.Find(new BsonDocument()).ToList().OrderByDescending(x => x.Created).Take(5).ToList();
            return blogs;
        }

        public Blog GetById(int id)
        {
            FilterDefinition<Blog> filter = Builders<Blog>.Filter.Eq("Id", id);
            Blog blog = _blogCollection.Find(filter).FirstOrDefault();
            return blog;
        }

        public Blog GetByUserId(int userId)
        {
            FilterDefinition<Blog> filter = Builders<Blog>.Filter.Eq("UserId", userId);
            Blog blog = _blogCollection.Find(filter).FirstOrDefault();
            return blog;
        }

        public void Update(BlogViewModel blogViewModel)
        {
            FilterDefinition<Blog> filter = Builders<Blog>.Filter.Eq("Id", blogViewModel.Id);
            UpdateDefinition<Blog> update = Builders<Blog>.Update.Set("Name", blogViewModel.Name)
                                                                 .Set("Description", blogViewModel.Description);
            _blogCollection.UpdateOne(filter, update);
        }
    }
}
