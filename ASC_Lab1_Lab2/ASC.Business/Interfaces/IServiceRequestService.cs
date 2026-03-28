using ASC.Business.Models;
using ASC.Model.Entities;

namespace ASC.Business.Interfaces;

public interface IServiceRequestService
{
    Task<List<ServiceRequest>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ServiceRequest>> GetRecentAsync(int count, CancellationToken cancellationToken = default);
    Task CreateAsync(ServiceRequestCreateDto request, CancellationToken cancellationToken = default);
}
