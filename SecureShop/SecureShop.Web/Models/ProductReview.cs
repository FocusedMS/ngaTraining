using System.ComponentModel.DataAnnotations;

namespace SecureShop.Web.Models
{
    public class ProductReview
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required, StringLength(40)]
        public string ReviewerName { get; set; } = string.Empty;

        [Required, StringLength(300, MinimumLength = 3)]
        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public Product? Product { get; set; }
    }
}
