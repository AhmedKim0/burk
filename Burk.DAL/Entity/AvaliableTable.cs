using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burk.DAL.Interfaces;

namespace Burk.DAL.Entity
{
    public class AvaliableTable
    {
        [Required]
        [Key]
        public int TableId { get; set; }
        [Required]
        public int ClientId { get; set; }
        public bool IsAvaliable { get; set; }
        public Client Client { get; set; }
      
    }
}
