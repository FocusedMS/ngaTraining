using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureShop.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
	[HttpGet]
	public IActionResult Dashboard()
	{
		return View();
	}
}


