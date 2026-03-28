using ASC.Business.Interfaces;
using ASC.Business.Models;
using ASC.DataAccess.Interfaces;
using ASC.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASC.Business.Services;

public class ServiceRequestService : IServiceRequestService
{
    private readonly IUnitOfWork _unitOfWork;

    public ServiceRequestService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<List<ServiceRequest>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _unitOfWork.Repository<ServiceRequest>()
            .Query()
            .OrderByDescending(x => x.CreatedOnUtc)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<ServiceRequest>> GetRecentAsync(int count, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.Repository<ServiceRequest>()
            .Query()
            .OrderByDescending(x => x.CreatedOnUtc)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(ServiceRequestCreateDto request, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.Repository<ServiceRequest>();
        var totalCount = await repository.Query().CountAsync(cancellationToken);

        var entity = new ServiceRequest
        {
            RequestNumber = $"SR-{DateTime.UtcNow:yyyyMMdd}-{totalCount + 1:000}",
            CustomerName = request.CustomerName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            VehicleRegistrationNumber = request.VehicleRegistrationNumber,
            CarModel = request.CarModel,
            ServiceType = request.ServiceType,
            Status = "New",
            BranchName = request.BranchName,
            AssignedEngineerName = request.AssignedEngineerName,
            ComplaintDescription = request.ComplaintDescription,
            AppointmentDateUtc = request.AppointmentDateUtc,
            EstimatedAmount = request.EstimatedAmount,
            CreatedBy = request.Email
        };

        await repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
