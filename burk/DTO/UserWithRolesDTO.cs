namespace Burk.DTO;

public class UserWithRolesDTO
{
	public string UserId { get; set; }
	public string UserName { get; set; }
	public List<string> Roles { get; set; }
}
