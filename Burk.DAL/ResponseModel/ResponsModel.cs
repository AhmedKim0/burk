using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Burk.DAL.ResponseModel;
public class Response<T>
{
	public Response(T _data)
	{
		Data = _data;
		Errors = new List<Error>();
	}

	public bool IsSuccess
	{
		get
		{
			return Errors?.Count <= 0;
		}
	}
	public T? Data { get; set; }
	public List<Error>? Errors { get; set; }
}
