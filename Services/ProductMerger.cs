using ProductIntegrator.Models;

namespace ProductIntegrator.Services
{
    public static class ProductMerger
    {
        // Method to merge products from different providers into a unified list
        public static List<UnifiedProduct> MergeProducts(List<Products1> products1List, List<Products2> products2List, List<Products3> produkty3List)
        {
            var unifiedProducts = (from products1 in products1List
                from product in products1.Product
                select new UnifiedProduct
                {
                    Description = null, // Not available in provider 1
                    ImageUrl = null, // Not available in provider 1
                    Name = null, // Not available in provider 1
                    Variants = product.Sizes.SizeList.Select(s => new Variant
                        {
                            Sku = s.Code,
                            InStock = true, // Assume in stock based on provided data
                            Quantity = s.Quantity
                        })
                        .ToList()
                }).ToList();

            unifiedProducts.AddRange(from products2 in products2List
                from product in products2.Product
                select new UnifiedProduct
                {
                    Description = null, // Not available in provider 2
                    ImageUrl = null, // Not available in provider 2
                    Name = null, // Not available in provider 2
                    Variants = new List<Variant> { new Variant { Sku = product.Sku, InStock = product.InStock, Quantity = product.Qty } }
                });

            unifiedProducts.AddRange(from produkty3 in produkty3List
                from product in produkty3.Produkt
                select new UnifiedProduct
                {
                    Description = product.DlugiOpis,
                    ImageUrl = product.Zdjecia.Zdjecie.FirstOrDefault()?.Url,
                    Name = product.Nazwa,
                    Variants = new List<Variant>
                    {
                        new Variant
                        {
                            Sku = product.Kod, // Assuming 'kod' is SKU
                            InStock = true, // Assume in stock based on provided data
                            Quantity = 0 // Quantity not provided in provider 3
                        }
                    }
                });

            return unifiedProducts;
        }
    }
}
