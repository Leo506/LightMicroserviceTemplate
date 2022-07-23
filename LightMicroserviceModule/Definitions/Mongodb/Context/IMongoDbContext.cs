using LightMicroserviceModule.DbBase;
using MongoDB.Driver;

namespace LightMicroserviceModule.Definitions.Mongodb.Context;

/// <summary>
/// Custom context for mongo database
/// </summary>
/// <typeparam name="T">Model type</typeparam>
public interface IMongoDbContext<T> : IDbContext<T>
{
    IMongoCollection<T> GetCollection();
}