using System.ComponentModel.DataAnnotations;

namespace Burk.DTO;

public class AddQuestionDTO
{
	[Required]
	public int Id { get; set; }
	[Required] 
	public int QuestionNumber { get;set; }
	[Required]

	public string data { get; set; }
	[Required]

	public int type { get; set; }// 1 rate 2/comment 3/yesorno
}
