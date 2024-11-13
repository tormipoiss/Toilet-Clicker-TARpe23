using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toilet_Clicker.Core.Domain;
using Toilet_Clicker.Core.Dto;

namespace Toilet_Clicker.Core.ServiceInterface
{
	public interface IToiletsServices
	{
		Task<Toilet> DetailsAsync(Guid id);
		Task<Toilet> Create(ToiletDto dto);
		Task<Toilet> Update(ToiletDto dto);
		Task<Toilet> Delete(Guid id);
	}
}
