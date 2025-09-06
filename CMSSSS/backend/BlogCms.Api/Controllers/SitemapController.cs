using BlogCms.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BlogCms.Api.Controllers;

[ApiController]
public class SitemapController(BlogDbContext db, IConfiguration cfg) : ControllerBase
{
    [HttpGet("/sitemap.xml")]
    [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
    public async Task<IActionResult> Sitemap()
    {
        var baseUrl = cfg["Site:BaseUrl"] ?? "http://localhost:5173";
        var urls = await db.Posts.AsNoTracking().Where(p => p.Status == Domain.Entities.PostStatus.Published)
            .OrderByDescending(p => p.PublishedAt).Select(p => $"{baseUrl}/post/{p.Slug}").ToListAsync();

        var sb = new StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
        foreach (var u in urls) { sb.AppendLine($"  <url><loc>{u}</loc></url>"); }
        sb.AppendLine("</urlset>");
        return Content(sb.ToString(), "application/xml", Encoding.UTF8);
    }

    [HttpGet("/robots.txt")]
    [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any)]
    public IActionResult Robots()
    {
        var lines = new[] {
            "User-agent: *",
            "Disallow: /preview",
            "Allow: /",
            "Sitemap: /sitemap.xml"
        };
        return Content(string.Join("\n", lines), "text/plain", Encoding.UTF8);
    }
}
