using System.Xml.Serialization;

namespace ProductIntegrator.Models
{
    public class Product3
    {
        [XmlElement("dlugi_opis")]
        public string Description { get; set; }

        [XmlElement("zdjecia")]
        public Images Images { get; set; }

        [XmlElement("nazwa")]
        public string Name { get; set; }

        [XmlElement("kod")]
        public string Kod { get; set; }
    }

    public class Image
    {
        [XmlElement("url")]
        public string Url { get; set; }
    }

    public class Images
    {
        [XmlElement("zdjecie")]
        public List<Image> Zdjecie { get; set; }
    }
}