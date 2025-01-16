using System.ComponentModel.DataAnnotations;

namespace Toilet_Clicker.Models.Accounts
{
	public class ForgotPasswordViewModel
	{
		[Required]
		[EmailAddress]
        public string Email { get; set; }
    }
}
