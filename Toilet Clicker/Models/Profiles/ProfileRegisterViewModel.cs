using Toilet_Clicker.Core.Domain;

namespace Toilet_Clicker.Models.Profiles
{
    public class ProfileRegisterViewModel
    {
        public Guid ID { get; set; }
        public string ApplicationUserID { get; set; } // 1-1
        public string ScreenName { get; set; }
        public List<ToiletOwnership> MyToilets { get; set; }
        public ProfileStatus CurrentStatus { get; set; }

        public bool ProfileType { get; set; } //true, admin, false, player
    }
}
