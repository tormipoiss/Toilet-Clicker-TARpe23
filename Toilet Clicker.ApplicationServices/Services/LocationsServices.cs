using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Toilet_Clicker.Core.Domain;
using Toilet_Clicker.Core.Dto;
using Toilet_Clicker.Core.ServiceInterface;
using Toilet_Clicker.Data;

namespace Toilet_Clicker.ApplicationServices.Services
{
	public class LocationsServices : ILocationsServices
	{
		private readonly ToiletClickerContext _context;
		private readonly IFileServices _fileServices;

		public LocationsServices(ToiletClickerContext context, IFileServices fileServices)
		{
			_context = context;
			_fileServices = fileServices;
		}

		/// <summary>
		/// Get Details for one Location
		/// </summary>
		/// <param name="id">Id of location to show details of</param>
		/// <returns>resulting location</returns>
		public async Task<Location> DetailsAsync(Guid id)
		{
			var result = await _context.Locations
				.FirstOrDefaultAsync(x => x.ID == id);
			return result;
		}

		public async Task<Location> Create(LocationDto dto)
		{
			Location location = new Location();

			// set by service
			location.ID = Guid.NewGuid();
			location.LocationWasMade = DateTime.Now;

			//set by user
			location.LocationType = (Core.Domain.LocationType)dto.LocationType;
			location.LocationName = dto.LocationName;
			location.LocationDescription = dto.LocationDescription;

			//set for db
			location.CreatedAt = DateTime.Now;

			//files
			if (dto.Files != null)
			{
				_fileServices.UploadFilesToDatabaseLocation(dto, location);
			}

			await _context.Locations.AddAsync(location);
			await _context.SaveChangesAsync();

			return location;
		}

		public async Task<Location> Update(LocationDto dto)
		{
			Location location = new Location();

			// set by service
			location.ID = dto.ID;
			location.LocationWasMade = DateTime.Now;

			//set by user
			location.LocationType = (Core.Domain.LocationType)dto.LocationType;
			location.LocationName = dto.LocationName;
			location.LocationDescription = dto.LocationDescription;

			//set for db
			location.CreatedAt = DateTime.Now;

			//files
			if (dto.Files != null)
			{
				_fileServices.UploadFilesToDatabaseLocation(dto, location);
			}

			_context.Locations.Update(location);
			await _context.SaveChangesAsync();

			return location;
		}

		public async Task<Location> Delete(Guid id)
		{
			var result = await _context.Locations
				.FirstOrDefaultAsync(x => x.ID == id);
			_context.Locations.Remove(result);
			await _context.SaveChangesAsync();

			return result;
		}
	}
}
