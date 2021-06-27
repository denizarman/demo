using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Dtos.Base
{
    public class ExceptionDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ExceptionDto(Exception ex, int statusCode)
        {
            Message = ex.Message;
            StatusCode = statusCode;
        }
    }
}
