using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Toilet_Clicker.Core.Domain
{
	public class ApplicationUser : IdentityUser
	{
        public string City { get; set; }
    }
}
