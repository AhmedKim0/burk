using Burk.BL.Interface;
using Burk.DTO;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Burk.Controllers.ReviewController;
[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
	private readonly IReviewService _reviewService;

	public ReviewController(IReviewService reviewService)
	{
		_reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
	}




	[Authorize(Roles = "Admin,Waiter")]
	[HttpGet("GetClientByPhone")]
	public async Task<IActionResult> GetClientByPhone(string phoneNumber)
	{
		if(!phoneNumber.IsNullOrEmpty()) 
		{ var client = await _reviewService.GetClientByPhone(phoneNumber);
			if (client != null) { return Ok(client); }
			return NotFound();
		}
		return BadRequest(ModelState);
	}




	[Authorize(Roles = "Admin,Waiter")]
	[HttpPost("AddReview")]
	public async Task<IActionResult> AddReview(SubmitReviewDTO dto)
	{
		if(ModelState.IsValid) {
			return Ok(await _reviewService.AddReview(dto)); }
		return BadRequest(ModelState);
	}






	[Authorize(Roles = "Admin")]
	[HttpGet("GetAllReview")]
	public async Task<IActionResult> GetAllReview( )
	{
		return Ok(await _reviewService.GetAllReview());
	}
}
