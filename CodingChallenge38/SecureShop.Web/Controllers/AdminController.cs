using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureShop.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Admin")]
    public class AdminController : Controller
    {
        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
