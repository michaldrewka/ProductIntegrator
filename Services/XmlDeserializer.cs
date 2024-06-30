using System.Xml.Serialization;
using ProductIntegrator.Models;

namespace ProductIntegrator.Services
{
    public static class XmlDeserializer
    {
        // Method to deserialize XML data from provider 1
        public static Products1 DeserializeProvider1(string filePath)
        {
            var serializer = new XmlSerializer(typeof(Offer));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                {
                    var offer = (Offer)serializer.Deserialize(fileStream);
                    return (Products1)offer.Products;
                }
            }
        }

        // Method to deserialize XML data from provider 2
        public static Products2 DeserializeProvider2(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Products2));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return (Products2)serializer.Deserialize(fileStream);
            }
        }

        // Method to deserialize XML data from provider 3
        public static Produkty3 DeserializeProvider3(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Produkty3));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return (Produkty3)serializer.Deserialize(fileStream);
            }
        }
    }
}