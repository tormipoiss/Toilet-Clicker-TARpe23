using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Toilet_Clicker.Core.Domain;
using Toilet_Clicker.Core.ServiceInterface;
using Toilet_Clicker.Data;

namespace Toilet_Clicker.ApplicationServices.Services
{
	public class ToiletsServices : IToiletsServices
	{
		private readonly ToiletClickerContext _context;
        // private readonly IFileServices _fileServices;

        public ToiletsServices(ToiletClickerContext context/*, IFileServices fileServices*/)
        {
            _context = context;
            // _fileServices = fileServices;
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
            return result;
        }
    }
}
