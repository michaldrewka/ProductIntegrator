using ProductIntegrator.Models;
using ProductIntegrator.Interfaces;

namespace ProductIntegrator.Services
{
    public class ProductService : IProductService
    {
        public List<UnifiedProduct> GetUnifiedProducts()
        {
            var appDataPath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data");
            var supplier1Path = Path.Combine(appDataPath, "Supplier1");
            var supplier2Path = Path.Combine(appDataPath, "Supplier2");
            var supplier3Path = Path.Combine(appDataPath, "Supplier3");

            var xmlDeserializerProvider1 = new XmlDeserializerProvider1();
            var products1 = xmlDeserializerProvider1.Deserialize(
                Path.Combine(supplier1Path, "dostawca1plik1.xml"),
                Path.Combine(supplier1Path, "dostawca1plik2.xml")
            );

            var xmlDeserializerProvider2 = new XmlDeserializerProvider2();
            var products2 = xmlDeserializerProvider2.Deserialize(
                Path.Combine(supplier2Path, "dostawca2plik1.xml"),
                Path.Combine(supplier2Path, "dostawca2plik2.xml")
            );

            var xmlDeserializerProvider3 = new XmlDeserializerProvider3();
            var products3 = xmlDeserializerProvider3.Deserialize(
                Path.Combine(supplier3Path, "dostawca3plik1.xml")
            );

            return MergeProducts(products1, products2, products3);
        }

        private List<UnifiedProduct> MergeProducts(List<UnifiedProduct> products1, List<UnifiedProduct> products2, List<UnifiedProduct> products3)
        {
            var unifiedProducts = new List<UnifiedProduct>();
            unifiedProducts.AddRange(products1);
            unifiedProducts.AddRange(products2);
            unifiedProducts.AddRange(products3);

            return unifiedProducts;
        }
    }
}
