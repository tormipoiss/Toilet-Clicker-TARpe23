using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Toilet_Clicker.Core.Domain
{
	public class Toilet
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

		//db only
		public DateTime CreatedAt { get; set; }
	}
}
