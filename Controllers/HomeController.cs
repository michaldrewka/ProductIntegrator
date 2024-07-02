using Microsoft.AspNetCore.Mvc;
using ProductIntegrator.Services;
using System.IO;

namespace ProductIntegrator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Load data from XML files located in the App_Data directory
            var appDataPath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data");
            var supplier1Path = Path.Combine(appDataPath, "Supplier1");
            var supplier2Path = Path.Combine(appDataPath, "Supplier2");
            var supplier3Path = Path.Combine(appDataPath, "Supplier3");

            var products1 = XmlDeserializer.DeserializeProvider1(Path.Combine(supplier1Path, "dostawca1plik1.xml"), Path.Combine(supplier1Path, "dostawca1plik2.xml"));
            var products2 = XmlDeserializer.DeserializeProvider2(Path.Combine(supplier2Path, "dostawca2plik1.xml"), Path.Combine(supplier2Path, "dostawca2plik2.xml"));
            var products3 = XmlDeserializer.DeserializeProvider3(Path.Combine(supplier3Path, "dostawca3plik1.xml"));

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