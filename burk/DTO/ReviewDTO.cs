using Burk.DAL.Entity;

using System.ComponentModel.DataAnnotations;

namespace Burk.DTO;

public class ReviewDTO
{
	[Required]
	public string ClientName {  get; set; }
	
	public int ClientId { get; set; }
	//[Required]
	//public string CheckNo { get; set; }
	public int QuestionNumber { get; set; }
	public int AnswerType { get; set; }
	public int? rate { get; set; }
	public string? comment { get; set; }

	public bool? yesOrNO { get; set; }
}
