using Burk.BL.Interface;
using Burk.DAL.Context;
using Burk.DAL.Entity;
using Burk.DAL.Repository.Interface;
using Burk.DTO;
using Burk.DAL.ResponseModel;

using Microsoft.EntityFrameworkCore;

namespace Burk.BL.Imp;

public class QuestionService: IQuestionService
{
	private readonly IAsyncRepository<Question> _questionRepository;

	public QuestionService(IAsyncRepository<Question> questionRepository)
	{
		_questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
	}



	public async Task<Response<List<Question>>> GetAllQuestions()
	{
		Response<List<Question>> res = new Response<List<Question>>(new List<Question>());
		try { 
		
		res.Data= await _questionRepository.ListAllAsync(false);

		return res;
		}
		catch (Exception ex)
		{

			res.Errors.Add(new Error { Message = ex.Message, ErrorCode = "500" });
			return res;

		}
	}
	public async Task<Response<Question>> AddQuestion(AddQuestionDTO questionDTO)
	{
		Response<Question> res = new Response<Question>(new Question());
		int questionnumber = await _questionRepository.CountAllAsync()+1;
		
		try { 
		if (questionDTO != null && !(await _questionRepository.AnyAsync(q=>q.Id == questionnumber)))
		{
			Question question = new Question()
			{
				QuestionNumber= questionnumber,
				data = questionDTO.data,
				type = questionDTO.type

			};
			res.Data = await _questionRepository.AddAsync(question);

			return res;
		}
		res.Errors.Add(new Error { Message="an erorr accured please contact",ErrorCode="500"});
		return res;
		}
		catch (Exception ex) {

			res.Errors.Add(new Error { Message = ex.Message, ErrorCode = "500" });
			return res;

		}

	}
	public async Task<Response<Question>> EditQuestion(int questionumber,EditQuestionDTO questionDTO)
	{
		Response<Question> res = new Response<Question>(new Question());
		try { 
		var question = await _questionRepository.FirstOrDefaultAsync(x => x.QuestionNumber == questionumber, false);
		if(!(question == null && questionDTO==null))
			{ 

			question.data= questionDTO.data;
			res.Data= await _questionRepository.UpdateAsync(question);

			return res;


			}
		res.Errors.Add(new Error { Message = "not exist question", ErrorCode = "400" });
			return res; }
		catch (Exception ex)
		{

			res.Errors.Add(new Error { Message = ex.Message, ErrorCode = "500" });
			return res;

		}
	}
	public async Task<Response<bool>> DeleteQuestion(int id)
	{
		Response<bool> res = new Response<bool>(default(bool));
		try
		{ 
		// there is more implemention we will disccuss about deleting reviews related to this question
		var question = await _questionRepository.FirstOrDefaultAsync(q => q.Id == id, false);
		if (question != null)
		{
			await _questionRepository.DeleteAsync(question);
				res.Data = true;
				return res;
		}
			res.Data = false;
			return  res ;
		}
		catch (Exception ex)
		{

			res.Errors.Add(new Error { Message = ex.Message, ErrorCode = "500" });
			return res;

		}

	}
	



}
