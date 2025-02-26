using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LibraryManager.Models;
using LibraryManager.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // LOGIN
        [HttpGet]
        public IActionResult Login()
        {
            return PartialView("_LoginModal", new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_LoginModal", model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return PartialView("_LoginModal", model);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                // If returnUrl exists and is a local URL, use it; otherwise, refresh page
                return Json(new { success = true, returnUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : "" });
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return PartialView("_LoginModal", model);
        }



        // REGISTER
        [HttpGet]
        public IActionResult Register()
        {
            return PartialView("_RegisterModal", new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Json(new { success = true });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return PartialView("_RegisterModal", model);
        }


        // LOGOUT
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("BooksTable", "Books");
        }
    }
}
