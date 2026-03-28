using ASC.Business.Interfaces;
using ASC.DataAccess.Interfaces;
using ASC.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASC.Business.Services;

public class MasterDataService : IMasterDataService
{
    private readonly IUnitOfWork _unitOfWork;

    public MasterDataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<List<MasterDataKey>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _unitOfWork.Repository<MasterDataKey>()
            .Query()
            .Include(x => x.Values.OrderBy(v => v.DisplaySequence))
            .OrderBy(x => x.DisplayName)
            .ToListAsync(cancellationToken);

    public Task<List<MasterDataValue>> GetValuesAsync(string keyName, CancellationToken cancellationToken = default) =>
        _unitOfWork.Repository<MasterDataValue>()
            .Query()
            .Include(x => x.MasterDataKey)
            .Where(x => x.MasterDataKey != null && x.MasterDataKey.Name == keyName && x.IsActive)
            .OrderBy(x => x.DisplaySequence)
            .ToListAsync(cancellationToken);

    public Task<int> CountValuesAsync(CancellationToken cancellationToken = default) =>
        _unitOfWork.Repository<MasterDataValue>().Query().CountAsync(cancellationToken);
}
