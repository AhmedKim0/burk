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
	//[HttpPost]
	//public async Task<IActionResult> AddReview(int id, ReviewDTO DTO)
	//{
	//	if(Mode)
	//}
}
