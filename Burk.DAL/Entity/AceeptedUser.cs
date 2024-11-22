using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burk.DAL.Interfaces;

namespace Burk.DAL.Entity
{
    public class AceeptedUser
    {
        
        [Key]
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }

        [Required]
        public string TableNumber {  get; set; }

        public Client Client { get; set; }
      
    }
}
