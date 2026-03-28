using ASC.Model.Entities;

namespace ASC.Business.Models;

public class DashboardSummaryDto
{
    public int TotalServiceRequests { get; set; }
    public int OpenServiceRequests { get; set; }
    public int TotalProducts { get; set; }
    public int TotalMasterDataValues { get; set; }
    public IReadOnlyList<ServiceRequest> RecentRequests { get; set; } = Array.Empty<ServiceRequest>();
}
