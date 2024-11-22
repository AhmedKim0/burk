using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using burk.BL.Imp;
using burk.BL.interfaces;
using burk.DTO;
using Burk.DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace burk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IAccountService _account;
        private readonly IMapper _mapper;

        public RegistrationController(UserManager<AppUser> userManager, IConfiguration configuration,
           IAccountService account,IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _account = account;
           

        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewUser(ClientDto user)
        {
            var IsAvaliableClient = _account.IsClientAvaliable(user.Name, user.PhoneNumber);
            if (IsAvaliableClient)
                return BadRequest("Client is Avaliable");
            if (ModelState.IsValid)
            {
                AppUser appUser = new()
                {
                    UserName = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,

                };
                IdentityResult result = await _userManager.CreateAsync(appUser,user.PhoneNumber);
                if (result.Succeeded)
                {
                   _account.SaveClient(user);

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
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LoginDto login)
        {
            if (ModelState.IsValid)
            {
                AppUser? user = await _userManager.FindByNameAsync(login.Name);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, login.PhoneNumber))
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
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                        var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            claims: claims,
                            issuer: _configuration["JWT:Issuer"],
                            audience: _configuration["JWT:Audience"],
                            expires: DateTime.Now.AddHours(1),
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
