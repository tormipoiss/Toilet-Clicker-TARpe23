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
	public class ToiletsServices : IToiletsServices
	{
		private readonly ToiletClickerContext _context;
        private readonly IFileServices _fileServices;

        public ToiletsServices(ToiletClickerContext context, IFileServices fileServices)
        {
            _context = context;
            _fileServices = fileServices;
        }
        
        /// <summary>
        /// Get Details for one Toilet
        /// </summary>
        /// <param name="id">Id of toilet to show details of</param>
        /// <returns>resulting toilet</returns>
        public async Task<Toilet> DetailsAsync(Guid id)
        {
            var result = await _context.Toilets
                .FirstOrDefaultAsync(x => x.ID == id);
			return await _context.Toilets
		        .Include(t => t.Location) // Include Location navigation property
		        .FirstOrDefaultAsync(t => t.ID == id);
		}

        public async Task<Toilet> Create(ToiletDto dto)
        {
            Toilet toilet = new Toilet();

            // set by service
            toilet.ID = Guid.NewGuid();
            toilet.Power = 1;
            toilet.Speed = 1;
            toilet.Score = 0;
            toilet.ToiletWasBorn = DateTime.Now;

            //set by user
            toilet.ToiletName = dto.ToiletName;
            toilet.LocationID = dto.LocationID;
            toilet.Location = dto.Location;

            //set for db
            toilet.CreatedAt = DateTime.Now;

            //files
            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, toilet);
            }

            await _context.Toilets.AddAsync(toilet);
            await _context.SaveChangesAsync();

            return toilet;
        }

		public async Task<Toilet> Update(ToiletDto dto)
		{
			Toilet toilet = new Toilet();

            // set by service
            toilet.ID = dto.ID;
			toilet.Power = dto.Power;
			toilet.Speed = dto.Speed;
			toilet.Score = dto.Score;
			toilet.ToiletWasBorn = dto.ToiletWasBorn;

			//set by user
			toilet.ToiletName = dto.ToiletName;
			toilet.LocationID = dto.LocationID;
			toilet.Location = dto.Location;

			//set for db
			toilet.CreatedAt = dto.CreatedAt;

			//files
			if (dto.Files != null)
			{
				_fileServices.UploadFilesToDatabase(dto, toilet);
			}
            _context.Toilets.Update(toilet);
            await _context.SaveChangesAsync();

            return toilet;
		}

        public async Task<Toilet> Delete(Guid id)
        {
            var result = await _context.Toilets
                .FirstOrDefaultAsync(x => x.ID == id);
            _context.Toilets.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
	}
}
