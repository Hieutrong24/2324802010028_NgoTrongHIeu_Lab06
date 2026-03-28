using ASC.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASC.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private readonly Dictionary<string, object> _repositories = new();

    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var typeName = typeof(TEntity).Name;

        if (_repositories.TryGetValue(typeName, out var repository))
        {
            return (IRepository<TEntity>)repository;
        }

        var instance = new Repository<TEntity>(_context);
        _repositories[typeName] = instance;
        return instance;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        _context.Dispose();
    }
}
