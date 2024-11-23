using Burk.DAL.Context;
using Burk.DAL.Entity;
using Burk.DAL.Repository.Interface;
using Burk.DTO;

using Microsoft.EntityFrameworkCore;

namespace Burk.BL.Imp;

public class QuestionService
{
	private readonly BurkDbContext _db;

	public QuestionService(BurkDbContext db)
	{
		 _db = db ?? throw new ArgumentNullException(nameof(db));
	}
	public async Task<Question> AddQuestion(AddQuestionDTO questionDTO)
	{
		
		if (questionDTO == null && !(await _db.Questions.AnyAsync(q=>q.Id == questionDTO.Id)))
		{
			Question question = new Question()
			{
				Id = questionDTO.Id,
				data = questionDTO.data,
				type = questionDTO.type

			};
			var entry =await _db.Questions.AddAsync(question);
			await _db.SaveChangesAsync();
			return question;
		}
		return new Question();

	}
	public async Task<Question> EditQuestion(int id,EditQuestionDTO questionDTO)
	{
		var question = await _db.Questions.FirstOrDefaultAsync(x => x.Id == id);
		if(!(question == null && questionDTO==null))
			{ 

			question.data= questionDTO.data;
			_db.Questions.Update(question);
			await _db.SaveChangesAsync();


			return question;


			}
		return new Question(); 
	}
	public async Task<bool> DeleteQuestion(int id)
	{

		// there is more implemention we will disccuss about deleting reviews related to this question
		var question = await _db.Questions.FirstOrDefaultAsync(q=>q.Id==id);
		if (question != null)
		{
			 _db.Questions.Remove(question);
			await _db.SaveChangesAsync();
			return true;
		}
		return false;

	}



}
