using System.ComponentModel.DataAnnotations;

namespace SecureShop.Web.Models
{
    public class ReviewInputModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required, StringLength(40)]
        public string ReviewerName { get; set; } = string.Empty;

        [Required, StringLength(300, MinimumLength = 3)]
        public string Comment { get; set; } = string.Empty;
    }
}
