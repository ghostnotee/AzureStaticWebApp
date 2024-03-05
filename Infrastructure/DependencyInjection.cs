using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Users;
using Domain.Users.Entities;
using Infrastructure.Authentication;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, AppSettings appSettings)
    {
        serviceCollection.AddPersistance(appSettings);
        serviceCollection.AddAuth();
        return serviceCollection;
    }

    private static void AddPersistance(this IServiceCollection serviceCollection, AppSettings appSettings)
    {
        serviceCollection.AddDbContext<SqlContext>(builder => builder.UseSqlServer(appSettings.SqlServerConnectionStrings));
        serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped(typeof(ISqlRepository<>), typeof(SqlRepository<>));
    }

    private static void AddAuth(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddIdentityCore<User>(options => options.User.RequireUniqueEmail = true).AddRoles<Role>()
            .AddEntityFrameworkStores<SqlContext>();
        serviceCollection.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    }
}