using System.Xml.Serialization;

namespace ProductIntegrator.Models
{
    public class Product3
    {
        [XmlElement("dlugi_opis")]
        public string DlugiOpis { get; set; }

        [XmlElement("zdjecia")]
        public Zdjecia Zdjecia { get; set; }

        [XmlElement("nazwa")]
        public string Nazwa { get; set; }

        [XmlElement("kod")]
        public string Kod { get; set; }
    }

    public class Zdjecie
    {
        [XmlElement("url")]
        public string Url { get; set; }
    }

    public class Zdjecia
    {
        [XmlElement("zdjecie")]
        public List<Zdjecie> Zdjecie { get; set; }
    }
}