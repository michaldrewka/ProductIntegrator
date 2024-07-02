using ProductIntegrator.Models;

namespace ProductIntegrator.Services
{
    public static class ProductMerger
    {
        // Method to merge products from different providers into a unified list
        public static List<UnifiedProduct> MergeProducts(List<UnifiedProduct> products1List, List<UnifiedProduct> products2List, List<UnifiedProduct> products3List)
        {
            products1List.AddRange(products2List);
            products1List.AddRange(products3List);

            return products1List;
        }
    }
}
