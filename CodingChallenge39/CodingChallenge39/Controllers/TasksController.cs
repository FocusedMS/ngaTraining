using CodingChallenge39.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using YourProjectNamespace.Models;

namespace YourProjectNamespace.Controllers
{
    [Authorize] 
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TasksController(ApplicationDbContext db) => _db = db;

        // GET: /Tasks/Create
        [HttpGet]
        public IActionResult Create() => View();

        // POST: /Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF protection
        public async Task<IActionResult> Create(TaskItem input)
        {
            if (!ModelState.IsValid) return View(input);

            input.OwnerUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            input.CreatedAtUtc = DateTime.UtcNow;

            _db.TaskItems.Add(input);
            await _db.SaveChangesAsync();

            // Redirect to "Dashboard" (User TaskList)
            return RedirectToAction("TaskList", "User");
        }

        // GET: /Tasks/Edit/5
        [HttpGet]
        [Authorize(Policy = "CanEditTaskPolicy")]
        public async Task<IActionResult> Edit(int id)
        {
            var t = await _db.TaskItems.FindAsync(id);
            if (t == null) return NotFound();

            // Users (non-admin) can only edit their own tasks
            if (!User.IsInRole("Admin") &&
                t.OwnerUserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return Forbid();

            return View(t);
        }

        // POST: /Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CanEditTaskPolicy")]
        public async Task<IActionResult> Edit(int id, TaskItem updated)
        {
            if (id != updated.Id) return BadRequest();
            if (!ModelState.IsValid) return View(updated);

            var existing = await _db.TaskItems.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return NotFound();

            if (!User.IsInRole("Admin") &&
                existing.OwnerUserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return Forbid();

            existing.Title = updated.Title;
            existing.Description = updated.Description;
            existing.IsCompleted = updated.IsCompleted;

            await _db.SaveChangesAsync();
            return RedirectToAction("TaskList", "User");
        }
    }
}
