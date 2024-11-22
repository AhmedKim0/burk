using System.ComponentModel.DataAnnotations;

namespace burk.DTO
{
    public class LoginDto
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
