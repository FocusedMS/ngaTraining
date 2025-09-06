using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoleBasedProductManagement.Models;

public class Product
{
    public int Id { get; set; }

    [Required, StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [StringLength(300)]
    public string? Description { get; set; }

    // Stored encrypted using Data Protection
    [Required]
    public string ProtectedPrice { get; set; } = string.Empty;

    // Plain value for forms/display (not stored directly)
    [NotMapped]
    [Range(0.01, 1000000)]
    public decimal Price { get; set; }
}
