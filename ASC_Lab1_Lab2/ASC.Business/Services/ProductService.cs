using ASC.Business.Interfaces;
using ASC.DataAccess.Interfaces;
using ASC.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASC.Business.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _unitOfWork.Repository<Product>()
            .Query()
            .OrderBy(x => x.Category)
            .ThenBy(x => x.Name)
            .ToListAsync(cancellationToken);

    public Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        _unitOfWork.Repository<Product>().Query().CountAsync(cancellationToken);
}
