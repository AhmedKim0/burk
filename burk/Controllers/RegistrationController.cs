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
        public async Task<IActionResult> RegisterNewUser(ClientDto user )
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
        
    }
}
