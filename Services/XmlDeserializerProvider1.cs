using ProductIntegrator.Interfaces;
using ProductIntegrator.Models;
using System.Xml.Linq;

namespace ProductIntegrator.Services
{
    public class XmlDeserializerProvider1 : IXmlDeserializer
    {
        public List<UnifiedProduct> Deserialize(string filePath1, string? filePath2)
        {
            var productsList = new List<UnifiedProduct>();

            // Logic to deserialize XML from provider 1

            var xml1 = XDocument.Load(filePath1);
            var listOfProducts = xml1.Descendants("product")
                .Select(p => new
                {
                    Id = p.Attribute("id")?.Value,
                    Sizes = p.Descendants("size")
                        .Select(s => new
                        {
                            Quantity = s.Element("stock")?.Attribute("quantity")?.Value
                        })
                        .ToList()
                })
                .ToList();

            if (filePath2 == null) return productsList;
            {
                var xml2 = XDocument.Load(filePath2);
                var products = xml2.Descendants("product")
                    .Select(p => new
                    {
                        Id = (string)p.Attribute("id")!,
                        Name = (string)p.Descendants("name")
                            .FirstOrDefault(n => (string)n.Attribute(XNamespace.Xml + "lang")! == "pol")!,
                        Desc = (string)p.Descendants("long_desc")
                            .FirstOrDefault(d => (string)d.Attribute(XNamespace.Xml + "lang")! == "pol")!,
                        IconUrl = p.Descendants("icon")
                            .Select(i => (string)i.Attribute("url")!)
                            .FirstOrDefault(),
                        Parameters = p.Descendants("parameter")
                            .Where(param => (string)param.Attribute("name")! != "g100s" && (string)param.Attribute("name")! != "prodavab")
                            .ToDictionary(
                                param => (string)param.Attribute("name")!,
                                param => param.Descendants("value")
                                    .Select(val => (string)val.Attribute("name")!)
                                    .ToList()
                            )
                    })
                    .ToList();

                // Join the two lists on Id
                var joinedProducts = from p1 in listOfProducts
                    join p2 in products on p1.Id equals p2.Id
                    select new
                    {
                        p1.Id,
                        p1.Sizes,
                        p2.Name,
                        p2.Desc,
                        p2.IconUrl,
                        p2.Parameters
                    };

                foreach (var product in joinedProducts)
                {
                    var unifiedProduct = new UnifiedProduct
                    {
                        Name = product.Name,
                        Description = product.Desc,
                        ImageUrl = product.IconUrl,
                        Quantity = product.Sizes.FirstOrDefault()?.Quantity,
                        Parameters = product.Parameters
                    };

                    productsList.Add(unifiedProduct);
                }
            }

            return productsList;
        }
    }
}
