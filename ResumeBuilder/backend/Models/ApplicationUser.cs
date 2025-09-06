using Microsoft.AspNetCore.Identity;

namespace ResumeApi.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}