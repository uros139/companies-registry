using System.Linq.Expressions;

namespace CompaniesRegistry.Application.Abstractions.Data;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void Remove(T entity);
    IQueryable<T> QueryAll();
    IQueryable<T> QueryAllAsNoTracking();
    IQueryable<T> QueryAllIncluding(params Expression<Func<T, object>>[] paths);
    IQueryable<T> QueryAllAsNoTrackingIncluding(params Expression<Func<T, object>>[] paths);
}
