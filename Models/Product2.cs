using System.Xml.Serialization;

namespace ProductIntegrator.Models
{
    public class Product2
    {
        [XmlElement("sku")]
        public string Sku { get; set; }

        [XmlElement("qty")]
        public int Qty { get; set; }

        [XmlElement("in_stock")]
        public bool InStock { get; set; }
    }
}