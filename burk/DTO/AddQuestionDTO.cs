using Burk.DAL.Entity.Enums;

using System.ComponentModel.DataAnnotations;

namespace Burk.DTO;

public class AddQuestionDTO
{

	[Required] 
	public int QuestionNumber { get;set; }
	[Required]

	public string data { get; set; }
	[Required]

	public QuestionType type { get; set; }// 1 rate 2/comment 3/yesorno
}
