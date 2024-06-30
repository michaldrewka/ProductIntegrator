using System.Xml.Serialization;

namespace ProductIntegrator.Models
{
    public class Category
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}