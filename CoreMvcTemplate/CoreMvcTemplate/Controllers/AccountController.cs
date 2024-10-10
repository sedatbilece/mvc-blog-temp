using CoreMvcTemplate.Entities;
using CoreMvcTemplate.Entities.Data;
using CoreMvcTemplate.Models.Account;
using CoreMvcTemplate.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcTemplate.Controllers
{
    public class AccountController : Controller
	{


		private readonly AppDbContext _context;
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;

		public AccountController(AppDbContext context, SignInManager<User> signInManager, UserManager<User> userManager)
		{
			_context = context;
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public IActionResult Login(string ReturnUrl)
		{
			var model = new LoginViewModel();
			if (!string.IsNullOrEmpty(ReturnUrl))
				model.ReturnUrl = ReturnUrl;
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName);
			if (user == null)
			{
				TempData["Error"] = "Invalid username or password";
				return RedirectToAction("Login");
			}

			var result = await _signInManager.PasswordSignInAsync(user, model.Password, lockoutOnFailure: false, isPersistent: true);
			if (result.Succeeded)
			{

				if (!string.IsNullOrEmpty(model.ReturnUrl))
					return Redirect(model.ReturnUrl);
				else
					return RedirectToAction("Index");
			}
			else
			{
				TempData["Error"] = "Invalid username or password";
				return RedirectToAction("Login");
			}



		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
