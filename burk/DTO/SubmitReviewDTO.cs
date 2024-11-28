using Burk.DAL.Entity;

using System.ComponentModel.DataAnnotations;

namespace Burk.DTO;

public class SubmitReviewDTO
{

	[Required]
	public string CheckNo { get; set; }
	[Required]
	public string ClientName { get; set; }
	[Required]
	public string PhoneNumber { get; set; }
	public string? Email { get; set; }
	public List<AnswerDTO>? Answers { get; set; }




}


