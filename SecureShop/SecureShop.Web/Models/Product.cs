using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SecureShop.Web.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(80)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Description { get; set; }

        [Precision(18, 2)]
        [Range(0.0, 999999.99)]
        public decimal Price { get; set; }
    }
}
