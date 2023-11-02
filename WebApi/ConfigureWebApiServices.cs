using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebApi.Errors;
using WebApi.Extensions.Services;
using WebApi.Middlewares;

namespace WebApi
{
    public static class ConfigureWebApiServices
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerService();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddHttpContextAccessor();
            services.AddCorsService(configuration);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                    .Where(s => s.Value!.Errors.Count() > 0)
                    .SelectMany(s => s.Value!.Errors)
                    .Select(e => e.ErrorMessage);

                    return new BadRequestObjectResult(new ValidationResponse(errors));
                };
            });

            services.AddScoped<ExceptionHandlerMiddleware>();

            return services;
        }
    }
}
