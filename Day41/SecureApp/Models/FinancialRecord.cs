using System.ComponentModel.DataAnnotations;

namespace SecureApp.Models
{
	public class FinancialRecord
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public Guid OwnerUserId { get; set; }

		// Encrypted payload (AES-GCM): contains sensitive fields (PAN, account numbers, etc.)
		[Required]
		public byte[] EncryptedPayload { get; set; } = Array.Empty<byte>();

		// HMAC over ciphertext + associated data (owner id) to preserve integrity
		[Required]
		public byte[] PayloadHmac { get; set; } = Array.Empty<byte>();

		public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
	}
}


