using System.Xml.Serialization;

namespace ProductIntegrator.Models
{
    public class Photos
    {
        [XmlElement("photo")]
        public List<Photo> PhotoList { get; set; }
    }
}