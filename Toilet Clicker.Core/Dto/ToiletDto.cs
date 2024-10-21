using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Toilet_Clicker.Core.Dto
{
	public class ToiletDto
	{
		public Guid ID { get; set; }
		public string ToiletName { get; set; }
		public ulong Power { get; set; }
		public ulong Speed { get; set; }
		public ulong Score { get; set; }
		public DateTime ToiletWasBorn { get; set; }

		// image
		
		public List<IFormFile> Files { get; set; }
		public IEnumerable<FileToDatabaseDto> Image { get; set; } = new List<FileToDatabaseDto>();
		

		//db only
		public DateTime CreatedAt { get; set; }
	}
}
