using System.ComponentModel.DataAnnotations;

namespace Toilet_Clicker.Models.Accounts
{
	public class LoginViewModel
	{
        [Required]
        [EmailAddress]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Display(Name = "Remember this account?")]
        public bool RememberMe { get; set; }
        public string? ReturnURL { get; set; }
    }
}
