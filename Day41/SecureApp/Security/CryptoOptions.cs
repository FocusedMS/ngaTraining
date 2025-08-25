namespace SecureApp.Security
{
	public class CryptoOptions
	{
		public string AesKey { get; set; } = string.Empty; // base64
		public string HmacKey { get; set; } = string.Empty; // base64
		public int Pbkdf2Iterations { get; set; } = 100_000;
		public int SaltSizeBytes { get; set; } = 16;
		public int KeySizeBytes { get; set; } = 32; // 256-bit
	}
}


