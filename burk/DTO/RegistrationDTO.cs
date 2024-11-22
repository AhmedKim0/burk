using System.ComponentModel.DataAnnotations;

namespace Burk.DTO
{
    public class RegistrationDTO
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }
		[Required]
		public int role {  get; set; } // { 1"Admin", 2"Reserver",3 "Waiter" };

	}
}
