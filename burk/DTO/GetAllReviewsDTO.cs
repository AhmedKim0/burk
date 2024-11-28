using Burk.DAL.Entity.Enums;

namespace Burk.DTO;

public class GetAllReviewsDTO
{
	public string ClientName { get; set; }
	public string PhoneNumber { get; set; }
	public string? Email { get; set; }
	public List<CheckDTo>? checkDTos { get; set; }
	

}


public class CheckDTo
{
	public string CheckNo { get; set; }
	public DateTime CreatedAt { get; set; }
	public List<Answer>? Answers {  get; set; }

}

public class Answer
{
	public int QuestionNumber { get; set; }
	public QuestionType AnswerType { get; set; }
	public int? rate { get; set; }
	public string? comment { get; set; }


	public bool? yesOrNO { get; set; }
}
