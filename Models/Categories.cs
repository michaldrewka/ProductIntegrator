using System.Xml.Serialization;

namespace ProductIntegrator.Models
{
    public class Categories
    {
        [XmlElement("category")]
        public List<Category> CategoryList { get; set; }
    }
}