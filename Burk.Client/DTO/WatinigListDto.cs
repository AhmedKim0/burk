using System.ComponentModel.DataAnnotations;

namespace Burk.Client.DTO
{
    public class WatinigListDto
    {
        [Required]
        public string ClientName { get; set; }
		[Required]
		public string PhoneNumber { get; set; }

        public string? Email { get; set; }
        public int? Visitors { get; set; }
	
		public int? area { get; set; }

        public bool? Smoking { get; set; }
		[Required]
		public DateTime AttendanceTime { get; set; }
    }
}
