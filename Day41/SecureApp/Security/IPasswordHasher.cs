namespace SecureApp.Security
{
	public interface IPasswordHasher
	{
		(string hashBase64, string saltBase64) Hash(string password);
		bool Verify(string password, string hashBase64, string saltBase64);
	}
}


