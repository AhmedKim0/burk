using Burk.DAL.Entity.Common;
using Burk.DAL.Entity.Common.Interfaces;
using Burk.DAL.Entity.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burk.DAL.Entity;
public class Question : BaseAuditableEntity, IAuditable, IHardDeletable
{

	public int QuestionNumber {  get; set; }
	public string data { get; set; }

	public  QuestionType type{ get; set; }// 1 rate 2/comment 3/yesorno
	public List<Review> reviews { get; set; }


}
