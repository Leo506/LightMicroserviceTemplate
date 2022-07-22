using AutoMapper;
using LightMicroserviceModule.Definitions.Base;
using LightMicroserviceModule.Definitions.Mongodb.Context;
using LightMicroserviceModule.Definitions.Mongodb.Models;
using LightMicroserviceModule.Definitions.Mongodb.ViewModels;
using MongoDB.Driver;

namespace LightMicroserviceModule.Definitions.Mongodb;

public class MongoDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("mongo");

        services.AddTransient<IMongoClient>(provider => new MongoClient(connectionString));

        services.AddSingleton<IMongoDbContext<PersonModel>>(provider => new MongodbContext<PersonModel>(
            new MongodbSettings()
            {
                ConnectionString = connectionString,
                CollectionName = configuration["Person:Collection"],
                DbName = configuration["Person:Database"]
            }, provider.GetRequiredService<IMongoClient>()));

        services.AddSingleton<MongodbWorker<PersonModel>>();
    }
}