using System.ComponentModel.DataAnnotations;

namespace SecureShop.Models;

public class Review
{
	public int Id { get; set; }

	[Required]
	public int ProductId { get; set; }

	[Required]
	[StringLength(1000)]
	public string Content { get; set; } = string.Empty;

	[Range(1,5)]
	public int Rating { get; set; }
}


