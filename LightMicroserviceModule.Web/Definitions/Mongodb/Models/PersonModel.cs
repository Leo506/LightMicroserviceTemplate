using LightMicroserviceModule.Infrastructure.Mongodb;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LightMicroserviceModule.Definitions.Mongodb.Models;

public class PersonModel : IMongoModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("first_name")] public string FirstName { get; set; } = null!;

    [BsonElement("last_name")] public string LastName { get; set; } = null!;
    
    [BsonElement("skills")]
    [BsonIgnoreIfNull]
    public string[] Skills { get; set; }

}