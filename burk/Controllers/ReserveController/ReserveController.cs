using Burk.BL.Imp;
using Burk.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Burk.Controllers.ReserveController;
[Route("api/[controller]")]
[ApiController]
public class ReserveController : ControllerBase
{
    private readonly ReserveService _reserveService;

    public ReserveController(ReserveService reserveService)
	{
        _reserveService = reserveService;
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
    public async Task<IActionResult> RemoveAccepted(int id)
    {
         await _reserveService.RemoveAccepted(id);
        
            return Ok("Removed");
       

    }

}
