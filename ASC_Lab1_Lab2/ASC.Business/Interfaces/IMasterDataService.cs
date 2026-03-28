using ASC.Model.Entities;

namespace ASC.Business.Interfaces;

public interface IMasterDataService
{
    Task<List<MasterDataKey>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<MasterDataValue>> GetValuesAsync(string keyName, CancellationToken cancellationToken = default);
    Task<int> CountValuesAsync(CancellationToken cancellationToken = default);
}
