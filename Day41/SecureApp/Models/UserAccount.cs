using System.ComponentModel.DataAnnotations;

namespace SecureApp.Models
{
	public class UserAccount
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		[EmailAddress]
		[MaxLength(256)]
		public string Email { get; set; } = string.Empty;

		// Never store plain passwords. Store strong hash and salt.
		[Required]
		public string PasswordHash { get; set; } = string.Empty;

		[Required]
		public string PasswordSalt { get; set; } = string.Empty;

		[MaxLength(100)]
		public string Role { get; set; } = "User";
	}
}


