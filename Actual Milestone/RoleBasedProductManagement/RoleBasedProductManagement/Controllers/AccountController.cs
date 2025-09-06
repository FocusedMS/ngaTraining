using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoleBasedProductManagement.ViewModels;

namespace RoleBasedProductManagement.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signIn;
    private readonly UserManager<IdentityUser> _users;

    public AccountController(SignInManager<IdentityUser> signIn, UserManager<IdentityUser> users)
    {
        _signIn = signIn;
        _users = users;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVm vm, string? returnUrl = null)
    {
        if (!ModelState.IsValid) return View(vm);

        var result = await _signIn.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, lockoutOnFailure: true);
        if (result.Succeeded)
        {
            TempData["Success"] = "Logged in.";
            return Redirect(returnUrl ?? Url.Action("Index", "Products")!);
        }
        ModelState.AddModelError(string.Empty, "Invalid credentials.");
        return View(vm);
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVm vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var user = new IdentityUser { UserName = vm.Email, Email = vm.Email, EmailConfirmed = true };
        var result = await _users.CreateAsync(user, vm.Password);
        if (result.Succeeded)
        {
            TempData["Success"] = "Registration successful. Please log in.";
            return RedirectToAction(nameof(Login));
        }
        foreach (var e in result.Errors) ModelState.AddModelError("", e.Description);
        return View(vm);
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signIn.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult AccessDenied() => View();
}
