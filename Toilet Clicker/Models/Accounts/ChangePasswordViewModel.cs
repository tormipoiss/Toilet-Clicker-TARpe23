using System.ComponentModel.DataAnnotations;

namespace Toilet_Clicker.Models.Accounts
{
	public class ChangePasswordViewModel
	{
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Type your current password: ")]
        public string CurrentPassword { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Type your new password: ")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Type it again to confirm: ")]
        public string ConfirmPassword { get; set; }

    }
}
