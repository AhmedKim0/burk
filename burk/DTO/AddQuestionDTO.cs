namespace Burk.DTO;

public class AddQuestionDTO
{
	public int Id { get; set; }
	public string data { get; set; }
	public int type { get; set; }// 1 rate 2/comment 3/yesorno
}
