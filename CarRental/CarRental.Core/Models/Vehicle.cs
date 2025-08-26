using System.ComponentModel.DataAnnotations;

namespace CarRental.Core.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Make { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Model { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string VehicleType { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string LicensePlate { get; set; } = string.Empty;
        
        public int Year { get; set; }
        
        public decimal DailyRate { get; set; }
        
        public bool IsAvailable { get; set; } = true;
        
        public string? Description { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
    }
}
