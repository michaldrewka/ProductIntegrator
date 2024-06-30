using System.Xml.Serialization;

namespace ProductIntegrator.Models
{
    [XmlRoot("produkty")]
    public class Produkty3
    {
        [XmlElement("produkt")]
        public List<Produkt> Produkt { get; set; }
    }
}