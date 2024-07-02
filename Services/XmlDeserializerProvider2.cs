using ProductIntegrator.Interfaces;
using ProductIntegrator.Models;
using System.Xml.Linq;

namespace ProductIntegrator.Services
{
    public class XmlDeserializerProvider2 : IXmlDeserializer
    {
        public List<UnifiedProduct> Deserialize(string filePath1, string? filePath2)
        {
            var productsList = new List<UnifiedProduct>();

            // Logic to deserialize XML from provider 2

            var xml1 = XDocument.Load(filePath1);
            var listOfProducts = xml1.Descendants("product")
                .Select(p => new
                {
                    Id = (string)p.Descendants("id").FirstOrDefault()!,
                    Qty = (string)p.Descendants("qty").FirstOrDefault()!
                })
                .ToList();

            var xml2 = XDocument.Load(filePath2);
            var products = xml2.Descendants("product")
                .Select(p => new
                {
                    Id = (string)p.Descendants("id").FirstOrDefault()!,
                    Name = (string)p.Descendants("name").FirstOrDefault()!,
                    Desc = (string)p.Descendants("desc").FirstOrDefault()!,
                    Photo = (string)p.Descendants("photo").FirstOrDefault(n => (string)n.Attribute("main")! == "1")!,
                    Weight = (string)p.Descendants("weight").FirstOrDefault()!,
                })
                .ToList();

            var joinedProducts = from p1 in listOfProducts
                                 join p2 in products on p1.Id equals p2.Id
                                 select new
                                 {
                                     p1.Id,
                                     p2.Name,
                                     p2.Desc,
                                     p2.Photo,
                                     p1.Qty,
                                     p2.Weight
                                 };

            foreach (var product in joinedProducts)
            {
                var unifiedProduct = new UnifiedProduct
                {
                    Name = product.Name,
                    Description = product.Desc,
                    ImageUrl = product.Photo,
                    Quantity = product.Qty
                };

                unifiedProduct.Parameters.Add("Waga", new List<string> { product.Weight });

                productsList.Add(unifiedProduct);
            }

            return productsList;
        }
    }
}
