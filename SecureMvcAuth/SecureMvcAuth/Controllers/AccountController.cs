using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureMvcAuth.Models;

namespace SecureMvcAuth.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string username, string password, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        // Basic validation
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            ModelState.AddModelError(string.Empty, "Username and password are required.");
            return View();
        }

        // Find user by username
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid credentials.");
            return View();
        }

        // Attempt to sign in
        var signInResult = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);
        
        if (signInResult.Succeeded)
        {
            // Get user roles and redirect accordingly
            var userRoles = await _userManager.GetRolesAsync(user);
            
            if (userRoles.Contains("Admin"))
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                return RedirectToAction("UserProfile", "Profile");
            }
        }

        // If we get here, login failed
        ModelState.AddModelError(string.Empty, "Invalid credentials.");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
