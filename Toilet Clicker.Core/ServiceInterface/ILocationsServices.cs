using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toilet_Clicker.Core.Domain;
using Toilet_Clicker.Core.Dto;

namespace Toilet_Clicker.Core.ServiceInterface
{
	public interface ILocationsServices
	{
		Task<Location> DetailsAsync(Guid id);
		Task<Location> Create(LocationDto dto);
		Task<Location> Update(LocationDto dto);
		Task<Location> Delete(Guid id);
	}
}
