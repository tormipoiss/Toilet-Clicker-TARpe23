using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Toilet_Clicker.Core.Dto
{
	public class ToiletDto
	{
		public Guid ID { get; set; }
		public string ToiletName { get; set; }
		public BigInteger Power { get; set; }
		public BigInteger Speed { get; set; }
		public BigInteger Score { get; set; }
		public DateTime ToiletWasBorn { get; set; }

		// image
		/* 
		public List<IFormFile> Files { get; set; }
		public IEnumerable<FileToDatabaseDto> Image { get; set; } = new List<FileToDatabaseDto>();
		*/

		//db only
		public DateTime CreatedAt { get; set; }
	}
}
