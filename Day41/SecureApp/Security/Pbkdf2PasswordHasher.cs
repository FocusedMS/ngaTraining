using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace SecureApp.Security
{
	public class Pbkdf2PasswordHasher : IPasswordHasher
	{
		private readonly CryptoOptions _options;
		public Pbkdf2PasswordHasher(IOptions<CryptoOptions> options)
		{
			_options = options.Value;
		}

		public (string hashBase64, string saltBase64) Hash(string password)
		{
			var salt = RandomNumberGenerator.GetBytes(_options.SaltSizeBytes);
			var derived = Rfc2898DeriveBytes.Pbkdf2(password, salt, _options.Pbkdf2Iterations, HashAlgorithmName.SHA256, _options.KeySizeBytes);
			return (Convert.ToBase64String(derived), Convert.ToBase64String(salt));
		}

		public bool Verify(string password, string hashBase64, string saltBase64)
		{
			var salt = Convert.FromBase64String(saltBase64);
			var expected = Convert.FromBase64String(hashBase64);
			var derived = Rfc2898DeriveBytes.Pbkdf2(password, salt, _options.Pbkdf2Iterations, HashAlgorithmName.SHA256, expected.Length);
			return CryptographicOperations.FixedTimeEquals(derived, expected);
		}
	}
}


