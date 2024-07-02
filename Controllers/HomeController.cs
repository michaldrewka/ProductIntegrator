using Microsoft.AspNetCore.Mvc;
using ProductIntegrator.Interfaces;
using ProductIntegrator.Services;

namespace ProductIntegrator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var unifiedProducts = _productService.GetUnifiedProducts();
            return View(unifiedProducts);
        }

        [HttpPost]
        public IActionResult FlagProduct(string productName)
        {
            // Implementation of product flagging logic, e.g., saving to a database
            TempData["Message"] = $"Product '{productName}' has been flagged.";
            return RedirectToAction("Index");
        }
    }
}