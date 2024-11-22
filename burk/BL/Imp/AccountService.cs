using AutoMapper;
using burk.BL.interfaces;
using burk.DTO;
using Burk.DAL.Entity;
using Burk.DAL.Repositories.imp;
using Burk.DAL.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;

using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;




namespace burk.BL.Imp
{

    public class AccountService:IAccountService
    {
        private readonly IClientRepo _repo;
        private readonly IMapper _mapper;
        public AccountService(IClientRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _mapper = mapper;
        }
        public bool IsClientAvaliable(string name , string phone)
        {
            var isAValiablename = IsNameAvaliable(name);
            var isAValiablephone = IsNameAvaliable(phone);
            if (isAValiablename||isAValiablephone)
                return true;
            return false;
        }
        private async Task< bool> IsPhoneavaliable(string phone)
        {
            var client =await _repo.GetClientByPhone(phone);
            if (client == null) 
                return false;
            return true;
        }
        public bool IsNameAvaliable(string name)
        {
            var client=_repo.GetClientByName(name);
            if (client==null)
                return false;
            return true;
        }
        public void SaveClient(ClientDto user)
        {
            var client = _mapper.Map<Client>(user);
            _repo.AddClient(client);
        }
        ///        private readonly UserManager<AppUser> _userManager;
        ///        private readonly IConfiguration configuration;
        ///        public AccountService(UserManager<AppUser> userManager, IConfiguration configuration)
        ///        {
        ///            _userManager = userManager;
        ///            this.configuration = configuration;
        ///        }
        ///        public async Task<IActionResult> RegisterNewUser(Client user)
        ///        {
        ///            if (ModelState.IsValid)
        ///            {
        ///                AppUser appUser = new()
        ///                {
        ///                    UserName = user.Name,
        ///                    Email = user.Email,
        ///                };
        ///                IdentityResult result = await _userManager.CreateAsync(appUser, user.PhoneNumber);
        ///                if (result.Succeeded)
        ///                {
        ///                    return Ok("Success");
        ///                }
        ///                else
        ///                {
        ///                    foreach (var item in result.Errors)
        ///                    {
        ///                        ModelState.AddModelError("", item.Description);
        ///                    }
        ///                }
        ///            }
        ///            return BadRequest(ModelState);
        ///        }
        ///        public async Task<IActionResult> LogIn(dtoLogin login)
        ///        {
        ///            if (ModelState.IsValid)
        ///            {
        ///                AppUser? user = await _userManager.FindByNameAsync(login.userName);
        ///                if (user != null)
        ///                {
        ///                    if (await _userManager.CheckPasswordAsync(user, login.password))
        ///                    {
        ///                        var claims = new List<Claim>();
        ///                        //claims.Add(new Claim("name", "value"));
        ///                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
        ///                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
        ///                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        ///                        var roles = await _userManager.GetRolesAsync(user);
        ///                        foreach (var role in roles)
        ///                        {
        ///                            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
        ///                        }
        ///                        //signingCredentials
        ///                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
        ///                        var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        ///                        var token = new JwtSecurityToken(
        ///                            claims: claims,
        ///                            issuer: configuration["JWT:Issuer"],
        ///                            audience: configuration["JWT:Audience"],
        ///                            expires: DateTime.Now.AddHours(1),
        ///                            signingCredentials: sc
        ///                            );
        ///                        var _token = new
        ///                        {
        ///                            token = new JwtSecurityTokenHandler().WriteToken(token),
        ///                            expiration = token.ValidTo,
        ///                        };
        ///                        return Ok(_token);
        ///                    }
        ///                    else
        ///                    {
        ///                        return Unauthorized();
        ///                    }
        ///                }
        ///                else
        ///                {
        ///                    ModelState.AddModelError("", "User Name is invalid");
        ///                }
        ///            }
        ///            return BadRequest(ModelState);
        ///        }
    }


}
