using System.Xml.Serialization;
using ProductIntegrator.Models;

namespace ProductIntegrator.Services
{
    public static class XmlDeserializer
    {
        // Method to deserialize XML data from supplier 1
        public static List<Products1> DeserializeProvider1(string[] filePaths)
        {
            List<Products1> productsList = new List<Products1>();

            foreach (var filePath in filePaths)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Offer));
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    var offer = (Offer)serializer.Deserialize(fileStream);
                    productsList.Add((Products1)offer.Products);
                }
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