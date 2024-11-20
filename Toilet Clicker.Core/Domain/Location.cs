using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toilet_Clicker.Core.Domain
{
	public enum LocationType
	{
		House, Hotel, Public, Forest, BlackHole, NeutronStar, Underwater, Office, SkibidiLand
	}
	public class Location
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