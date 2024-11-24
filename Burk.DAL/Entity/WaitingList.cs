using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burk.DAL.Interfaces;

namespace Burk.DAL.Entity
{
    public class WaitingList : BaseAuditableEntity, IAuditable
    {
        public int ClientId { get; set; }

		public string ClientName { get; set; }
        public string PhoneNumber { get; set; }

        public string? Email { get; set; }

        public int? Visitors { get; set; }
        public int? TableNumber { get; set; }

        public DateTime ReservationTime { get; set; } = DateTime.Now;
        public DateTime AttendanceTime { get; set; }
        public bool IsAccepted { get; set; }

        public int? area { get; set; }
        public bool? Smoking { get; set; }
        public Client  client { get; set; }









	}
}
