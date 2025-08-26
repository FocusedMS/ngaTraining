using System.ComponentModel.DataAnnotations;

namespace CarRental.Core.DTOs
{
    public class CreateVehicleDto
    {
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
        
        [Range(1900, 2030)]
        public int Year { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal DailyRate { get; set; }
        
        public string? Description { get; set; }
    }

    public class UpdateVehicleDto
    {
        [StringLength(100)]
        public string? Make { get; set; }
        
        [StringLength(100)]
        public string? Model { get; set; }
        
        [StringLength(50)]
        public string? VehicleType { get; set; }
        
        [StringLength(20)]
        public string? LicensePlate { get; set; }
        
        [Range(1900, 2030)]
        public int? Year { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal? DailyRate { get; set; }
        
        public bool? IsAvailable { get; set; }
        
        public string? Description { get; set; }
    }

    public class VehicleResponseDto
    {
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal DailyRate { get; set; }
        public bool IsAvailable { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
