using Burk.BL.Interface;
using Burk.DAL.Entity;
using Burk.DTO;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Burk.Controllers.QuestionController;
[Route("api/[controller]")]
[ApiController]
public class QuestionController : ControllerBase
{
	private readonly IQuestionService questionService;

	public QuestionController(IQuestionService questionService)
	{
		this.questionService = questionService ?? throw new ArgumentNullException(nameof(questionService));
	}
	[Authorize(Roles = "Admin")]
	[HttpGet("GetAllQuestion")]
	public async Task<IActionResult> GetAllQuestion()
	{
		var questions = await questionService.GetAllQuestions();
		return Ok(questions);

	}
	[Authorize(Roles = "Admin")]
	[HttpPost("AddQuestion")]
	public async Task<IActionResult> AddQuestion(AddQuestionDTO addQuestionDTO)
	{
		if(ModelState.IsValid) {
		var question = await questionService.AddQuestion(addQuestionDTO);
			return Ok(question);
		}
		return BadRequest();
	}
	[Authorize(Roles = "Admin")]
	[HttpPut("EditQuestion")]
	public async Task<IActionResult> EditQuestion(int id,EditQuestionDTO editQuestionDTO)
	{
		if(ModelState.IsValid)
		{
			var question = await questionService.EditQuestion(id ,editQuestionDTO);
			return Ok(question);

		}
		return BadRequest();
	}
	[Authorize(Roles = "Admin")]
	[HttpDelete("RemoveQuestion")]
	public async Task<IActionResult> RemoveQuestion(int id)
	{
		if (ModelState.IsValid)
		{
			var question = await questionService.DeleteQuestion(id);
			return Ok(question);

		}
		return BadRequest();
	}






}
