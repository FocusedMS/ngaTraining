using System.ComponentModel.DataAnnotations;

namespace CarRental.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;
        
        [StringLength(255)]
        public string? PasswordHash { get; set; }
        
        [StringLength(255)]
        public string? GitHubId { get; set; }
        
        [StringLength(255)]
        public string? GitHubUsername { get; set; }
        
        [StringLength(255)]
        public string? GitHubEmail { get; set; }
        
        [StringLength(255)]
        public string? RefreshToken { get; set; }
        
        public DateTime? RefreshTokenExpiryTime { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
    }
}
