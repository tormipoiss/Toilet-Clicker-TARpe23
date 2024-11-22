using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Toilet_Clicker.Core.Domain;

namespace Toilet_Clicker.Core.Dto
{
	public enum LocationType
	{
		House, Hotel, Public, Forest, BlackHole, NeutronStar, Underwater, Office, SkibidiLand
	}
	public class LocationDto
	{
		[Key]
		public Guid ID { get; set; }
		public string LocationName { get; set; }
		public LocationType LocationType { get; set; }
		public string LocationDescription { get; set; }
		public DateTime LocationWasMade { get; set; }
		public ICollection<Toilet>? Toilets { get; set; } = new List<Toilet>();

		// image
		public List<IFormFile> Files { get; set; }
		public IEnumerable<FileToDatabaseDto> Image { get; set; } = new List<FileToDatabaseDto>();

		//db only
		public DateTime CreatedAt { get; set; }
	}
}
