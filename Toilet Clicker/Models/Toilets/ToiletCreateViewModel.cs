using System.ComponentModel.DataAnnotations.Schema;

namespace Toilet_Clicker.Models.Toilets
{
	public class ToiletCreateViewModel
	{
		public Guid? ID { get; set; }
		public string ToiletName { get; set; }
		public ulong Power { get; set; }
		public ulong PowerPrice { get; set; }
		public ulong Speed { get; set; }
		public ulong SpeedPrice { get; set; }
		public ulong Score { get; set; }
		public Guid? LocationID { get; set; }
		public DateTime ToiletWasBorn { get; set; }
        public List<IFormFile> Files { get; set; }
        public List<ToiletImageViewModel> Image { get; set; } = new List<ToiletImageViewModel>();

        //db only
        public DateTime CreatedAt { get; set; }
	}
}
