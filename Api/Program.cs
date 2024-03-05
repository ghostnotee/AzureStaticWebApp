using System.Text.Json;
using Api.Middlewares;
using Application;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Settings;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var host = new HostBuilder()
    .ConfigureAppConfiguration(builder =>
    {
        if (Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") != "Development") return;
        builder.AddJsonFile("local.settings.json", false, true);
        builder.Build();
    })
    .ConfigureFunctionsWorkerDefaults(builder =>
    {
        builder.UseMiddleware<ExceptionMiddleware>().UseMiddleware<AuthMiddleware>();
        builder.Services.Configure<JsonSerializerOptions>(options => options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
    })
    .ConfigureServices((context, collection) =>
    {
        var appSettings = SetupConfig(context, collection);
        collection.AddInfrastructure(appSettings).AddApplication();

        var dbContext = collection.BuildServiceProvider().GetService<SqlContext>();
        dbContext!.Database.MigrateAsync();

        collection.AddApplicationInsightsTelemetryWorkerService();
        collection.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();

AppSettings SetupConfig(HostBuilderContext builder, IServiceCollection services)
{
    AppSettings appSettings;
    if (Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") != "Development")
    {
        appSettings = new AppSettings(false);
        services.AddSingleton(appSettings);
        return appSettings;
    }

    services.AddOptions();
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    appSettings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>()!.Value;
    services.AddSingleton(appSettings);
    return appSettings;
}