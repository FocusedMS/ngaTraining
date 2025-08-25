namespace SecureApp.Security
{
	public interface IEncryptionService
	{
		byte[] Encrypt(byte[] plaintext, byte[] associatedData);
		(byte[] ciphertext, bool valid) Decrypt(byte[] ciphertext, byte[] associatedData);
	}
}


