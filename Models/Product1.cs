using System.Xml.Serialization;

namespace ProductIntegrator.Models
{
    [XmlRoot(ElementName = "size")]
    public class Size
    {
        [XmlElement("code")]
        public string Code { get; set; }

        [XmlElement("quantity")]
        public int Quantity { get; set; }
    }

    [XmlRoot(ElementName = "sizes")]
    public class Sizes
    {
        [XmlElement("size")]
        public List<Size> SizeList { get; set; }
    }

    public class Product1
    {
        [XmlElement("sizes")]
        public Sizes Sizes { get; set; }
    }
}