using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace API.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;
        public CachedAttribute(int timeToLiveSeconds)
        {
            _timeToLiveSeconds = timeToLiveSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cachService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            var cachekey = GenerateCachKeyfromRequest(context.HttpContext.Request);
            var cacheResponse = await cachService.GetCachedResponseAsync(cachekey);

            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;

                return;

            }

            var executedContext = await next(); // move to controller

            if (executedContext.Result is OkObjectResult okObjectResult)
            {
                await cachService.CacheResponseAsync(cachekey,
                 okObjectResult.Value,
                  TimeSpan.FromSeconds(_timeToLiveSeconds));
            }
        }

        private string GenerateCachKeyfromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append($"{request.Path}");

            foreach (var (key, Value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{Value}");
            }

            return keyBuilder.ToString();
        }
    }
}