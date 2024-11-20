using Toilet_Clicker.Core.Domain;

namespace Toilet_Clicker.Models.Locations
{
	public class LocationDetailsViewModel
	{
		public Guid ID { get; set; }
		public string LocationName { get; set; }
		public LocationType LocationType { get; set; }
		public string LocationDescription { get; set; }
		public DateTime LocationWasMade { get; set; }
		//public List<IFormFile> Files { get; set; }
		public List<LocationImageViewModel> Image { get; set; } = new List<LocationImageViewModel>();
	}
}
