using BlogCms.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Text.RegularExpressions;

namespace BlogCms.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // require auth to upload
    public class MediaController(IWebHostEnvironment env, ILogger<MediaController> log) : ControllerBase
    {
        /// <summary>
        /// Upload an image. Accepts multipart/form-data with fields: file, folder?, alt?
        /// </summary>
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]     // <-- key for Swagger
        [RequestSizeLimit(60_000_000)]        // 60 MB
        public async Task<IActionResult> Upload([FromForm] MediaUploadRequest req)
        {
            if (req.File is null || req.File.Length == 0)
                return BadRequest("No file uploaded.");

            // Resolve web root even if it was null at app start
            var webRoot = env.WebRootPath;
            if (string.IsNullOrWhiteSpace(webRoot))
                webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            var folder = string.IsNullOrWhiteSpace(req.Folder) ? "posts" : Slugify(req.Folder!);
            var targetDir = Path.Combine(webRoot, "media", folder);
            Directory.CreateDirectory(targetDir); // ensure path exists

            // sanitize filename
            var ext = Path.GetExtension(req.File.FileName).ToLowerInvariant();
            var name = Path.GetFileNameWithoutExtension(req.File.FileName);
            var safeName = Slugify(name);
            var fileName = $"{safeName}-{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(targetDir, fileName);

            // Try to process as image; if it fails, just save the stream
            try
            {
                using var input = req.File.OpenReadStream();
                using var image = await Image.LoadAsync(input); // throws if not an image

                if (image.Width > 1600)
                {
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max,
                        Size = new Size(1600, 0)
                    }));
                }
                await image.SaveAsync(fullPath);
            }
            catch (Exception ex)
            {
                log.LogWarning(ex, "Image processing failed; saving raw file.");
                using var fs = System.IO.File.Create(fullPath);
                await req.File.CopyToAsync(fs);
            }

            var url = $"/media/{folder}/{fileName}";
            return Ok(new { fileName, url, alt = req.Alt, size = req.File.Length });
        }

        private static string Slugify(string input)
        {
            input = input.Trim().ToLowerInvariant();
            input = Regex.Replace(input, @"\s+", "-");
            input = Regex.Replace(input, @"[^a-z0-9\-]", "");
            input = Regex.Replace(input, @"-+", "-");
            return input.Trim('-');
        }
    }
}
