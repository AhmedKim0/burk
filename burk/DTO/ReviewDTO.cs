using Burk.DAL.Entity.Enums;

using System.ComponentModel.DataAnnotations;

namespace Burk.DTO;

public class AnswerDTO
{
	[Required]
	public int QuestionNumber { get; set; }
	[Required]
	public QuestionType AnswerType { get; set; }
	public int? rate { get; set; }
	public string? comment { get; set; }

	public bool? yesOrNO { get; set; }

}