using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burk.DAL.Entity;
public class ApplicationUserRole : IdentityUserRole<string>
{
	public bool IsDeleted { get; set; }
}