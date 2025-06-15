using CompaniesRegistry.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CompaniesRegistry.Infrastructure.Database;

public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public Task AddAsync(T entity, CancellationToken cancellationToken = default) =>
        _dbSet.AddAsync(entity, cancellationToken).AsTask();

    public void Remove(T entity) => _dbSet.Remove(entity);

    public IQueryable<T> QueryAll() => _dbSet;

    public IQueryable<T> QueryAllAsNoTracking() => _dbSet.AsNoTracking();

    public IQueryable<T> QueryAllIncluding(params Expression<Func<T, object>>[] paths) =>
        paths.Aggregate(_dbSet.AsQueryable(),
            (current, path) => current.Include(path));

    public IQueryable<T> QueryAllAsNoTrackingIncluding(params Expression<Func<T, object>>[] paths) =>
        paths.Aggregate(_dbSet.AsNoTracking().AsQueryable(),
            (current, path) => current.Include(path));

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        context.SaveChangesAsync(cancellationToken);


}
