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
    public class Review 
    {
        [Required]
        [Key]
        public string CheckNo { get; set; }
        public int QuestionNumber { get; set; }
        public int? rate {  get; set; }
        public string? comment {  get; set; }
        public int  AnswerType { get; set; }    

        public bool? yesOrNO { get; set; }


        public int ClientId  { get; set; }

        public Client Client { get; set; }



    }
}
