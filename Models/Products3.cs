using System.Xml.Serialization;

namespace ProductIntegrator.Models
{
    [XmlRoot("produkty")]
    public class Products3
    {
        [XmlElement("produkt")]
        public List<Product3> Produkt { get; set; }
    }
}