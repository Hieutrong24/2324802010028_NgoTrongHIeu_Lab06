using ASC.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASC.Web.Controllers;

public class MasterDataController : Controller
{
    private readonly IMasterDataService _masterDataService;

    public MasterDataController(IMasterDataService masterDataService)
    {
        _masterDataService = masterDataService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var data = await _masterDataService.GetAllAsync(cancellationToken);
        return View(data);
    }
}
