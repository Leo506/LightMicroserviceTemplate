using AutoMapper;
using LightMicroserviceModule.Definitions.Base;
using LightMicroserviceModule.Definitions.Mongodb.Models;
using LightMicroserviceModule.Definitions.Mongodb.ViewModels;
using LightMicroserviceModule.Domain.DbBase;
using LightMicroserviceModule.Infrastructure.Mongodb;
using MongoDB.Driver;

namespace LightMicroserviceModule.Definitions.Mongodb;

public class MongoDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("mongo");

        services.AddTransient<IMongoClient>(provider => new MongoClient(connectionString));

        services.AddSingleton<IRepository<PersonModel>>(provider =>
        {
            var client = provider.GetRequiredService<IMongoClient>();
            var settings = new MongodbSettings()
            {
                ConnectionString = connectionString,
                CollectionName = configuration["Person:Collection"],
                DbName = configuration["Person:Database"]
            };
            var logger = provider.GetRequiredService<ILogger<MongoRepository<PersonModel>>>();
            return new MongoRepository<PersonModel>(client, settings, logger);
        });
    }

    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.MapGet("/get/all", async (IRepository<PersonModel> repository, HttpContext context) =>
        {
            var result = await repository.GetAllAsync();
            if (!result.Ok)
            {
                await context.Response.WriteAsync(result.Error.Message);
                return;
            }

            await context.Response.WriteAsJsonAsync(result.Result);
        });

        app.MapPost("/add/{lastName}&{firstName}",
            async (IRepository<PersonModel> repository, string lastName, string firstName, HttpContext context) =>
            {
                var result = await repository.AddAsync(new PersonModel()
                {
                    FirstName = firstName,
                    LastName = lastName
                });

                if (!result.Ok)
                {
                    await context.Response.WriteAsync("Can't add new record");
                    return;
                }

                await context.Response.WriteAsync(result.Result);
            });

        app.MapPost("/update/{oldFirstName}/{newName}", async (IRepository<PersonModel> repository, string oldFirstName,
            string newName, HttpContext context) =>
        {
            var toUpdate = await repository.Get(model => model.FirstName == oldFirstName);
            if (!toUpdate.Ok)
            {
                await context.Response.WriteAsync("No such object");
                return;
            }

            var model = toUpdate.Result;
            model.FirstName = newName;
            var updateResult = await repository.UpdateAsync(model);
            if (!updateResult.Ok)
            {
                await context.Response.WriteAsync("Failed to update");
                return;
            }

            await context.Response.WriteAsync("Successfully updated");
        });

        app.MapPost(
            "/delete/{lastName}" ,async (IRepository<PersonModel> repository, string lastName, HttpContext context) =>
            {
                var toDelete = await repository.Get(model => model.LastName == lastName);
                if (!toDelete.Ok)
                {
                    await context.Response.WriteAsync("Can't find item to delete");
                    return;
                }

                var deletingResult = await repository.DeleteAsync(toDelete.Result);
                if (!deletingResult.Ok)
                {
                    await context.Response.WriteAsync(deletingResult.Error.Message);
                    return;
                }

                await context.Response.WriteAsync("Successfully deleted");
            });
    }
}