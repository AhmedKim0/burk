﻿using Burk.BL.Interface;
using Burk.DAL.Context;
using Burk.DAL.Entity;
using Burk.DAL.Repository.Interface;
using Burk.DTO;

using Microsoft.EntityFrameworkCore;

namespace Burk.BL.Imp;

public class QuestionService: IQuestionService
{
	private readonly IAsyncRepository<Question> _questionRepository;

	public QuestionService(IAsyncRepository<Question> questionRepository)
	{
		_questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
	}

	public async Task<List<Question>> GetAllQuestions()
	{
		return await _questionRepository.ListAllAsync();
	}
	public async Task<Question> AddQuestion(AddQuestionDTO questionDTO)
	{
		
		if (questionDTO != null && !(await _questionRepository.AnyAsync(q=>q.Id == questionDTO.QuestionNumber)))
		{
			Question question = new Question()
			{
				Id = questionDTO.Id,
				data = questionDTO.data,
				type = questionDTO.type

			};
			var entry =await _questionRepository.AddAsync(question);

			return question;
		}
		return null;

	}
	public async Task<Question> EditQuestion(int id,EditQuestionDTO questionDTO)
	{
		var question = await _questionRepository.FirstOrDefaultAsync(x => x.Id == id);
		if(!(question == null && questionDTO==null))
			{ 

			question.data= questionDTO.data;
			await _questionRepository.UpdateAsync(question);



			return question;


			}
		return null;
	}
	public async Task<bool> DeleteQuestion(int id)
	{

		// there is more implemention we will disccuss about deleting reviews related to this question
		var question = await _questionRepository.FirstOrDefaultAsync(q => q.Id == id);
		if (question != null)
		{
			await _questionRepository.DeleteAsync(question);

			return true;
		}
		return false;

	}
	



}
