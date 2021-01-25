using BlogRepository.DataAccess.Collection;
using BlogRepository.DataAccess.Dao.Interfaces;
using BlogRepository.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogRepository.DataAccess.Dao
{
    public class UserDao : MongoDbClient, IUserDao
    {
        private readonly IMongoCollection<User> _userCollection;
        public UserDao(IMongoClient mongoClient) : base(mongoClient)
        {
            _userCollection = Database.GetCollection<User>("Users");
        }

        public List<User> GetUsers()
        {
            List<User> users = _userCollection.Find(new BsonDocument()).ToList();
            return users;
        }

        public User GetById(int id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
            User user = _userCollection.Find(filter).FirstOrDefault();
            return user;
        }

        public User GetByUsername(string username)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("Username", username);
            User user = _userCollection.Find(filter).FirstOrDefault();
            return user;
        }

        public int Create(RegisterModel registerModel)
        {
            User existingUser = GetByUsername(registerModel.Username);
            if (existingUser != null)
            {
                throw new Exception($"Login {registerModel.Username} jest już zajęty!");
            }

            List<User> users = GetUsers();
            int lastId = users.Any() ? users.Max(x => x.Id) : 0;

            var user = new User(++lastId, registerModel);
            _userCollection.InsertOne(user);

            return user.Id;
        }
    }
}
