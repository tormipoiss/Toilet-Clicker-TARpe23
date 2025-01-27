using Microsoft.AspNetCore.Mvc;
using Toilet_Clicker.ApplicationServices.Services;
using Toilet_Clicker.Core.Domain;
using Toilet_Clicker.Core.Dto.AccountsDtos;
using Toilet_Clicker.Core.ServiceInterface;
using Toilet_Clicker.Data;

namespace Toilet_Clicker.Controllers
{
    public class PlayerProfilesController : Controller
    {
        private readonly ToiletClickerContext _context;
        private readonly IToiletsServices _toiletsServices;
        public PlayerProfilesController(ToiletClickerContext context, ToiletsServices toiletsServices)
        {
            _context = context;
            _toiletsServices = toiletsServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(_context.PlayerProfiles.OrderByDescending(x => x.ScreenName));
        }

        [HttpGet]
        public async Task<IActionResult> NewProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewProfile(PlayerProfileDto dto)
        {
            string userid = TempData["NewUserID"].ToString();
            //if (ViewData["NewUserID"] == null)
            if (userid == null)
            {
                return View("Index");
            }

            var newprofile = new PlayerProfile()
            {
                ID = dto.ID,
                ApplicationUserID = TempData["NewUserID"].ToString(),
                ScreenName = dto.ScreenName,
                MyToilets = new List<ToiletOwnership>(),
                CurrentStatus = ProfileStatus.Active,
                ProfileType = false,
                ProfileStatusLastChangedAt = DateTime.UtcNow,
                ProfileAttributedToAnAccountUserAt = DateTime.UtcNow,
                ProfileCreatedAt = DateTime.UtcNow,
                ProfileModifiedAt = DateTime.UtcNow,
            };
            ToiletOwnership startingToilet = new();
            startingToilet = await ToiletsController.NewRandomToiletOwnership(startingToilet);// call controller method
            newprofile.MyToilets.Add(startingToilet);
            await _context.SaveChangesAsync();
            // TODO: caLll create ownership method from titanservices.
            // return here, into startingtitan, the generated titan
            // append this titan to the user
            // TODO: implement random titanownership for the new userprofile.
            var result = await _context.PlayerProfiles.AddAsync(newprofile);
            await _context.SaveChangesAsync();

            //Code provided by: Mel Kosk
            var user = await _context.Users.FindAsync(newprofile.ApplicationUserID);
            user.PlayerProfileID = dto.ID;
            await _context.SaveChangesAsync();

            if (result == null)
            {
                return View("Index");
            }

            return View("~/Views/Home/Index.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> NewPlayerProfile()
        {
            return View();
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
