using System.Reflection.Metadata;
using System.Xml.Linq;
using System.Xml.Serialization;
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
                            SizeId = s.Attribute("id")?.Value,
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
                    ShortDesc = (string)p
                        .Descendants("short_desc")
                        .FirstOrDefault(d => (string)d.Attribute(XNamespace.Xml + "lang")! == "pol")!,
                    IconUrl = p.Descendants("icon")
                        .Select(i => (string)i.Attribute("url")!)
                        .FirstOrDefault()
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
                    p2.ShortDesc,
                    p2.IconUrl
                };

            // Create UnifiedProduct objects and populate productsList
            foreach (var product in joinedProducts)
            {
                var variants = product.Sizes.Select(variant => new Variant()
                {
                    Quantity = variant.Quantity,
                    VariantType = "Size: " + variant.SizeId
                }).ToList();

                var unifiedProduct = new UnifiedProduct
                {
                    Name = product.Name,
                    Description = product.ShortDesc,
                    ImageUrl = product.IconUrl,
                    Variants = variants
                };

                productsList.Add(unifiedProduct);
            }

            return productsList;
        }

        // Method to deserialize XML data from supplier 2
        public static List<Products2> DeserializeProvider2(string[] filePaths)
        {
            List<Products2> productsList = new List<Products2>();

            foreach (var filePath in filePaths)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Products2));
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    productsList.Add((Products2)serializer.Deserialize(fileStream));
                }
            }

            return productsList;
        }

        // Method to deserialize XML data from supplier 3
        public static List<Products3> DeserializeProvider3(string[] filePaths)
        {
            List<Products3> productsList = new List<Products3>();

            foreach (var filePath in filePaths)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Products3));
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    productsList.Add((Products3)serializer.Deserialize(fileStream));
                }
            }

            return productsList;
        }
}
}