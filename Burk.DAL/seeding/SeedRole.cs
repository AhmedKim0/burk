
using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class SeedRole
{
	public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
	{
		string[] roles = { "Admin", "Reserver", "Waiter" };

		foreach (var role in roles)
		{
			if (!await roleManager.RoleExistsAsync(role))
			{
				await roleManager.CreateAsync(new IdentityRole(role));
			}
		}
	}
}
