namespace ProductBlazorApp1.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }

        // Bir markanın birden fazla ürünü olabilir
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
