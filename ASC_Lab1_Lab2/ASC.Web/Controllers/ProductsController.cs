using ASC.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASC.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var products = await _productService.GetAllAsync(cancellationToken);
        return View(products);
    }
}
