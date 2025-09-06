using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ResumeApi.Data;
using ResumeApi.Models;

namespace ResumeApi.Controllers;

/// <summary>
/// Admin endpoints for user and system monitoring.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles="Admin")]
public class AdminController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userMgr;
    private readonly AppDbContext _db;

    public AdminController(UserManager<ApplicationUser> userMgr, AppDbContext db)
    {
        _userMgr = userMgr; _db = db;
    }

    /// <summary>List users (id, email, full name).</summary>
    [HttpGet("users")]
    public IActionResult Users()
    {
        var list = _userMgr.Users.Select(u => new { u.Id, u.Email, u.FullName }).ToList();
        return Ok(list);
    }

    /// <summary>Simple activity metrics (totals, last 24h and 7d resumes).</summary>
    [HttpGet("metrics")]
    public IActionResult Metrics()
    {
        var totalUsers = _userMgr.Users.Count();
        var totalResumes = _db.Resumes.Count();
        var last24hResumes = _db.Resumes.Count(r => r.CreatedAt >= DateTime.UtcNow.AddDays(-1));
        var last7dResumes = _db.Resumes.Count(r => r.CreatedAt >= DateTime.UtcNow.AddDays(-7));
        return Ok(new { totalUsers, totalResumes, last24hResumes, last7dResumes });
    }
}