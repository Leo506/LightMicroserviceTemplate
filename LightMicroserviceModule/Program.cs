using LightMicroserviceModule.Definitions.Base;

try
{
    // created builder
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDefinitions(builder, typeof(Program));

    // create application
    var app = builder.Build();
    app.UseDefinitions();

    // start application
    app.Run();

    return 0;
}
catch (Exception ex)
{
    return 1;
}

finally
{
    
}