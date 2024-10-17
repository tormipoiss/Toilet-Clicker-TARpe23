using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toilet_Clicker.Core.Domain;

namespace Toilet_Clicker.Core.ServiceInterface
{
	public interface IToiletsServices
	{
		Task<Toilet> DetailsAsync(Guid id);
	}
}
