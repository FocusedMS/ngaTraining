using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResumeApi.Data;
using ResumeApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using iText.Kernel.Pdf;
using iText.Layout;
using ResumeApi.Repositories;

namespace ResumeApi.Controllers;

/// <summary>
/// Manage resumes: create, update, list, download PDF, and get AI suggestions.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ResumesController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userMgr;
    private readonly IResumeRepository _repo;

    public ResumesController(AppDbContext db, UserManager<ApplicationUser> userMgr, IResumeRepository repo)
    {
        _db = db; _userMgr = userMgr; _repo = repo;
    }

    private string? UserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
    private bool IsAdmin() => User.IsInRole("Admin");

    public record ResumeDto([Required]string Title, string? PersonalInfo, string? Education, string? Experience, string? Skills);

    /// <summary>List resumes for the current user; Admin can pass all=1 to list all.</summary>
    [HttpGet]
    [Authorize(Roles="RegisteredUser,Admin")]
    public async Task<IActionResult> List([FromQuery] bool all = false)
    {
        if (IsAdmin() && all)
            return Ok(await _repo.ListAllAsync());

        var uid = UserId();
        if (uid is null) return Unauthorized();

        return Ok(await _repo.ListByUserAsync(uid));
    }

    /// <summary>Get a resume by id (owner or Admin only).</summary>
    [HttpGet("{id:int}")]
    [Authorize(Roles="RegisteredUser,Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        var r = await _repo.GetByIdAsync(id);
        if (r == null) return NotFound();
        if (!IsAdmin() && r.UserId != UserId()) return Forbid();
        return Ok(r);
    }

    /// <summary>Create a new resume for the current user.</summary>
    [HttpPost]
    [Authorize(Roles="RegisteredUser,Admin")]
    public async Task<IActionResult> Create(ResumeDto dto)
    {
        var uid = UserId();
        if (uid is null) return Unauthorized();

        var r = new Resume
        {
            UserId = uid,
            Title = dto.Title,
            PersonalInfo = dto.PersonalInfo,
            Education = dto.Education,
            Experience = dto.Experience,
            Skills = dto.Skills
        };
        var idNew = await _repo.CreateAsync(r);
        return Ok(idNew);
    }

    /// <summary>Update a resume (owner or Admin only).</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles="RegisteredUser,Admin")]
    public async Task<IActionResult> Update(int id, ResumeDto dto)
    {
        var r = await _repo.GetByIdAsync(id);
        if (r == null) return NotFound();
        if (!IsAdmin() && r.UserId != UserId()) return Forbid();

        r.Title = dto.Title;
        r.PersonalInfo = dto.PersonalInfo;
        r.Education = dto.Education;
        r.Experience = dto.Experience;
        r.Skills = dto.Skills;
        r.UpdatedAt = DateTime.UtcNow;
        await _repo.UpdateAsync(r);
        return Ok(new { message = "Updated" });
    }

    /// <summary>Delete a resume (owner or Admin only).</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles="RegisteredUser,Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var r = await _repo.GetByIdAsync(id);
        if (r == null) return NotFound();
        if (!IsAdmin() && r.UserId != UserId()) return Forbid();

        await _repo.DeleteAsync(r);
        return NoContent();
    }

    /// <summary>Download a resume as a formatted PDF (owner or Admin only).</summary>
    [HttpGet("download/{id:int}")]
    [Authorize(Roles="RegisteredUser,Admin")]
    public async Task<IActionResult> Download(int id, [FromQuery] string? template = "classic")
    {
        var r = await _repo.GetByIdAsync(id);
        if (r == null) return NotFound();
        if (!IsAdmin() && r.UserId != UserId()) return Forbid();

        using var ms = new MemoryStream();
        var writer = new PdfWriter(ms);
        var pdf = new PdfDocument(writer);
        var doc = new Document(pdf);
        doc.SetMargins(36, 36, 36, 36);

        void RenderClassic()
        {
            var title = new iText.Layout.Element.Paragraph(r.Title)
                .SetBold().SetFontSize(18);
            doc.Add(title);

            void AddSection(string header, string? body)
            {
                if (string.IsNullOrWhiteSpace(body)) return;
                doc.Add(new iText.Layout.Element.Paragraph(header).SetBold().SetFontSize(12).SetMarginTop(12));
                doc.Add(new iText.Layout.Element.Paragraph(body).SetFontSize(10).SetMarginTop(4));
            }

            AddSection("Personal", r.PersonalInfo);
            AddSection("Education", r.Education);
            AddSection("Experience", r.Experience);
            AddSection("Skills", r.Skills);
        }

        void RenderCompact()
        {
            var title = new iText.Layout.Element.Paragraph(r.Title)
                .SetBold().SetFontSize(14);
            doc.Add(title);
            var all = new System.Text.StringBuilder();
            if (!string.IsNullOrWhiteSpace(r.PersonalInfo)) all.AppendLine(r.PersonalInfo);
            if (!string.IsNullOrWhiteSpace(r.Education)) all.AppendLine("\nEducation:\n" + r.Education);
            if (!string.IsNullOrWhiteSpace(r.Experience)) all.AppendLine("\nExperience:\n" + r.Experience);
            if (!string.IsNullOrWhiteSpace(r.Skills)) all.AppendLine("\nSkills:\n" + r.Skills);
            doc.Add(new iText.Layout.Element.Paragraph(all.ToString()).SetFontSize(10));
        }

        if ((template ?? "classic").Equals("compact", StringComparison.OrdinalIgnoreCase)) RenderCompact();
        else RenderClassic();
        if (!string.IsNullOrWhiteSpace(r.PersonalInfo))
            { /* sections added via AddSection */ }
        doc.Close();

        return File(ms.ToArray(), "application/pdf", $"resume-{id}.pdf");
    }

    /// <summary>Generate and persist AI suggestions for the resume.</summary>
    [HttpPost("{id:int}/ai-suggestions")]
    [Authorize(Roles="RegisteredUser,Admin")]
    public async Task<IActionResult> AiSuggestions(int id)
    {
        var r = await _repo.GetByIdAsync(id);
        if (r == null) return NotFound();
        if (!IsAdmin() && r.UserId != UserId()) return Forbid();

        var suggestions = new List<object>();

        if (string.IsNullOrWhiteSpace(r.PersonalInfo) || r.PersonalInfo!.Length < 160)
            suggestions.Add(new { section="PersonalInfo", message="Write a concise 2–3 sentence summary highlighting role, YOE, core stack, and 1 metric.", applySnippet = "Full‑stack developer with 3+ years experience in .NET, React, and Azure. Delivered 5+ features improving performance by 25%." });

        if (string.IsNullOrWhiteSpace(r.Experience))
            suggestions.Add(new { section="Experience", message="Add 3–6 STAR-style bullet points; start with strong verbs, include metrics.", applySnippet = "• Improved API latency by 35% by optimizing EF Core queries and adding caching.\n• Led migration to Azure App Service, reducing downtime to < 5 min.\n• Mentored 2 juniors; increased team throughput by 15%." });
        else if (!(r.Experience.Any(char.IsDigit) || r.Experience.Contains("%")))
            suggestions.Add(new { section="Experience", message="Quantify at least two bullets (%, ms, $, users).", applySnippet = "• Reduced build time by 40% by parallelizing CI steps.\n• Cut error rates by 18% through input validation and logging." });

        var skills = (r.Skills ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (skills.Length < 5)
            suggestions.Add(new { section="Skills", message="List 8–12 skills grouped (Backend, Frontend, Cloud, Testing).", applySnippet = "Backend: .NET, EF Core, SQL\nFrontend: React, TypeScript\nCloud: Azure App Service, Azure SQL\nTesting: xUnit, Playwright" });

        var payload = new { generatedAtUtc = DateTime.UtcNow, suggestions };
        r.AiSuggestions = System.Text.Json.JsonSerializer.Serialize(payload);
        r.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return Ok(payload);
    }

    /// <summary>Preview AI suggestions without saving them.</summary>
    [HttpPost("{id:int}/ai-suggestions/preview")]
    [Authorize(Roles="RegisteredUser,Admin")]
    public async Task<IActionResult> AiSuggestionsPreview(int id)
    {
        var r = await _repo.GetByIdAsync(id);
        if (r == null) return NotFound();
        if (!IsAdmin() && r.UserId != UserId()) return Forbid();

        var suggestions = new List<object>();

        if (string.IsNullOrWhiteSpace(r.PersonalInfo) || r.PersonalInfo!.Length < 160)
            suggestions.Add(new { section="PersonalInfo", message="Write a concise 2–3 sentence summary highlighting role, YOE, core stack, and 1 metric.", applySnippet = "Full‑stack developer with 3+ years experience in .NET, React, and Azure. Delivered 5+ features improving performance by 25%." });

        if (string.IsNullOrWhiteSpace(r.Experience))
            suggestions.Add(new { section="Experience", message="Add 3–6 STAR-style bullet points; start with strong verbs, include metrics.", applySnippet = "• Improved API latency by 35% by optimizing EF Core queries and adding caching.\n• Led migration to Azure App Service, reducing downtime to < 5 min.\n• Mentored 2 juniors; increased team throughput by 15%." });
        else if (!(r.Experience.Any(char.IsDigit) || r.Experience.Contains("%")))
            suggestions.Add(new { section="Experience", message="Quantify at least two bullets (%, ms, $, users).", applySnippet = "• Reduced build time by 40% by parallelizing CI steps.\n• Cut error rates by 18% through input validation and logging." });

        var skills = (r.Skills ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (skills.Length < 5)
            suggestions.Add(new { section="Skills", message="List 8–12 skills grouped (Backend, Frontend, Cloud, Testing).", applySnippet = "Backend: .NET, EF Core, SQL\nFrontend: React, TypeScript\nCloud: Azure App Service, Azure SQL\nTesting: xUnit, Playwright" });

        var payload = new { generatedAtUtc = DateTime.UtcNow, suggestions };
        return Ok(payload);
    }
}