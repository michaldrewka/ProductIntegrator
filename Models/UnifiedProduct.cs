namespace ProductIntegrator.Models
{
    public class UnifiedProduct
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Quantity { get; set; }
        public Dictionary<string, List<string>> Parameters { get; set; }

        public UnifiedProduct()
        {
            Parameters = new Dictionary<string, List<string>>();
        }
    }
}