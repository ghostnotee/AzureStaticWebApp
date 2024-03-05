namespace Application.Common.Interfaces.Persistence;

public interface IDbContext
{
    Task SaveAsync(CancellationToken cancellationToken = default);
}