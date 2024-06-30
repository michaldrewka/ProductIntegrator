using System.Xml.Serialization;

namespace ProductIntegrator.Models
{
    [XmlRoot("products")]
    public class Products2
    {
        [XmlElement("product")]
        public List<Product2> Product { get; set; }
    }
}