using System.ComponentModel.DataAnnotations;

namespace SecureApp.Models
{
	public class CreateUserDto
	{
		[Required]
		[EmailAddress]
		[MaxLength(256)]
		public string Email { get; set; } = string.Empty;

		[Required]
		[MinLength(12)]
		public string Password { get; set; } = string.Empty;
	}
}


