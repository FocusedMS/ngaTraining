using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureShop.Models;

public class Order
{
	public int Id { get; set; }

	[Required]
	public string UserId { get; set; } = string.Empty;

	[DataType(DataType.DateTime)]
	public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

	[Range(0, 100000000)]
	[Column(TypeName = "decimal(18,2)")]
	public decimal Total { get; set; }

	public List<OrderItem> Items { get; set; } = new();
}


