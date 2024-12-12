using Microsoft.AspNetCore.Mvc;
using Toilet_Clicker.Core.Domain;
using Toilet_Clicker.Data;

namespace Toilet_Clicker.Controllers
{
    public class PlayerProfilesController : Controller
    {
        private readonly ToiletClickerContext _context;
        public PlayerProfilesController(ToiletClickerContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(_context.PlayerProfiles.OrderByDescending(x => x.ScreenName));
        }
        //[HttpGet]
        // public async Task<Player>

        //[HttpGet]
        // method that gets the user the view for playerprofile info

        //[HttpPost]
        // method to generate new playerprofile, info is gotten from a view
        // that the player is directed to, right after confirmation.

        //[HttpGet]
        // method FOR ADMINS to get view for player profile modification

        //[HttpPost]
        // method FOR USERS to get SETTINGS view for player confirmation

        //[HttpGet]
        // something something
    }
}
