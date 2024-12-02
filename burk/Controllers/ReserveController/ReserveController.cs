﻿using Burk.BL.Imp;
using Burk.BL.Interface;
using Burk.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Burk.DAL.ResponseModel;

namespace Burk.Controllers.ReserveController { 
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
    {
        var res = await _reserveService.GetWaitingListAsync();
        return Ok(res);

    }
    //[HttpGet("GetAllAcceptedList")]
    //public async Task<IActionResult> GetAllAcceptedList()
    //{
    //    var list = await _reserveService.GetAcceptedUserAsync();
    //    return Ok(list);

    //}





    //[HttpPost("AcceptUser")]
    //   public async Task<IActionResult> AcceptUser(int id, int tablenumber)
    //   {

    //      string state= await _reserveService.AcceptUser(id, tablenumber);
    //	if (!ModelState.IsValid)
    //		return BadRequest(state);
    //	if (state == "done")
    //        return Ok("Accepted");
    //       return BadRequest(state);

    //   }
    //   [HttpPost("UnAcceptUser")]
    //   public async Task<IActionResult> UnAcceptUser(int id)
    //   {
    //       string state = await _reserveService.UnAcceptUser(id);
    //       if (state == "done")
    //           return Ok("Accepted");
    //       return BadRequest(state);
    //   }
    [HttpPost("ConfirmUser")]
    public async Task<IActionResult> ConfirmUser(int id, int tablenumber)
    {


        if (!ModelState.IsValid)
            { 
            var eres = new Response<bool>(default(bool));
            eres.Errors.Add(new Error { ErrorCode = "400", Message = "Invalid data" });
            return BadRequest(eres); 
            }
		var res = await _reserveService.ConfirmUser(id, tablenumber);
		return Ok(res);


    }
    [HttpPost("UnConfirmUser")]
    public async Task<IActionResult> UnConfirmUser(int id)
    {
		if (!ModelState.IsValid)
		{
			var eres = new Response<bool>(default(bool));
			eres.Errors.Add(new Error { ErrorCode = "400", Message = "Invalid data" });
			return BadRequest(eres);
		}

		var res = await _reserveService.UnConfirmUser(id);
        if(res.IsSuccess)
        return Ok(res);
        return BadRequest(res);


    }
    [HttpPut("EditUser")]
    public async Task<IActionResult> EditAcceptedUser(int id, EditUserDTO userDTO)
    {
		if (!ModelState.IsValid)
		{
			var eres = new Response<bool>(default(bool));
			eres.Errors.Add(new Error { ErrorCode = "400", Message = "Invalid data" });
			return BadRequest(eres);
		}
		var res = await _reserveService.EditAccepted(id, userDTO);
        if (res.IsSuccess)
        return Ok(res);
        return BadRequest(res);

    }
    [HttpDelete("CancelReservation")]
    public async Task<IActionResult> CancelReservation(int id, bool IsLeaving)
	// true if the customer came and false if the use was fake or didnot come
	{
		if (!ModelState.IsValid)
		{
			var eres = new Response<bool>(default(bool));
			eres.Errors.Add(new Error { ErrorCode = "400", Message = "Invalid data" });
			return BadRequest(eres);
		}
		var res = await _reserveService.CancelUserWaiting(id,IsLeaving);
		if (res.IsSuccess)
			return Ok(res);
        return BadRequest(res);



	}

}
}