using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Toilet_Clicker.Core.Domain;
using Toilet_Clicker.Core.ServiceInterface;

namespace Toilet_Clicker.ApplicationServices.Services
{
    public class PlayerProfilesServices : IPlayerProfilesServices
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PlayerProfilesServices
            (
                UserManager<ApplicationUser> userManager
            )
        {
            _userManager = userManager;
        }

        public async Task<PlayerProfile> Create(string useridfor)
        {
            var user = await _userManager.FindByIdAsync(useridfor);
            string userid = user.Id;
            var profile = new PlayerProfile()
            {
                ID = new Guid(),
                ApplicationUserID = userid,
                ScreenName = "",
                MyToilets = new List<ToiletOwnership>(),
                CurrentStatus = ProfileStatus.Active,
                ProfileType = false,
                ProfileStatusLastChangedAt = DateTime.UtcNow,
                ProfileAttributedToAnAccountUserAt = DateTime.UtcNow,
                ProfileCreatedAt = DateTime.UtcNow,
                ProfileModifiedAt = DateTime.UtcNow,
            };
            return profile;
        }
    }
}
