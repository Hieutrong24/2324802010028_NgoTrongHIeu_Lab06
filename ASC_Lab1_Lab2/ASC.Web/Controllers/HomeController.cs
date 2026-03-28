using System.Diagnostics;
using ASC.Business.Interfaces;
using ASC.Web.Configuration;
using ASC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ASC.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IOptions<ApplicationSettings> _applicationSettings;
    private readonly IDashboardService _dashboardService;

    public HomeController(
        ILogger<HomeController> logger,
        IOptions<ApplicationSettings> applicationSettings,
        IDashboardService dashboardService)
    {
        _logger = logger;
        _applicationSettings = applicationSettings;
        _dashboardService = dashboardService;
    }

    public IActionResult Index()
    {
        return View(_applicationSettings.Value);
    }

    public async Task<IActionResult> Dashboard(CancellationToken cancellationToken)
    {
        var summary = await _dashboardService.GetSummaryAsync(cancellationToken);
        var appSettings = _applicationSettings.Value;

        var model = new DashboardViewModel
        {
            ApplicationTitle = appSettings.ApplicationTitle,
            SupportEmail = appSettings.SupportEmail,
            WelcomeMessage = appSettings.DashboardWelcomeMessage,
            TotalServiceRequests = summary.TotalServiceRequests,
            OpenServiceRequests = summary.OpenServiceRequests,
            TotalProducts = summary.TotalProducts,
            TotalMasterDataValues = summary.TotalMasterDataValues,
            RecentRequests = summary.RecentRequests
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        _logger.LogError("Unhandled error occurred.");
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}
