using System.ComponentModel.DataAnnotations;

namespace ProductBlazorApp1.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        [MaxLength(1000)] //SQLite için uyumlu
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; } // 1–5
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
