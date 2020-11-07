using BlogRepository.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace BlogRepository.DataAccess.Collection
{
    public class User
    {
        public User(int id, RegisterModel registerModel)
        {
            Id = id;
            Name = registerModel.Name;
            LastName = registerModel.LastName;
            Username = registerModel.Username;
            Password = registerModel.Password;
        }

        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
