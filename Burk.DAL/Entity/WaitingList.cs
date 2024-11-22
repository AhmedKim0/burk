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
        public string ClientName { get; set; }

        public int Visitors { get; set; }

        public DateTime ReservationTime { get; set; }= DateTime.Now;
        public DateTime AttendanceTime { get; set; }

        public int area {  get; set; }
        public bool Smoking { get;  set; }





    }
}
