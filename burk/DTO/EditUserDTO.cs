namespace Burk.DTO;

public class EditUserDTO
{

	public int TableNumber { get; set; }
	public int? Visitors { get; set; }

	public DateTime AttendanceTime { get; set; }

	public int area { get; set; }
	public bool Smoking { get; set; }
}
