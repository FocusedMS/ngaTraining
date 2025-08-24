using CodingChallenge39.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace YourProjectNamespace.Controllers
{
    [Authorize(Roles = "User,Admin")]
    [Route("User")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UserController(ApplicationDbContext db) => _db = db;

        [HttpGet("TaskList")]
        public async Task<IActionResult> TaskList()
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var tasks = await _db.TaskItems.AsNoTracking()
                .Where(t => t.OwnerUserId == uid)
                .OrderByDescending(t => t.CreatedAtUtc)
                .ToListAsync();

            return View(tasks);
        }
    }
}
