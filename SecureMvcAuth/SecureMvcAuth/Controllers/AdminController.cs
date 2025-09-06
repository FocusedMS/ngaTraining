using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureMvcAuth.Models;

namespace SecureMvcAuth.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Dashboard()
    {
        // Get all users for admin dashboard
        var allUsers = _userManager.Users.ToList();
        return View(allUsers);
    }
}
