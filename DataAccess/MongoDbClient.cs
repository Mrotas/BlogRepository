using MongoDB.Driver;

namespace BlogRepository.DataAccess
{
    public abstract class MongoDbClient
    {
        protected IMongoDatabase Database { get; set; }
        protected MongoDbClient(IMongoClient mongoClient)
        {
            Database = mongoClient.GetDatabase("Blogs");
        }
    }
}
