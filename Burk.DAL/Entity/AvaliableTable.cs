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
        
        public int TableId { get; set; }
        [Required]
        public string ClientId { get; set; }
        bool IsAvaliable { get; set; }
      
    }
}
