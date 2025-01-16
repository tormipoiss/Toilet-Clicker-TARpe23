using System.ComponentModel.DataAnnotations;

namespace Toilet_Clicker.Models.Accounts
{
	public class RegisterViewModel
	{
        [Required]
        [EmailAddress]
		public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm your password by typing it again: ")]
        [Compare("Password", ErrorMessage = "Password and its confirmation do not match. Please try again")]
        public string ConfirmPassword { get; set; }
        public string City { get; set; }
    }
}
