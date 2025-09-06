using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeApi.Models;

public class Resume
{
    [Key]
    public int ResumeId { get; set; }

    [Required]
    public string UserId { get; set; } = default!;

    [Required, MaxLength(160)]
    public string Title { get; set; } = "My Resume";

    public string? PersonalInfo { get; set; }
    public string? Education { get; set; }
    public string? Experience { get; set; }
    public string? Skills { get; set; }

    public string? AiSuggestions { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}