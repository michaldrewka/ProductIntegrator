using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using ProductIntegrator.Models;

namespace ProductIntegrator.Services
{
    public static class XmlDeserializer
    {
        public static List<UnifiedProduct> DeserializeProvider1(string[] filePaths)
        {
            var productsList = new List<UnifiedProduct>();

            foreach (var filePath in filePaths)
            {
                // Load the XML file into an XDocument
                var doc = XDocument.Load(filePath);

                // Get the <name> element with xml:lang="pol"
                var nameElement = doc
                    .Descendants("name")
                    .FirstOrDefault(e => e.Attribute(XNamespace.Xml + "lang")?.Value == "pol");

                // Get the <short_desc> element with xml:lang="pol"
                var descriptionElement = doc
                    .Descendants("short_desc")
                    .FirstOrDefault(e => e.Attribute(XNamespace.Xml + "lang")?.Value == "pol");

                // Get the <icon> element 
                var imageUrl = doc
                    .Descendants("icon")
                    .FirstOrDefault()
                    ?.Attribute("url");


                // Extract the text content inside the CDATA section
                var productName = nameElement?.Value;

                    if (productName == null) continue;
                    var productSupplier1 = new UnifiedProduct
                    {
                        Name = productName,
                        Description = descriptionElement?.Value,
                        ImageUrl = imageUrl?.Value,
                        Variants = new List<Variant>()
                    };

                    productsList.Add(productSupplier1);
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