using System.Collections;
using LightMicroserviceModule.DbBase;
using LightMicroserviceModule.Definitions.Mongodb.Models;
using MongoDB.Driver;

namespace LightMicroserviceModule.Definitions.Mongodb.Context;

public class MongodbContext<T> : IMongoDbContext<T>
{
    private readonly IMongoCollection<T> _collection;

    public MongodbContext(MongodbSettings settings, IMongoClient client) => 
        _collection = client.GetDatabase(settings.DbName).GetCollection<T>(settings.CollectionName);

    public IMongoCollection<T> GetCollection() => _collection;

    public IEnumerator<T> GetEnumerator() => _collection.AsQueryable().AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}