using Toilet_Clicker.Core.Dto;

namespace Toilet_Clicker.Models.Stories
{
    public class ToiletOwnershipFromStoryViewModel
    {
        public string PlayerProfileGuid { get; set; }
        public string StoryGUID { get; set; }
        public ToiletOwnershipDto AddedToilet { get; set; }
    }
}
