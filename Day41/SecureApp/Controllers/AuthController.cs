using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureApp.Data;
using SecureApp.Models;
using SecureApp.Security;
using System.ComponentModel.DataAnnotations;

namespace SecureApp.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly AppDbContext _db;
		private readonly IPasswordHasher _hasher;

		public AuthController(AppDbContext db, IPasswordHasher hasher)
		{
			_db = db;
			_hasher = hasher;
		}

		public record RegisterRequest([Required, EmailAddress] string Email, [Required, MinLength(12)] string Password);
		public record LoginRequest([Required, EmailAddress] string Email, [Required] string Password);

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequest request)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var exists = await _db.Users.AnyAsync(u => u.Email == request.Email);
			if (exists) return Conflict("Email already registered");
			var (hash, salt) = _hasher.Hash(request.Password);
			var user = new UserAccount { Email = request.Email, PasswordHash = hash, PasswordSalt = salt };
			_db.Users.Add(user);
			await _db.SaveChangesAsync();
			return CreatedAtAction(nameof(Register), new { id = user.Id }, new { user.Id, user.Email });
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest request)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var user = await _db.Users.Where(u => u.Email == request.Email).FirstOrDefaultAsync();
			if (user == null) return Unauthorized();
			var ok = _hasher.Verify(request.Password, user.PasswordHash, user.PasswordSalt);
			if (!ok) return Unauthorized();
			return Ok(new { message = "authenticated" });
		}
	}
}


