using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toilet_Clicker.Core.Domain;
using Toilet_Clicker.Core.Dto.AccountsDtos;

namespace Toilet_Clicker.Core.ServiceInterface
{
    public interface IAccountsServices
    {
		Task<ApplicationUser> ConfirmEmail(string userId, string token);
		Task<ApplicationUser> Register(ApplicationUserDto dto);
		Task<ApplicationUser> Login(LoginDto dto);
	}
}
