using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Toilet_Clicker.Core.Dto
{
    public class ToiletOwnershipDto : ToiletDto
    {
        public Guid OwnershipID { get; set; }
        public ulong Power { get; set; }
        public ulong Speed { get; set; }
        public ulong Score { get; set; }
        public DateTime ToiletWasBorn { get; set; }
        //public string OwnedByPlayerProfile { get; set; } //is string but holds guid

        // image
        public List<IFormFile> Files { get; set; }
        public IEnumerable<FileToDatabaseDto> Image { get; set; } = new List<FileToDatabaseDto>();

        //db only
        public DateTime OwnershipCreatedAt { get; set; }
        public DateTime OwnershipUpdatedAt { get; set; }
    }
}
