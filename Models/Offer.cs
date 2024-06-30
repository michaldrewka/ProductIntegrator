using System.Xml.Serialization;

namespace ProductIntegrator.Models
{
    [XmlRoot("offer")]
    public class Offer
    {
        [XmlElement("products")]
        public Products1 Products { get; set; }
    }
}
