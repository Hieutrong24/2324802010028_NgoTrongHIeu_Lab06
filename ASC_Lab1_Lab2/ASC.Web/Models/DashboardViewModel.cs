using ASC.Model.Entities;

namespace ASC.Web.Models;

public class DashboardViewModel
{
    public string ApplicationTitle { get; set; } = string.Empty;
    public string SupportEmail { get; set; } = string.Empty;
    public string WelcomeMessage { get; set; } = string.Empty;
    public int TotalServiceRequests { get; set; }
    public int OpenServiceRequests { get; set; }
    public int TotalProducts { get; set; }
    public int TotalMasterDataValues { get; set; }
    public IReadOnlyList<ServiceRequest> RecentRequests { get; set; } = Array.Empty<ServiceRequest>();
}
