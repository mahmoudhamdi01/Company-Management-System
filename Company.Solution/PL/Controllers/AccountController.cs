using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PL.ViewModels;

namespace PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
				var User = new AppUser()
				{
					UserName = model.Email.Split('@')[0],
					FName = model.FirstName,
					LName = model.LastName,
					Email = model.Email,
					IsAgree = model.IsAgree,
				};
				var Result = await _userManager.CreateAsync(User, model.Password);
				if (Result.Succeeded)
					return RedirectToAction(nameof(Login));
				else
				{
					foreach (var error in Result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
				}
			}
				return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
			if(ModelState.IsValid)
			{
				var User = await _userManager.FindByEmailAsync(model.Email);
				if(User is not null)
				{
					var Flag = await _userManager.CheckPasswordAsync(User, model.Password);
					if(Flag)
					{
						var Result = await _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
						return RedirectToAction("Index", "Home");
					}
					else
						ModelState.AddModelError(string.Empty, "Password Is Invalid");
				}
				else
					ModelState.AddModelError(string.Empty, "Email Is not Existed");
			}
				return View(model);
        }

		public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
    }
}
