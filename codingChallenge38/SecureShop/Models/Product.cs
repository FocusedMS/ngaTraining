using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureShop.Models;

public class Product
{
	public int Id { get; set; }

	[Required]
	[StringLength(100)]
	public string Name { get; set; } = string.Empty;

	[Required]
	[StringLength(1000)]
	public string Description { get; set; } = string.Empty;

	[Range(0.01, 1000000)]
	[Column(TypeName = "decimal(18,2)")]
	public decimal Price { get; set; }
}


