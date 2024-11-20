using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Toilet_Clicker.Core.Domain;
using Toilet_Clicker.Data;
using Toilet_Clicker.Models.Accounts;

namespace Toilet_Clicker.Controllers
{
	public class AccountsController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly ToiletClickerContext _context;

		public AccountsController
			(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			ToiletClickerContext context
			)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> AddPassword()
		{
			var user = await _userManager.GetUserAsync(User);
			var userHasPassword = await _userManager.HasPasswordAsync(user);
			if ( userHasPassword )
			{
				RedirectToAction("ChangePassword");
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddPassword(AddPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.GetUserAsync(User);
				var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
				if (!result.Succeeded)
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
					return View();
				}
				await _signInManager.RefreshSignInAsync(user);
				return View("AddPasswordConfirmation");
			}
			return View(model);
		}
	}
}
