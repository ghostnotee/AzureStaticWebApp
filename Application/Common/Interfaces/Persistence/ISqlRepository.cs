using System.Collections;
using System.Linq.Expressions;

namespace Application.Common.Interfaces.Persistence;

public interface ISqlRepository<TEntity> : IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable
{
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void RemoveRange(params TEntity[] entities);
    void RemoveRange(IEnumerable<TEntity> entities);
    void Remove(Expression<Func<TEntity, bool>> predicate);
}