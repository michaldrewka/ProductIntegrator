using System.Xml.Linq;
using ProductIntegrator.Models;

namespace ProductIntegrator.Services
{
    public static class XmlDeserializer
    {
        public static List<UnifiedProduct> DeserializeProvider1(string file1Path, string file2Path)
        {
            var productsList = new List<UnifiedProduct>();

            // Load the XML file into an XDocument
            var xml1  = XDocument.Load(file1Path);
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
            
            var xml2 = XDocument.Load(file2Path);

            var products = xml2.Descendants("product")
                .Select(p => new
                {
                    Id = (string)p.Attribute("id")!,
                    Name = (string)p
                        .Descendants("name")
                        .FirstOrDefault(n => (string)n.Attribute(XNamespace.Xml + "lang")! == "pol")!,
                    Desc = (string)p
                        .Descendants("long_desc")
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

            // Create UnifiedProduct objects and populate productsList
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

            return productsList;
        }

        // Method to deserialize XML data from supplier 2
        public static List<UnifiedProduct> DeserializeProvider2(string file1Path, string file2Path)
        {
            var productsList = new List<UnifiedProduct>();

            // Load the XML file into an XDocument
            var xml1 = XDocument.Load(file1Path);
            var listOfProducts = xml1.Descendants("product")
                .Select(p => new
                {
                    Id = (string)p
                        .Descendants("id")
                        .FirstOrDefault()!,
                    Qty = (string)p.Descendants("qty")
                        .FirstOrDefault()!
                })
                .ToList();

            var xml2 = XDocument.Load(file2Path);

            var products = xml2.Descendants("product")
                .Select(p => new
                {
                    Id = (string)p
                        .Descendants("id")
                        .FirstOrDefault()!,
                    Name = (string)p
                        .Descendants("name")
                        .FirstOrDefault()!,
                    Desc = (string)p
                        .Descendants("desc")
                        .FirstOrDefault()!,
                    Photo = (string)p.Descendants("photo")
                        .FirstOrDefault(n => (string)n.Attribute("main")! == "1")!,
                    Weight = (string)p
                        .Descendants("weight")
                        .FirstOrDefault()!,
                })
                .ToList();

            // Join the two lists on Id
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

            // Create UnifiedProduct objects and populate productsList
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

        // Method to deserialize XML data from supplier 3
        public static List<UnifiedProduct> DeserializeProvider3(string filePath)
        {
            var productsList = new List<UnifiedProduct>();

            var doc = XDocument.Load(filePath);
            var products = doc.Descendants("produkt")
                .Select(p => new
                {
                    Name = (string)p
                        .Descendants("nazwa")
                        .FirstOrDefault()!,
                    Desc = (string)p
                        .Descendants("dlugi_opis")
                        .FirstOrDefault()!,
                    Photo = (string)p.Descendants("zdjecie")
                        .Select(i => (string)i.Attribute("url")!)
                        .FirstOrDefault()!,
                    Size = (string)p.Descendants("rozmiar")
                        .FirstOrDefault()!,
                    Color = (string)p.Descendants("kolor")
                        .FirstOrDefault()!,
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