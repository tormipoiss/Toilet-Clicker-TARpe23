using System.Numerics;

namespace Toilet_Clicker.Models.Toilets
{
	public class ToiletIndexViewModel
	{
		public Guid ID { get; set; }
		public string ToiletName { get; set; }
		public BigInteger Power { get; set; }
		public BigInteger Speed { get; set; }
		public BigInteger Score { get; set; }
		public DateTime ToiletWasBorn { get; set; }

		//db only
		public DateTime CreatedAt { get; set; }
	}
}
