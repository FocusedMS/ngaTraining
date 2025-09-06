using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using BlogCms.Api.Data;
using BlogCms.Api.Domain.Entities;
using BlogCms.Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogCms.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // default: auth required; selectively allow anon below
public class PostsController : ControllerBase
{
    private readonly BlogDbContext _db;
    public PostsController(BlogDbContext db) => _db = db;

    // -------------------- Public read (Published only) ----------------------

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<PostResponse>>> ListPublished([FromQuery] string? search = null)
    {
        var q = _db.Posts.AsNoTracking()
            .Where(p => p.Status == PostStatus.Published);

        if (!string.IsNullOrWhiteSpace(search))
        {
            q = q.Where(p => p.Title.Contains(search) || (p.Excerpt ?? "").Contains(search));
        }

        var items = await q
            .OrderByDescending(p => p.PublishedAt)
            .Select(p => new PostResponse
            {
                Id = p.Id,
                Title = p.Title,
                Slug = p.Slug,
                Excerpt = p.Excerpt,
                ContentHtml = p.ContentHtml,
                CoverImageUrl = p.CoverImageUrl,
                Status = p.Status.ToString(),
                AuthorId = p.AuthorId,
                CategoryId = p.CategoryId,
                PublishedAt = p.PublishedAt,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            })
            .ToListAsync();

        return Ok(items);
    }

    // GET: api/Posts/{slug}
    [HttpGet("{slug}", Order = 100)]
    [AllowAnonymous]
    public async Task<ActionResult<PostResponse>> GetBySlug(string slug)
    {
        var p = await _db.Posts.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Slug == slug && x.Status == PostStatus.Published);

        if (p is null) return NotFound(new { message = "Post not found" });
        return Ok(MapToResponse(p));
    }

    // -------------------- Authenticated (owner/admin) -----------------------

    // GET: api/Posts/me
    [HttpGet("me", Order = -1)]
    public async Task<ActionResult<IEnumerable<PostResponse>>> GetMine()
    {
        var uid = GetActorId();
        if (uid <= 0) return Unauthorized(new { message = "Invalid token" });

        var items = await _db.Posts.AsNoTracking()
            .Where(p => p.AuthorId == uid)
            .OrderByDescending(p => p.UpdatedAt)
            .Select(p => new PostResponse
            {
                Id = p.Id,
                Title = p.Title,
                Slug = p.Slug,
                Excerpt = p.Excerpt,
                ContentHtml = p.ContentHtml,
                CoverImageUrl = p.CoverImageUrl,
                Status = p.Status.ToString(),
                AuthorId = p.AuthorId,
                CategoryId = p.CategoryId,
                PublishedAt = p.PublishedAt,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            })
            .ToListAsync();

        return Ok(items);
    }

    // GET: api/Posts/{id}
    [HttpGet("{id:int}", Order = -1)]
    [Authorize(Roles = "Blogger,Admin")]
    public async Task<ActionResult<PostResponse>> GetById(int id)
    {
        var uid = GetActorId();
        if (uid <= 0) return Unauthorized(new { message = "Invalid token" });

        var p = await _db.Posts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (p is null) return NotFound(new { message = "Post not found" });

        if (p.AuthorId != uid && !User.IsInRole("Admin")) return Forbid();
        return Ok(MapToResponse(p));
    }

    // POST: api/Posts  (create draft)
    [HttpPost]
    [Authorize(Roles = "Blogger,Admin")]
    public async Task<IActionResult> Create([FromBody] PostCreateRequest body)
    {
        if (body is null) return BadRequest(new { message = "Body is required" });

        var uid = GetActorId();
        if (uid <= 0) return Unauthorized(new { message = "Invalid token" });

        var title = body.Title?.Trim();
        if (string.IsNullOrWhiteSpace(title))
            return BadRequest(new { message = "Title is required" });

        // slug + de-dupe
        var slug = Slugify(title);
        if (await _db.Posts.AnyAsync(p => p.Slug == slug))
        {
            var suffix6 = Guid.NewGuid().ToString("N")[..6];
            slug = $"{slug}-{suffix6}";
        }

        // pick a valid category id (or create a default one if none exist)
        var fallbackCategoryId = await _db.Categories
            .OrderBy(c => c.Id)
            .Select(c => c.Id)
            .FirstOrDefaultAsync();

        if (fallbackCategoryId == 0)
        {
            var general = new Category { Name = "General", Slug = "general" };
            _db.Categories.Add(general);
            await _db.SaveChangesAsync(); // get real Id
            fallbackCategoryId = general.Id;
        }

        var categoryId = fallbackCategoryId;
        if (body.CategoryId.HasValue && body.CategoryId.Value > 0)
        {
            var exists = await _db.Categories.AnyAsync(c => c.Id == body.CategoryId.Value);
            if (exists) categoryId = body.CategoryId.Value;
        }

        var post = new Post
        {
            Title = title,
            Slug = slug,
            Excerpt = body.Excerpt,
            ContentHtml = body.ContentHtml,
            CoverImageUrl = string.IsNullOrWhiteSpace(body.CoverImageUrl) ? null : body.CoverImageUrl,
            Status = PostStatus.Draft,
            AuthorId = uid,
            CategoryId = categoryId, // valid FK
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Posts.Add(post);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = post.Id }, MapToResponse(post));
    }

    // PUT: api/Posts/{id}  (update draft)
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Blogger,Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] PostUpdateRequest body)
    {
        var uid = GetActorId();
        if (uid <= 0) return Unauthorized(new { message = "Invalid token" });

        var p = await _db.Posts.FirstOrDefaultAsync(x => x.Id == id);
        if (p is null) return NotFound(new { message = "Post not found" });

        if (p.AuthorId != uid && !User.IsInRole("Admin")) return Forbid();

        if (!string.IsNullOrWhiteSpace(body.Title) && body.Title != p.Title)
        {
            p.Title = body.Title.Trim();
            var slug = Slugify(p.Title);
            if (await _db.Posts.AnyAsync(x => x.Id != id && x.Slug == slug))
            {
                var suffix6 = Guid.NewGuid().ToString("N")[..6];
                slug = $"{slug}-{suffix6}";
            }
            p.Slug = slug;
        }

        if (body.Excerpt != null) p.Excerpt = body.Excerpt;
        if (body.ContentHtml != null) p.ContentHtml = body.ContentHtml;
        if (body.CoverImageUrl != null)
            p.CoverImageUrl = string.IsNullOrWhiteSpace(body.CoverImageUrl) ? null : body.CoverImageUrl;

        if (body.CategoryId.HasValue && body.CategoryId.Value > 0)
        {
            var exists = await _db.Categories.AnyAsync(c => c.Id == body.CategoryId.Value);
            if (exists) p.CategoryId = body.CategoryId.Value;
        }

        p.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return Ok(new { success = true, message = "Draft updated", postId = p.Id });
    }

    // POST: api/Posts/{id}/submit  (send for review)
    [HttpPost("{id:int}/submit")]
    [Authorize(Roles = "Blogger,Admin")]
    public async Task<IActionResult> SubmitForReview(int id)
    {
        var uid = GetActorId();
        if (uid <= 0) return Unauthorized(new { message = "Invalid token" });

        var p = await _db.Posts.FirstOrDefaultAsync(x => x.Id == id);
        if (p is null) return NotFound(new { message = "Post not found" });
        if (p.AuthorId != uid && !User.IsInRole("Admin")) return Forbid();

        if (p.Status != PostStatus.Draft && p.Status != PostStatus.Rejected)
            return BadRequest(new { message = "Only drafts or rejected posts can be submitted." });

        p.Status = PostStatus.PendingReview;
        p.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return Ok(new { success = true, message = "Submitted for review", postId = p.Id });
    }

    // DELETE: api/Posts/{id}
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Blogger,Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var uid = GetActorId();
        if (uid <= 0) return Unauthorized(new { message = "Invalid token" });

        var p = await _db.Posts.FirstOrDefaultAsync(x => x.Id == id);
        if (p is null) return NotFound(new { message = "Post not found" });

        if (p.AuthorId != uid && !User.IsInRole("Admin")) return Forbid();

        _db.Posts.Remove(p);
        await _db.SaveChangesAsync();
        return Ok(new { success = true, message = "Post deleted", postId = id });
    }

    // -------------------- helpers --------------------

    private static PostResponse MapToResponse(Post p) => new()
    {
        Id = p.Id,
        Title = p.Title,
        Slug = p.Slug,
        Excerpt = p.Excerpt,
        ContentHtml = p.ContentHtml,
        CoverImageUrl = p.CoverImageUrl,
        Status = p.Status.ToString(),
        AuthorId = p.AuthorId,
        CategoryId = p.CategoryId,
        PublishedAt = p.PublishedAt,
        CreatedAt = p.CreatedAt,
        UpdatedAt = p.UpdatedAt
    };

    private int GetActorId()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                   ?? User.FindFirst("sub")?.Value
                   ?? User.FindFirst(ClaimTypes.Name)?.Value;
        return int.TryParse(idClaim, out var id) ? id : 0;
    }

    private static string Slugify(string? title)
    {
        var t = (title ?? "").Trim().ToLowerInvariant();
        t = Regex.Replace(t, @"[^a-z0-9]+", "-");
        t = Regex.Replace(t, @"-+", "-").Trim('-');
        if (string.IsNullOrWhiteSpace(t))
        {
            var g12 = Guid.NewGuid().ToString("N")[..12];
            return $"post-{g12}";
        }
        return t;
    }
}
