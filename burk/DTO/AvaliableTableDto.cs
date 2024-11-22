using System.ComponentModel.DataAnnotations;

namespace burk.DTO
{
    public class AvaliableTableDto
    {
        [Required]
        public int TableId { get; set; }
        [Required]
        public int ClientId { get; set; }
        public bool IsAvaliable { get; set; }
    }
}
