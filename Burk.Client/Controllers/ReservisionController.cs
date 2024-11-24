using Burk.Client.BL.Interfaces;
using Burk.Client.DTO;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Burk.Client.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReservisionController : ControllerBase
{
    private readonly IUserReserveService _reserveService ;
	public ReservisionController(IUserReserveService reserveService)
    {
		_reserveService=reserveService;


	}
	[HttpPost("ReserveATable")]
	public async Task<IActionResult> Reserve(WatinigListDto client)
	{
		if(ModelState.IsValid) {
			await _reserveService.AddReservaiton(client);
			return Ok ("done");
			}
		else
		{ return BadRequest(); }
	}

}


