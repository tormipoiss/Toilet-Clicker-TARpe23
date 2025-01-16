using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toilet_Clicker.Core.Domain
{
    public class ToiletOwnership : Toilet
    {
        public Guid OwnershipID { get; set; }
        public ulong Power { get; set; }
        public ulong Speed { get; set; }
        public ulong Score { get; set; }
        public DateTime ToiletWasBorn { get; set; }
        //public string OwnedByPlayerProfile { get; set; } //is string but holds guid

        //db only
        public DateTime OwnershipCreatedAt { get; set; }
        public DateTime OwnershipUpdatedAt { get; set; }
    }
}
