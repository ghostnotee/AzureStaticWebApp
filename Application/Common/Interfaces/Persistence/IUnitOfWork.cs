namespace Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    IEnumerable<TEntity> GetChanges<TEntity>() where TEntity : class;
    IEnumerable<dynamic> GetChanges();
}