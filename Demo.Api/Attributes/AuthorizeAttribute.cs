using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        string _policy;

        public AuthorizeAttribute()
        {
        }

        public AuthorizeAttribute(string policy)
        {
            _policy = policy;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"];
            var roles = (List<string>)context.HttpContext.Items["Roles"];
            if (user == null)
            {
                context.Result = new JsonResult(new { message = "User Unauthorized !" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else if (!string.IsNullOrEmpty(_policy) && !roles.Contains(_policy))
            {
                context.Result = new JsonResult(new { message = "User Policy is not permitted for this operation !" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
