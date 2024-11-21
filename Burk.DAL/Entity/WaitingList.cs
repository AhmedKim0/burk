using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burk.DAL.Interfaces;

namespace Burk.DAL.Entity
{
    public class WaitingList:BaseAuditableEntity,IAuditable
    {
        public string ClientName { get; set; }
        
        public int Visitors { get; set; }

    }
}
