using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toilet_Clicker.Core.Domain
{
    public enum ProfileStatus
    {
        Active, Abandoned, Deactivated, Locked, Banned, ManualReviewNecessary
    }
    public class PlayerProfile
    {
        public Guid ID { get; set; }
        public Guid ApplicationUserID { get; set; }
        public string ScreenName { get; set; }
        public List<ToiletOwnership> MyToilets { get; set; }
        public ProfileStatus CurrentStatus { get; set; }
        public bool ProfileType { get; set; } //true, admin, false, player
        //dbonly
        public DateTime ProfileCreatedAt { get; set; }
        public DateTime ProfileModifiedAt { get; set; }
        public DateTime ProfileAttributedToAnAccountUserAt { get; set; }
        public DateTime ProfileStatusLastChangedAt { get; set; }
    }
}
