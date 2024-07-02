using ProductIntegrator.Models;

namespace ProductIntegrator.Interfaces
{
    public interface IProductService
    {
        List<UnifiedProduct> GetUnifiedProducts();
    }
}