namespace ASC.DataAccess.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> Query();
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
