using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burk.DAL.Interfaces;

namespace Burk.DAL.Entity
{
	[Table("RecordedVisit")]
	public class RecordedVisit :  BaseAuditableEntity, IAuditable
    {
		


	   //public int WaitingListId { get; set; }

	   [Required]
        public int TableNumber {  get; set; }
        public int? Visitors { get; set; }
		public DateTime ReservationTime { get; set; } 
		public DateTime AttendanceTime { get; set; }

		public int? area { get; set; }
        public bool? Smoking { get; set; }
		public int ClientId { get; set; }

		public List<Review> Review { get; set; }
        //public WaitingList WaitingList { get; set; }


        public Client Client { get; set; }
      
    }
}
