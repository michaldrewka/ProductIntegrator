using System.Xml.Serialization;

namespace ProductIntegrator.Models
{
    [XmlRoot("offer")]
    public class Products1
    {
        [XmlElement("product")]
        public List<Product1> Product { get; set; }
    }
}