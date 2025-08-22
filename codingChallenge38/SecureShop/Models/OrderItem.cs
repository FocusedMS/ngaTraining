using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureShop.Models;

public class OrderItem
{
	public int Id { get; set; }

	[Required]
	public int ProductId { get; set; }

	[Required]
	public int OrderId { get; set; }

	[Range(1, 1000)]
	public int Quantity { get; set; }

	[Range(0.01, 1000000)]
	[Column(TypeName = "decimal(18,2)")]
	public decimal UnitPrice { get; set; }

	public Product? Product { get; set; }
}


