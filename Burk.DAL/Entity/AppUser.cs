using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Burk.DAL.Entity
{
    public class AppUser:IdentityUser
    {
		public bool IsDeleted { get; set; }
	}
}
