using Burk.BL.Interface;
using Burk.DAL.ResponseModel;
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
		if (!ModelState.IsValid)
		{
			var eres = new Response<bool>(default(bool));
			eres.Errors.Add(new Error { ErrorCode = "400", Message = "Invalid data" });
			return BadRequest(eres);
		}
		var res = await _reviewService.GetClientByPhone(phoneNumber);
			if(res.IsSuccess)
			return Ok(res); 
			
			return Ok(res.Errors);


		

	}
	[HttpPost("AddReview")]
	public async Task<IActionResult> AddReview(SubmitReviewDTO dto)
	{
		if (!ModelState.IsValid)
		{
			var eres = new Response<bool>(default(bool));
			eres.Errors.Add(new Error { ErrorCode = "400", Message = "Invalid data" });
			return BadRequest(eres);
		}
		var res = await _reviewService.AddReview(dto);
		return Ok(res);
	}
	[HttpGet("GetAllReview")]
	public async Task<IActionResult> GetAllReview()
	{
		if (!ModelState.IsValid)
		{
			var eres = new Response<bool>(default(bool));
			eres.Errors.Add(new Error { ErrorCode = "400", Message = "Invalid data" });
			return BadRequest(eres);
		}
		var res =await _reviewService.GetAllReview();
		return Ok(res);

	}
}
