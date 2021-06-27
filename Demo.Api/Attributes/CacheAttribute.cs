using Demo.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using ServiceStack;
using ServiceStack.Redis;
using ServiceStack.Text;
using System;
using System.Collections.Generic;

namespace Demo.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAttribute : ActionFilterAttribute
    {
        private string _key;
        private Type _type { get; set; }


        public CacheAttribute(string key, Type type)
        {
            _key = key;
            _type = type;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var redisClientManager = context.HttpContext.RequestServices.GetRequiredService<IRedisClientsManager>();

            using (var client = redisClientManager.GetClient())
            {
                Type genericListType = typeof(List<>).MakeGenericType(_type);
                var _cacheValues = client.Get<string>(_key);
                if (_cacheValues == null)
                {
                    base.OnActionExecuting(context);
                }
                else
                {
                    // TODO property isimlerini lowercase verdigi icin, listeyi olusturuyor, ama propertyler bos geliyor.
                    var result = JsonSerializer.DeserializeFromString(_cacheValues, genericListType);
                    context.Result = new OkObjectResult(result);
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var redisClientManager = context.HttpContext.RequestServices.GetRequiredService<IRedisClientsManager>();

            using (var client = redisClientManager.GetClient())
            {
                var result = context.Result as OkObjectResult;

                var x = result.Value;
                client.Set(_key, result.Value);
            }
            base.OnActionExecuted(context);
        }
    }
}
