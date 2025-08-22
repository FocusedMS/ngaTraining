using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace SecureShop.Controllers;

public class LoginViewModel
{
	[Required]
	[EmailAddress]
	public string Email { get; set; } = string.Empty;

	[Required]
	[DataType(DataType.Password)]
	[StringLength(100, MinimumLength = 8)]
	[RegularExpression("^(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9]).{8,}$", ErrorMessage = "Password must be 8+ chars with uppercase, number, and special.")]
	public string Password { get; set; } = string.Empty;

	public bool RememberMe { get; set; }
}

public class RegisterViewModel
{
	[Required]
	[EmailAddress]
	public string Email { get; set; } = string.Empty;

	[Required]
	[DataType(DataType.Password)]
	[StringLength(100, MinimumLength = 8)]
	[RegularExpression("^(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9]).{8,}$", ErrorMessage = "Password must be 8+ chars with uppercase, number, and special.")]
	public string Password { get; set; } = string.Empty;

	[Required]
	[Compare("Password")]
	public string ConfirmPassword { get; set; } = string.Empty;
}

public class AccountController : Controller
{
	private readonly SignInManager<IdentityUser> _signInManager;
	private readonly UserManager<IdentityUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;

	public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
	{
		_signInManager = signInManager;
		_userManager = userManager;
		_roleManager = roleManager;
	}

	[HttpGet]
	[AllowAnonymous]
	public IActionResult Login(string? returnUrl = null)
	{
		ViewData["ReturnUrl"] = returnUrl;
		return View();
	}

	[HttpPost]
	[AllowAnonymous]
	[EnableRateLimiting("login")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
	{
		if (!ModelState.IsValid)
		{
			return View(model);
		}

		var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
		if (result.Succeeded)
		{
			return RedirectToLocal(returnUrl);
		}
		if (result.IsLockedOut)
		{
			ModelState.AddModelError(string.Empty, "Account locked. Try later.");
			return View(model);
		}

		ModelState.AddModelError(string.Empty, "Invalid login attempt.");
		return View(model);
	}

	[HttpGet]
	[AllowAnonymous]
	public IActionResult Register()
	{
		return View();
	}

	[HttpPost]
	[AllowAnonymous]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Register(RegisterViewModel model)
	{
		if (!ModelState.IsValid)
		{
			return View(model);
		}

		var user = new IdentityUser { UserName = model.Email, Email = model.Email };
		var result = await _userManager.CreateAsync(user, model.Password);
		if (result.Succeeded)
		{
			if (!await _roleManager.RoleExistsAsync("Customer"))
			{
				await _roleManager.CreateAsync(new IdentityRole("Customer"));
			}
			await _userManager.AddToRoleAsync(user, "Customer");
			await _signInManager.SignInAsync(user, isPersistent: false);
			return RedirectToAction("Index", "Products");
		}
		foreach (var error in result.Errors)
		{
			ModelState.AddModelError(string.Empty, error.Description);
		}
		return View(model);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();
		return RedirectToAction("Login");
	}

	private IActionResult RedirectToLocal(string? returnUrl)
	{
		if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
		{
			return Redirect(returnUrl);
		}
		return RedirectToAction("Index", "Products");
	}
}


