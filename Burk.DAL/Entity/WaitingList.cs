using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burk.DAL.Entity.Common;
using Burk.DAL.Entity.Common.Interfaces;

namespace Burk.DAL.Entity
{
    public class WaitingList : BaseAuditableEntity, IAuditable, IHardDeletable
	{

		public string ClientName { get; set; }
        public string PhoneNumber { get; set; }

        public string? Email { get; set; }

        public int? Visitors { get; set; }
        public int? TableNumber { get; set; }

        public DateTime ReservationTime { get; set; } = DateTime.Now;
        public DateTime AttendanceTime { get; set; }
        public bool IsAccepted { get; set; }
        public int IsConfirmed {  get; set; } //0 for notconfirmed 1 confirmed 3canceled

        public int? area { get; set; }
        public bool? Smoking { get; set; }
		public int ClientId { get; set; }

		public Client  client { get; set; }









	}
}
