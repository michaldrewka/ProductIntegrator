using System.Xml.Serialization;

namespace ProductIntegrator.Models
{
    public class Photo
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("main")]
        public string Main { get; set; }

        [XmlText]
        public string Url { get; set; }
    }
}