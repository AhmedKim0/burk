using Burk.DAL.Entity;
using Burk.DTO;

namespace Burk.BL.Interface;

public interface IQuestionService
{
	Task<Question> AddQuestion(AddQuestionDTO questionDTO);
	Task<List<Question>> GetAllQuestions();
	Task<Question> EditQuestion(int id, EditQuestionDTO questionDTO);
	Task<bool> DeleteQuestion(int id);

}
