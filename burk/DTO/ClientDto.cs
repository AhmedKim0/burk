using System.ComponentModel.DataAnnotations;

namespace burk.DTO
{
    public class ClientDto
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Email { get; set; }
    }
}
