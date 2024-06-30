using System.Xml.Serialization;

namespace ProductIntegrator.Models
{
    public class Product2
    {
        [XmlElement("sku")]
        public string Sku { get; set; }

        [XmlElement("qty")]
        public string Qty { get; set; }

        [XmlElement("in_stock")]
        public bool InStock { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }
}