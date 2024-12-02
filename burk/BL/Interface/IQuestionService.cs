using Burk.DAL.Entity;
using Burk.DTO;
using Burk.DAL.ResponseModel;

namespace Burk.BL.Interface;

public interface IQuestionService
{
	Task<Response<Question>> AddQuestion(AddQuestionDTO questionDTO);
	Task<Response<List<Question>>> GetAllQuestions();
	Task<Response<Question>> EditQuestion(int id, EditQuestionDTO questionDTO);
	Task<Response<bool>> DeleteQuestion(int id);

}
