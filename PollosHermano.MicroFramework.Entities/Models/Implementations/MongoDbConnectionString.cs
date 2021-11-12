using PollosHermano.MicroFramework.Entities.Models.Interfaces;

namespace PollosHermano.MicroFramework.Entities.Models.Implementations
{
    public class MongoDbConnectionString : IMongoDbConnectionString
    {
        public string ConnectionString { get; set; }
    }
}
