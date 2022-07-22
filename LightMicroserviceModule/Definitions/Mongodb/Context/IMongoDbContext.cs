using LightMicroserviceModule.DbBase;
using MongoDB.Driver;

namespace LightMicroserviceModule.Definitions.Mongodb.Context;

public interface IMongoDbContext<T> : IDbContext<T>
{
    IMongoCollection<T> GetCollection();
}