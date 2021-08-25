using Demo.Api.Dtos.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                ExceptionDto exception;
                if (ex is FileNotFoundException ||
                    ex is KeyNotFoundException ||
                    ex is DirectoryNotFoundException)
                {
                    exception = new ExceptionDto(ex, StatusCodes.Status404NotFound);
                    string jsonString = JsonConvert.SerializeObject(exception);
                    await context.Response.WriteAsJsonAsync(jsonString);
                }
                throw ex;
            }
        }
    }
}
