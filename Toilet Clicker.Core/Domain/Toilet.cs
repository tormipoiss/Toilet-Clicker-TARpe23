using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toilet_Clicker.Core.Domain
{
	public class Toilet
	{
		public enum UpgradeList
		{
			Power, Speed, Size
		}
		public Guid ID { get; set; }
		public int ToiletName { get; set; }
		public List<UpgradeList> Upgrades { get; set; }
		public DateTime ToiletWasCreated { get; set; }

		//db only
		public DateTime CreatedAt { get; set; }
	}
}
