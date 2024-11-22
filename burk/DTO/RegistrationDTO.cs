using System.ComponentModel.DataAnnotations;

namespace burk.DTO
{
    public class RegistrationDTO
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }

    }
}
