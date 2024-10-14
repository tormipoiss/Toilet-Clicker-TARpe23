namespace Toilet_Clicker.Models.Toilets
{
	public class ToiletIndexViewModel
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
