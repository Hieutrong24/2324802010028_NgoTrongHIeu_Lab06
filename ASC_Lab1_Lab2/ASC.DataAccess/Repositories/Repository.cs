using ASC.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASC.DataAccess.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;

    public Repository(DbContext context)
    {
        _dbSet = context.Set<TEntity>();
    }

    public IQueryable<TEntity> Query() => _dbSet.AsQueryable();

    public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _dbSet.ToListAsync(cancellationToken);

    public ValueTask<TEntity?> GetEntityByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _dbSet.FindAsync([id], cancellationToken);

    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        await GetEntityByIdAsync(id, cancellationToken);

    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        _dbSet.AddAsync(entity, cancellationToken).AsTask();

    public void Update(TEntity entity) => _dbSet.Update(entity);

    public void Delete(TEntity entity) => _dbSet.Remove(entity);
}
