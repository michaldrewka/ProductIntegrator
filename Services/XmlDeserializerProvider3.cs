using ProductIntegrator.Interfaces;
using ProductIntegrator.Models;
using System.Xml.Linq;

namespace ProductIntegrator.Services
{
    public class XmlDeserializerProvider3 : IXmlDeserializer
    {
        public List<UnifiedProduct> Deserialize(string filePath1, string? filePath2 = null)
        {
            var productsList = new List<UnifiedProduct>();

            // Logic to deserialize XML from provider 3

            var doc = XDocument.Load(filePath1);
            var products = doc.Descendants("produkt")
                .Select(p => new
                {
                    Name = (string)p.Descendants("nazwa").FirstOrDefault()!,
                    Desc = (string)p.Descendants("dlugi_opis").FirstOrDefault()!,
                    Photo = (string)p.Descendants("zdjecie").Select(i => (string)i.Attribute("url")!).FirstOrDefault()!,
                    Size = (string)p.Descendants("rozmiar").FirstOrDefault()!,
                    Color = (string)p.Descendants("kolor").FirstOrDefault()!,
                })
                .ToList();

            foreach (var product in products)
            {
                var unifiedProduct = new UnifiedProduct
                {
                    Name = product.Name,
                    Description = product.Desc,
                    ImageUrl = product.Photo,
                };

                unifiedProduct.Parameters.Add("Rozmiar", new List<string> { product.Size });
                unifiedProduct.Parameters.Add("Kolor", new List<string> { product.Color });

                productsList.Add(unifiedProduct);
            }

            return productsList;
        }
    }
}
