using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using BlogRepository.Models.Blog;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
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

        public List<Blog> GetBlogs()
        {
            List<Blog> blogs = _blogCollection.Find(new BsonDocument()).ToList().ToList();
            return blogs;
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

        public int Insert(int userId)
        {
            List<Blog> blogs = GetBlogs();
            int lastId = blogs.Any() ? blogs.Max(x => x.Id) : 0;

            var blog = new Blog
            {
                Id = ++lastId,
                UserId = userId,
                Name = String.Empty,
                Description = String.Empty,
                Views = 0,
                Created = DateTime.Now
            };
            _blogCollection.InsertOne(blog);

            return blog.Id;
        }

        public void Update(BlogEditViewModel blog)
        {
            FilterDefinition<Blog> filter = Builders<Blog>.Filter.Eq("Id", blog.Id);
            UpdateDefinition<Blog> update = Builders<Blog>.Update.Set("Name", blog.Name)
                                                                 .Set("Description", blog.Description);
            _blogCollection.UpdateOne(filter, update);
        }

        public void UpdateViews(int blogId, int viewsCount)
        {
            FilterDefinition<Blog> filter = Builders<Blog>.Filter.Eq("Id", blogId);
            UpdateDefinition<Blog> update = Builders<Blog>.Update.Set("Views", viewsCount);
            _blogCollection.UpdateOne(filter, update);
        }

        public void Delete(int id)
        {
            FilterDefinition<Blog> filter = Builders<Blog>.Filter.Eq("Id", id);
            _blogCollection.DeleteOne(filter);
        }
    }
}
