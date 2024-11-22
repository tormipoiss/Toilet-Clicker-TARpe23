using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Toilet_Clicker.Core.Domain
{
	public enum LocationType
	{
		House, Hotel, Public, Forest, BlackHole, NeutronStar, Underwater, Office, SkibidiLand
	}
	public class Location
	{
		[Key]
		public Guid ID { get; set; }
		public string LocationName { get; set; }
		public LocationType LocationType { get; set; }
		public string LocationDescription { get; set; }
		public DateTime LocationWasMade { get; set; }
		public ICollection<Toilet>? Toilets { get; set; } = new List<Toilet>();

		//db only
		public DateTime CreatedAt { get; set; }
	}
}