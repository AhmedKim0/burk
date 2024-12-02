using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Enum
{
    public enum AppResponseCode
    {

        Success = 200,
        Failed = 201,
        BadRequest = 400,
        InternalServerError=500
    }
}
