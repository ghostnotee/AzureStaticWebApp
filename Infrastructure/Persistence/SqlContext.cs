using System.Reflection;
using Domain.Users;
using Domain.Users.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence;

public class SqlContext(DbContextOptions<SqlContext> options) : IdentityDbContext<User, Role, Guid>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}

public class SqlContextFactory : IDesignTimeDbContextFactory<SqlContext>
{
    public SqlContext CreateDbContext(string[] args)
    {
        return new SqlContext(SetupLocal());
    }

    private static DbContextOptions<SqlContext> SetupLocal()
    {
        var optionsBuilder = new DbContextOptionsBuilder<SqlContext>();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Pcychologiz;User=sa;Password=yourStrong(!)Password;TrustServerCertificate=True");
        optionsBuilder.EnableSensitiveDataLogging();
        return optionsBuilder.Options;
    }
}