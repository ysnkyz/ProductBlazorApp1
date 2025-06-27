namespace ProductBlazorApp1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        public string Description { get; set; } = string.Empty;

        // marka ilişkisi
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }        //Nullable

        // kategori ilişkisi
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }  

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
