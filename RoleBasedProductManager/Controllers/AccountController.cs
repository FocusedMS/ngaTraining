using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Models;

namespace ProductManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly SignInManager<ApplicationUser> _signInMgr;
        private readonly RoleManager<IdentityRole> _roleMgr;

        public AccountController(
            UserManager<ApplicationUser> userMgr,
            SignInManager<ApplicationUser> signInMgr,
            RoleManager<IdentityRole> roleMgr)
        {
            _userMgr = userMgr;
            _signInMgr = signInMgr;
            _roleMgr = roleMgr;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var newUser = new ApplicationUser
                {
                    UserName = registerModel.Username,
                    Email = registerModel.Email
                };

                var createResult = await _userMgr.CreateAsync(newUser, registerModel.Password);

                if (createResult.Succeeded)
                {
                    // Make sure the role exists first
                    if (!await _roleMgr.RoleExistsAsync(registerModel.Role))
                    {
                        await _roleMgr.CreateAsync(new IdentityRole(registerModel.Role));
                    }

                    // Give the user their role
                    await _userMgr.AddToRoleAsync(newUser, registerModel.Role);

                    // Log them in automatically
                    await _signInMgr.SignInAsync(newUser, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in createResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(registerModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInMgr.PasswordSignInAsync(
                    loginModel.Username, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(loginModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInMgr.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
