using Burk.DTO;

using Burk.DAL.Entity;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Burk.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		public AccountController(UserManager<AppUser> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			this.configuration = configuration;
		}

		private readonly UserManager<AppUser> _userManager;
		private readonly IConfiguration configuration;
		//[Authorize]
		//[Authorize(Roles = "Admin")]
		[HttpPost("Register")]
		public async Task<IActionResult> RegisterNewUser(RegistrationDTO user)
		{
			if (ModelState.IsValid)
			{
				AppUser appUser = new()
				{
					UserName = user.userName

				};

				IdentityResult result = await _userManager.CreateAsync(appUser, user.password);

				if (result.Succeeded)
				{
					switch (user.role) 
						{ 
						case 1:
					await _userManager.AddToRoleAsync(appUser, "Admin");
							break;
						case 2:
							await _userManager.AddToRoleAsync(appUser, "Reserver");
							break;
						case 3:
							await _userManager.AddToRoleAsync(appUser, "Waiter");
							break;

					}


					return Ok("Success");
					
				}
				else
				{
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("", item.Description);
					}
				}
			}
			return BadRequest(ModelState);
		}


		[HttpPost("Login")]
		public async Task<IActionResult> LogIn(LoginDTO login)
		{
			if (ModelState.IsValid)
			{
				AppUser? user = await _userManager.FindByNameAsync(login.userName);
				if (user != null)
				{
					if (await _userManager.CheckPasswordAsync(user, login.password))
					{
						var claims = new List<Claim>();
						//claims.Add(new Claim("name", "value"));
						claims.Add(new Claim(ClaimTypes.Name, user.UserName));
						claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
						claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
						var roles = await _userManager.GetRolesAsync(user);
						foreach (var role in roles)
						{
							claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
						}
						//signingCredentials
						var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
						var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
						var token = new JwtSecurityToken(
							claims: claims,
							issuer: configuration["JWT:Issuer"],
							audience: configuration["JWT:Audience"],
							expires: DateTime.Now.AddHours(8),
							signingCredentials: sc
							);
						var _token = new
						{
							token = new JwtSecurityTokenHandler().WriteToken(token),
							expiration = token.ValidTo,
						};
						return Ok(_token);
					}
					else
					{
						return Unauthorized();
					}
				}
				else
				{
					ModelState.AddModelError("", "User Name is invalid");
				}
			}
			return BadRequest(ModelState);
		}
	}
}
