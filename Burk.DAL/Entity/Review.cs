using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burk.DAL.Interfaces;

namespace Burk.DAL.Entity
{
    public class Review 
    {
        [Required]
        [Key]
        public string CheckNo { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
       
        public int Question1 { get; set; }//Rate1
        public string? Comment1 { get; set; }//coment 1
        public int Question2 { get; set; }//Rate2
        public string? Comment2 { get; set; }//coment 2
        public int Question3 { get; set; }//Rate3
        public string? Comment3 { get; set; }//coment 3
        public int Question4 { get; set; }//Rate4
        public string? Comment4 { get; set; }//coment 4
        public int Question5 { get; set; }//Rate5
        public string? Comment5 { get; set; }//coment 5
        public Client ClientId { get; set; } //Nav property one to many



    }
}
