namespace ProductIntegrator.Models
{
    public class UnifiedProduct
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public List<Variant> Variants { get; set; }
    }

    public class Variant
    {
        public int Quantity { get; set; }
    }
}