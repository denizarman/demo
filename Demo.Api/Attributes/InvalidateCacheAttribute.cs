using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class InvalidateCacheAttribute : ActionFilterAttribute
    {
        private string _key;

        public InvalidateCacheAttribute(string key)
        {
            _key = key;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var redisClientManager = context.HttpContext.RequestServices.GetRequiredService<IRedisClientsManager>();

            using (var client = redisClientManager.GetClient())
            {
                client.Remove("departments");
            }
            base.OnActionExecuted(context);
        }
    }
}
