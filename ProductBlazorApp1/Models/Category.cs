namespace ProductBlazorApp1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        // bir kategoriye ait birden fazla ürün olabilir
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
