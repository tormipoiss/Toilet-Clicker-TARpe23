using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Toilet_Clicker.Core.Domain
{
	public class Toilet
	{
		public Guid ID { get; set; }
		public string ToiletName { get; set; }
        public ulong Power { get; set; }
		public ulong Speed { get; set; }
		public ulong Score { get; set; }
		public DateTime ToiletWasBorn { get; set; }

		//db only
		public DateTime CreatedAt { get; set; }
	}
}
