using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burk.DAL.Entity.Common;
using Burk.DAL.Entity.Common.Interfaces;
using Burk.DAL.Entity.Enums;

namespace Burk.DAL.Entity
{
    public class Review :BaseAuditableEntity, IAuditable, IHardDeletable
	{
        [Required]
        public string CheckNo { get; set; }
        public int QuestionNumber { get; set; }
		public QuestionType AnswerType { get; set; }
		public int? rate {  get; set; }
        public string? comment {  get; set; }
  

        public bool? yesOrNO { get; set; }


        public int ClientId  { get; set; }

        public RecordedVisit? Recorded { get; set; } 

        public Client Client { get; set; }



    }
}
