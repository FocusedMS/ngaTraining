using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SecureMvcAuth.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        // Show access denied page for unauthorized users
        return View();
    }
}
