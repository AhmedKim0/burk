using Burk.BL.Interface;
using Burk.DTO;

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
	[HttpPost("AddReview")]
	public async Task<IActionResult> AddReview(SubmitReviewDTO dto)
	{
		if(ModelState.IsValid) {
			return Ok(await _reviewService.AddReview(dto)); }
		return BadRequest(ModelState);
	}
	[HttpGet("GetAllReview")]
	public async Task<IActionResult> GetAllReview()
	{
		var result =await _reviewService.GetAllReview();
			if (result != null) { 
		return Ok(result); }
			return BadRequest(ModelState);
	}
}
