using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Toilet_Clicker.Core.Domain;

namespace Toilet_Clicker.Core.Dto
{
	public class ToiletDto
	{
		[Key]
		public Guid ID { get; set; }
		public string ToiletName { get; set; }
		public ulong Power { get; set; }
		public ulong PowerPrice { get; set; }
		public ulong Speed { get; set; }
		public ulong SpeedPrice { get; set; }
		public ulong Score { get; set; }
		[ForeignKey(nameof(Location.ID))]
		public Guid? LocationID { get; set; }
		public Location? Location { get; set; }
		public DateTime ToiletWasBorn { get; set; }

		// image
		
		public List<IFormFile> Files { get; set; }
		public IEnumerable<FileToDatabaseDto> Image { get; set; } = new List<FileToDatabaseDto>();
		

		//db only
		public DateTime CreatedAt { get; set; }
	}
}
