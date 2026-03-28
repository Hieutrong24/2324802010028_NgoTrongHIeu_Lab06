using ASC.Model.Entities;

namespace ASC.Business.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken = default);
}
