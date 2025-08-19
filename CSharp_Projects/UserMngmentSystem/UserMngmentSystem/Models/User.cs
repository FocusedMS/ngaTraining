namespace UserMngmentSystem.Models
{
    public class User
    {
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string SensitiveInfo { get; set; }
    }
}
