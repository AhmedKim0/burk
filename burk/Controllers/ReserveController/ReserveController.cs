﻿using Burk.BL.Imp;
using Burk.BL.Interface;
using Burk.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Burk.Controllers.ReserveController;
[Route("api/[controller]")]
[ApiController]
public class ReserveController : ControllerBase
{
    private readonly IReserveService _reserveService;

    public ReserveController(IReserveService reserveService)
    {
        _reserveService = reserveService;
    }
	[HttpGet("GetAllWaitingList")]
	public async Task<IActionResult> GetAllWaitingList()
    {   var list =await _reserveService.GetWaitingListAsync();
        return Ok(list);

    }
	[HttpGet("GetAllAcceptedList")]
	public async Task<IActionResult> GetAllAcceptedList()
	{
		var list = await _reserveService.GetAcceptedUserAsync();
		return Ok(list);

	}





	[HttpPost("AcceptUser")]
    public async Task<IActionResult> AcceptUser(int id, int tablenumber)
    {

       string state= await _reserveService.AcceptUser(id, tablenumber);

        if (state == "done")
         return Ok("Accepted");
        return BadRequest(state);
        
    }
    [HttpPost("UnAcceptUser")]
    public async Task<IActionResult> UnAcceptUser(int id)
    {
        string state = await _reserveService.UnAcceptUser(id);
        if (state == "done")
            return Ok("Accepted");
        return BadRequest(state);
    }
    [HttpPut("EditUser")]
    public async Task<IActionResult> EditAcceptedUser(int id, EditUserDTO userDTO)
    {
        string state = await _reserveService.EditAccepted(id,userDTO);
        if (state == "done")
            return Ok("Accepted");
        return BadRequest(state);
    }
    [HttpDelete("RemoveUser")]
    public async Task<IActionResult> RemoveUserWaiting(int id,bool IsLeaving)
        // true if the customer came and false if the use was fake or didnot come
    {
        if (ModelState.IsValid) { 
         await _reserveService.RemoveUserWaiting(id, IsLeaving);
        
            return Ok("Removed");
        }
        return BadRequest();

    }

}
