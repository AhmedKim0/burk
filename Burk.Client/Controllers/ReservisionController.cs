using Burk.Client.BL.Interfaces;
using Burk.Client.DTO;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Burk.Client.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReservisionController : ControllerBase
{
    private readonly IReserveService _reserveService ;
	public ReservisionController(IReserveService reserveService)
    {
		_reserveService=reserveService;


	}
	[HttpPost]
	public async Task<IActionResult> Reserve(WatinigListDto client)
	{
		if(ModelState.IsValid) {
			await _reserveService.AddReservaiton(client);
			return Ok ();
			}
		else
		{ return BadRequest(); }
	}

}


