using ProductIntegrator.Models;

namespace ProductIntegrator.Services
{
    public static class ProductMerger
    {
        // Method to merge products from different providers into a unified list
        public static List<UnifiedProduct> MergeProducts(Products1 products1, Products2 products2, Produkty3 produkty3)
        {
            List<UnifiedProduct> unifiedProducts = new List<UnifiedProduct>();

            // Process data from provider 1
            foreach (var product in products1.Product)
            {
                UnifiedProduct unifiedProduct = new UnifiedProduct
                {
                    Description = null,  // Not available in provider 1
                    ImageUrl = null,     // Not available in provider 1
                    Name = null,         // Not available in provider 1
                    Variants = product.Sizes.SizeList.Select(s => new Variant
                    {
                        Sku = s.Code,
                        InStock = true,   // Assume in stock based on provided data
                        Quantity = s.Quantity
                    }).ToList()
                };
                unifiedProducts.Add(unifiedProduct);
            }

            // Process data from provider 2
            foreach (var product in products2.Product)
            {
                UnifiedProduct unifiedProduct = new UnifiedProduct
                {
                    Description = null,  // Not available in provider 2
                    ImageUrl = null,     // Not available in provider 2
                    Name = null,         // Not available in provider 2
                    Variants = new List<Variant>
                    {
                        new Variant
                        {
                            Sku = product.Sku,
                            InStock = product.InStock,
                            Quantity = product.Qty
                        }
                    }
                };
                unifiedProducts.Add(unifiedProduct);
            }

            // Process data from provider 3
            foreach (var product in produkty3.Produkt)
            {
                UnifiedProduct unifiedProduct = new UnifiedProduct
                {
                    Description = product.DlugiOpis,
                    ImageUrl = product.Zdjecia.Zdjecie.FirstOrDefault()?.Url,
                    Name = product.Nazwa,
                    Variants = new List<Variant>
                    {
                        new Variant
                        {
                            Sku = product.Kod,  // Assuming 'kod' is SKU
                            InStock = true,     // Assume in stock based on provided data
                            Quantity = 0       // Quantity not provided in provider 3
                        }
                    }
                };
                unifiedProducts.Add(unifiedProduct);
            }

            return unifiedProducts;
        }
    }
}
