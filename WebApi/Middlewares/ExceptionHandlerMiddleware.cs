using WebApi.Errors;

namespace WebApi.Middlewares
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        private readonly IHostEnvironment _environment;

        public ExceptionHandlerMiddleware(IHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                var details = ex.StackTrace?.ToString();
                var message = ex.InnerException?.Message ?? ex.Message;
                if (!_environment.IsDevelopment())
                {
                    details = null;
                    message = "Something went wron while processing your request";
                }

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(
                    new ExceptionResponse(
                        StatusCodes.Status500InternalServerError,
                        message,
                        details));
            }
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            return app;
        }
    }
}
