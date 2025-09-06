using BlogCms.Api.Data;
using BlogCms.Api.Domain.Entities;
using BlogCms.Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogCms.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class ModerationController : ControllerBase
{
    private readonly BlogDbContext db;
    public ModerationController(BlogDbContext db) { this.db = db; }

    [HttpGet("posts")]
    public async Task<IEnumerable<PostResponse>> Pending([FromQuery] string status = "PendingReview")
    {
        var st = Enum.TryParse<PostStatus>(status, out var s) ? s : PostStatus.PendingReview;
        var items = await db.Posts.Where(p => p.Status == s).OrderBy(p => p.CreatedAt).ToListAsync();
        return items.Select(p => new PostResponse {
            Id = p.Id, Title = p.Title, Slug = p.Slug, Excerpt = p.Excerpt,
            ContentHtml = p.ContentHtml, CoverImageUrl = p.CoverImageUrl, Status = p.Status.ToString(),
            AuthorId = p.AuthorId, CategoryId = p.CategoryId, PublishedAt = p.PublishedAt,
            CreatedAt = p.CreatedAt, UpdatedAt = p.UpdatedAt
        });
    }

    [HttpPost("posts/{id:int}/approve")]
    public async Task<IActionResult> Approve(int id)
    {
        var p = await db.Posts.FirstOrDefaultAsync(x => x.Id == id);
        if (p is null) return NotFound();
        if (p.Status != PostStatus.PendingReview) return BadRequest("Post is not pending review.");

        p.Status = PostStatus.Published;
        p.PublishedAt = DateTime.UtcNow;
        p.UpdatedAt = DateTime.UtcNow;

        db.AuditLogs.Add(new AuditLog { ActorId = 0, Action = "Approve", Entity = "Post", EntityId = p.Id });
        await db.SaveChangesAsync();
        return Ok(new { message = "approved" });
    }

    [HttpPost("posts/{id:int}/reject")]
    public async Task<IActionResult> Reject(int id, ModerationDecisionRequest body)
    {
        var p = await db.Posts.FirstOrDefaultAsync(x => x.Id == id);
        if (p is null) return NotFound();
        if (string.IsNullOrWhiteSpace(body.Reason) || body.Reason.Trim().Length < 10)
            return BadRequest("Reason must be at least 10 characters.");

        p.Status = PostStatus.Rejected;
        p.UpdatedAt = DateTime.UtcNow;
        db.AuditLogs.Add(new AuditLog {
            ActorId = 0,
            Action = "Reject",
            Entity = "Post",
            EntityId = p.Id,
            PayloadJson = System.Text.Json.JsonSerializer.Serialize(new { body.Reason })
        });
        await db.SaveChangesAsync();
        return Ok(new { message = "rejected", reason = body.Reason });
    }
}
