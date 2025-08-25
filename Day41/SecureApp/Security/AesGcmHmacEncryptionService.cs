using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace SecureApp.Security
{
	public class AesGcmHmacEncryptionService : IEncryptionService
	{
		private readonly byte[] _aesKey;
		private readonly byte[] _hmacKey;

		public AesGcmHmacEncryptionService(IOptions<CryptoOptions> options)
		{
			var opt = options.Value;
			_aesKey = Convert.FromBase64String(opt.AesKey);
			_hmacKey = Convert.FromBase64String(opt.HmacKey);
			if (_aesKey.Length != opt.KeySizeBytes)
			{
				throw new ArgumentException("AES key size mismatch");
			}
		}

		public byte[] Encrypt(byte[] plaintext, byte[] associatedData)
		{
			var nonce = RandomNumberGenerator.GetBytes(12);
			var ciphertext = new byte[plaintext.Length];
			var tag = new byte[16];
			using var aes = new AesGcm(_aesKey);
			aes.Encrypt(nonce, plaintext, ciphertext, tag, associatedData);
			var combined = new byte[nonce.Length + tag.Length + ciphertext.Length];
			Buffer.BlockCopy(nonce, 0, combined, 0, nonce.Length);
			Buffer.BlockCopy(tag, 0, combined, nonce.Length, tag.Length);
			Buffer.BlockCopy(ciphertext, 0, combined, nonce.Length + tag.Length, ciphertext.Length);

			// HMAC over combined + associatedData for integrity at rest
			using var hmac = new HMACSHA256(_hmacKey);
			hmac.TransformBlock(combined, 0, combined.Length, null, 0);
			hmac.TransformFinalBlock(associatedData, 0, associatedData.Length);
			var mac = hmac.Hash ?? Array.Empty<byte>();
			var result = new byte[combined.Length + mac.Length];
			Buffer.BlockCopy(combined, 0, result, 0, combined.Length);
			Buffer.BlockCopy(mac, 0, result, combined.Length, mac.Length);
			return result;
		}

		public (byte[] ciphertext, bool valid) Decrypt(byte[] ciphertext, byte[] associatedData)
		{
			// Split mac
			if (ciphertext.Length < 32) return (Array.Empty<byte>(), false);
			var mac = new byte[32];
			var combinedLen = ciphertext.Length - mac.Length;
			var combined = new byte[combinedLen];
			Buffer.BlockCopy(ciphertext, combinedLen, mac, 0, mac.Length);
			Buffer.BlockCopy(ciphertext, 0, combined, 0, combinedLen);

			// Verify HMAC
			using var hmac = new HMACSHA256(_hmacKey);
			hmac.TransformBlock(combined, 0, combined.Length, null, 0);
			hmac.TransformFinalBlock(associatedData, 0, associatedData.Length);
			var computed = hmac.Hash ?? Array.Empty<byte>();
			if (!CryptographicOperations.FixedTimeEquals(mac, computed))
			{
				return (Array.Empty<byte>(), false);
			}

			// Split nonce, tag, ct
			if (combinedLen < 12 + 16) return (Array.Empty<byte>(), false);
			var nonce = new byte[12];
			var tag = new byte[16];
			var ct = new byte[combinedLen - nonce.Length - tag.Length];
			Buffer.BlockCopy(combined, 0, nonce, 0, nonce.Length);
			Buffer.BlockCopy(combined, nonce.Length, tag, 0, tag.Length);
			Buffer.BlockCopy(combined, nonce.Length + tag.Length, ct, 0, ct.Length);

			var pt = new byte[ct.Length];
			try
			{
				using var aes = new AesGcm(_aesKey);
				aes.Decrypt(nonce, ct, tag, pt, associatedData);
				return (pt, true);
			}
			catch
			{
				return (Array.Empty<byte>(), false);
			}
		}
	}
}


