using System.ComponentModel.DataAnnotations;

namespace Burk.DTO;

public class LoginDTO
{
	[Required(ErrorMessage = "userName is required.")]
	public string userName { get; set; }
	[Required(ErrorMessage = "Password is required.")] 
	public string password { get; set; }
}
