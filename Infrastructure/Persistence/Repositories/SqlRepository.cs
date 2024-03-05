using System.Collections;
using System.Linq.Expressions;
using Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class SqlRepository<TEntity> : ISqlRepository<TEntity> where TEntity : class
{
    public SqlRepository(SqlContext context)
    {
        Context = context;
        Items = Context.Set<TEntity>();
    }

    private SqlContext Context { get; }
    private DbSet<TEntity> Items { get; }
    Type IQueryable.ElementType => ((IQueryable<TEntity>)Items).ElementType;
    Expression IQueryable.Expression => ((IQueryable<TEntity>)Items).Expression;
    IQueryProvider IQueryable.Provider => ((IQueryable<TEntity>)Items).Provider;
    IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator() => ((IEnumerable<TEntity>)Items).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TEntity>)Items).GetEnumerator();

    public void Add(TEntity entity) => Items.Add(entity);
    public void AddRange(IEnumerable<TEntity> entity) => Items.AddRange(entity);
    public void Update(TEntity entity) => Items.Update(entity);
    public void Remove(TEntity entity) => Items.Remove(entity);
    public void Remove(Expression<Func<TEntity, bool>> predicate) => Items.RemoveRange(Items.AsNoTracking().Where(predicate).ToList());
    public void RemoveRange(params TEntity[] entities) => Items.RemoveRange(entities);
    public void RemoveRange(IEnumerable<TEntity> entities) => Items.RemoveRange(entities);
}