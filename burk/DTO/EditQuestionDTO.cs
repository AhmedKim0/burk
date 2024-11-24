using System.ComponentModel.DataAnnotations;

namespace Burk.DTO;

public class EditQuestionDTO
{
	[Required]

	public string data { get; set; }
}
