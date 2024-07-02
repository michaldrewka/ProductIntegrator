using ProductIntegrator.Models;

namespace ProductIntegrator.Interfaces
{
    public interface IXmlDeserializer
    {
        List<UnifiedProduct> Deserialize(string filePath1, string? filePath2 = null);
    }
}