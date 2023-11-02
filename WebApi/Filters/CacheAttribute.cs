using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters
{
    public class CacheAttribute: ActionFilterAttribute
    {
        private TimeSpan _expiry = TimeSpan.FromMinutes(3);

        public CacheAttribute() { }
        public CacheAttribute(TimeSpan expiry)
        {
            _expiry = expiry;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context
                               .HttpContext
                               .RequestServices
                               .GetRequiredService<ICacheService>();

           var cachingKey = context.HttpContext.Request.Path +
                context.HttpContext.Request.QueryString.Value;

            var cachedResponse = await cacheService.GetAsync<object>(cachingKey);
            if(cachedResponse != null)
                context.Result = new OkObjectResult(cachedResponse);
            else
            {
                var response = await next();
                if(response.Result is OkObjectResult objectResult)
                    await cacheService.SetAsync(cachingKey, objectResult.Value, _expiry, true);
            }
        }
    }
}
