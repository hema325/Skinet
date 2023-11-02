namespace WebApi.Extensions.Services
{
    public static class CorsServiceExtension
    {
        private const string corsPolicy = "CorsPolicy";
        public static IServiceCollection AddCorsService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(corsPolicy, policy =>
                {
                    var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
                    policy.WithOrigins(allowedOrigins!);
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCorsService(this IApplicationBuilder app) {
            app.UseCors(corsPolicy);
            return app;
        }
    }
}
