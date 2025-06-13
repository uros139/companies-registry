using CompaniesRegistry.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;

namespace CompaniesRegistry.Infrastructure.Database;

public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _dbSet.FindAsync([id], cancellationToken).AsTask();

    public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult<IEnumerable<T>>(_dbSet.AsEnumerable());

    public Task AddAsync(T entity, CancellationToken cancellationToken = default) =>
        _dbSet.AddAsync(entity, cancellationToken).AsTask();

    public void Remove(T entity) => _dbSet.Remove(entity);

    public IQueryable<T> QueryAll() => _dbSet;

    public IQueryable<T> QueryAllAsNoTracking() => _dbSet.AsNoTracking();

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        context.SaveChangesAsync(cancellationToken);
}