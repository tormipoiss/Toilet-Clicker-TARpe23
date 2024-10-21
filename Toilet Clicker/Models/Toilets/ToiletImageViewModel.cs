namespace Toilet_Clicker.Models.Toilets
{
	public class ToiletImageViewModel
	{
		public Guid ImageID { get; set; }
		public string ImageTitle { get; set; }
		public byte[] ImageData { get; set; }
		public string Image { get; set; }
		public Guid? ToiletID { get; set; }
	}
}