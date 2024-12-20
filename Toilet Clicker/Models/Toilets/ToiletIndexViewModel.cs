﻿using System.Numerics;

namespace Toilet_Clicker.Models.Toilets
{
	public class ToiletIndexViewModel
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
