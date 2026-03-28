using ASC.Business.Interfaces;
using ASC.Business.Models;
using ASC.DataAccess.Interfaces;
using ASC.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASC.Business.Services;

public class DashboardService : IDashboardService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceRequestService _serviceRequestService;
    private readonly IProductService _productService;
    private readonly IMasterDataService _masterDataService;

    public DashboardService(
        IUnitOfWork unitOfWork,
        IServiceRequestService serviceRequestService,
        IProductService productService,
        IMasterDataService masterDataService)
    {
        _unitOfWork = unitOfWork;
        _serviceRequestService = serviceRequestService;
        _productService = productService;
        _masterDataService = masterDataService;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync(CancellationToken cancellationToken = default)
    {
        var totalServiceRequests = await _unitOfWork.Repository<ServiceRequest>().Query().CountAsync(cancellationToken);
        var openServiceRequests = await _unitOfWork.Repository<ServiceRequest>()
            .Query()
            .CountAsync(x => x.Status != "Completed", cancellationToken);

        return new DashboardSummaryDto
        {
            TotalServiceRequests = totalServiceRequests,
            OpenServiceRequests = openServiceRequests,
            TotalProducts = await _productService.CountAsync(cancellationToken),
            TotalMasterDataValues = await _masterDataService.CountValuesAsync(cancellationToken),
            RecentRequests = await _serviceRequestService.GetRecentAsync(5, cancellationToken)
        };
    }
}
