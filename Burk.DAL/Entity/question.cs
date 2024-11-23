using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burk.DAL.Entity;
public class Question
{
	
	public int Id { get; set; }
	public string data { get; set; }
	public int type { get; set; }// 1 rate 2/comment 3/yesorno

}
