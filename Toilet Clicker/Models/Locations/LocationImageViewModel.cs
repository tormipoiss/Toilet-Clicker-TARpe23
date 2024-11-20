namespace Toilet_Clicker.Models.Locations
{
	public class LocationImageViewModel
	{
		public Guid ImageID { get; set; }
		public string ImageTitle { get; set; }
		public byte[] ImageData { get; set; }
		public string Image { get; set; }
		public Guid? LocationID { get; set; }
	}
}
