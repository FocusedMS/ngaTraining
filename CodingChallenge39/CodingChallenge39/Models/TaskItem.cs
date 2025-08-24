using System.ComponentModel.DataAnnotations;

namespace YourProjectNamespace.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        [Required]
        public string OwnerUserId { get; set; } = string.Empty;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
