using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodingChallenge39.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Admin")]
    public class AdminController : Controller
    {
        [HttpGet("ManageTasks")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
