using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ResumeApi.Models;
using ResumeApi.Services;
using System.ComponentModel.DataAnnotations;

namespace ResumeApi.Controllers;

/// <summary>
/// Authentication endpoints for registering and logging in.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userMgr;
    private readonly SignInManager<ApplicationUser> _signInMgr;
    private readonly JwtService _jwt;

    public AuthController(UserManager<ApplicationUser> userMgr, SignInManager<ApplicationUser> signInMgr, JwtService jwt)
    {
        _userMgr = userMgr; _signInMgr = signInMgr; _jwt = jwt;
    }

    public record RegisterDto([Required,EmailAddress] string Email, [Required] string Password, string? FullName);
    public record LoginDto([Required,EmailAddress] string Email, [Required] string Password);

    /// <summary>Register a new user account.</summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new ApplicationUser { UserName = dto.Email, Email = dto.Email, FullName = dto.FullName };
        var result = await _userMgr.CreateAsync(user, dto.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);

        await _userMgr.AddToRoleAsync(user, "RegisteredUser");
        return Ok(new { message = "Registered" });
    }

    /// <summary>Login with email and password to receive a JWT.</summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userMgr.FindByEmailAsync(dto.Email);
        if (user is null) return Unauthorized();

        var check = await _signInMgr.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!check.Succeeded) return Unauthorized();

        var roles = await _userMgr.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? "RegisteredUser";
        var token = _jwt.CreateToken(user, role);
        return Ok(new { token, role, email = user.Email, name = user.FullName });
    }
}