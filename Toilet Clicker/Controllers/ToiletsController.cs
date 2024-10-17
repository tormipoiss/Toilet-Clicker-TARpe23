using Microsoft.AspNetCore.Mvc;

namespace Toilet_Clicker.Controllers
{
	public class ToiletsController : Controller
	{
		/*
		 * ToiletsController controls all functions for toilets, including, missions.
		 */
		public IActionResult Index()
		{
			return View();
		}
	}
}
