using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureMvcAuth.Models;

namespace SecureMvcAuth.Controllers;

[Authorize(Roles = "User,Admin")]
public class ProfileController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ProfileController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> UserProfile()
    {
        // Get current user's profile
        var currentUser = await _userManager.GetUserAsync(User);
        return View(currentUser);
    }
}
