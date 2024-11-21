using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burk.DAL.Interfaces;

namespace Burk.DAL.Entity
{
       public class Client:BaseAuditableEntity,IAuditable
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Email { get; set; }
      
        public ICollection<Review> Reviews { get; set; } //Nav property one to many
        public List<AvaliableTable> AvaliableTabel { get; set; }//Nav property one to many


    }
}
