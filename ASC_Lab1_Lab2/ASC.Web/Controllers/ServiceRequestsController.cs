using ASC.Business.Interfaces;
using ASC.Business.Models;
using ASC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASC.Web.Controllers;

public class ServiceRequestsController : Controller
{
    private readonly IServiceRequestService _serviceRequestService;
    private readonly IMasterDataService _masterDataService;

    public ServiceRequestsController(
        IServiceRequestService serviceRequestService,
        IMasterDataService masterDataService)
    {
        _serviceRequestService = serviceRequestService;
        _masterDataService = masterDataService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var requests = await _serviceRequestService.GetAllAsync(cancellationToken);
        return View(requests);
    }

    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        return View(await BuildViewModelAsync(new ServiceRequestCreateViewModel(), cancellationToken));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ServiceRequestCreateViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(await BuildViewModelAsync(model, cancellationToken));
        }

        await _serviceRequestService.CreateAsync(new ServiceRequestCreateDto
        {
            CustomerName = model.CustomerName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            VehicleRegistrationNumber = model.VehicleRegistrationNumber,
            CarModel = model.CarModel,
            ServiceType = model.ServiceType,
            BranchName = model.BranchName,
            AssignedEngineerName = model.AssignedEngineerName,
            ComplaintDescription = model.ComplaintDescription,
            AppointmentDateUtc = model.AppointmentDateUtc,
            EstimatedAmount = model.EstimatedAmount
        }, cancellationToken);

        TempData["SuccessMessage"] = "Service request created successfully.";
        return RedirectToAction(nameof(Index));
    }

    private async Task<ServiceRequestCreateViewModel> BuildViewModelAsync(
        ServiceRequestCreateViewModel model,
        CancellationToken cancellationToken)
    {
        var serviceTypes = await _masterDataService.GetValuesAsync("ServiceType", cancellationToken);
        model.ServiceTypes = serviceTypes
            .Select(x => new SelectListItem(x.DisplayName, x.Value))
            .ToList();

        return model;
    }
}
