using Microsoft.AspNetCore.Mvc;
using ProductIntegrator.Services;

namespace ProductIntegrator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Load data from XML files located in the App_Data directory
            var appDataPath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data");
            var products1 = XmlDeserializer.DeserializeProvider1(Path.Combine(appDataPath, "dostawca1plik1.xml"));
            var products2 = XmlDeserializer.DeserializeProvider2(Path.Combine(appDataPath, "dostawca2plik1.xml"));
            var products3 = XmlDeserializer.DeserializeProvider3(Path.Combine(appDataPath, "dostawca3plik1.xml"));

            // Merge the data into a unified product list
            var unifiedProducts = ProductMerger.MergeProducts(products1, products2, products3);

            // Pass the unified product list to the view
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