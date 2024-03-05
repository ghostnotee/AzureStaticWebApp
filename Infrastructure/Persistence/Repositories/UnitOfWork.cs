using Application.Common.Interfaces.Persistence;

namespace Infrastructure.Persistence.Repositories;

public class UnitOfWork(SqlContext context) : IUnitOfWork
{
    public IEnumerable<TEntity> GetChanges<TEntity>() where TEntity : class => context.ChangeTracker.Entries<TEntity>().Select(entity => entity.Entity);
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await context.SaveChangesAsync(cancellationToken);
    public IEnumerable<dynamic> GetChanges() => context.ChangeTracker.Entries().Select(entity => entity.Entity);
}