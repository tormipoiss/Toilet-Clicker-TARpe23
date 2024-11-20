using Toilet_Clicker.Core.Domain;

namespace Toilet_Clicker.Models.Locations
{
	public enum LocationType
	{
		House, Hotel, Public, Forest, BlackHole, NeutronStar, Underwater, Office, SkibidiLand
	}
	public class LocationIndexViewModel
	{
		public Guid ID { get; set; }
		public string LocationName { get; set; }
		public LocationType LocationType { get; set; }
		public string LocationDescription { get; set; }
		public DateTime LocationWasMade { get; set; }

		//db only
		public DateTime CreatedAt { get; set; }
	}
}
