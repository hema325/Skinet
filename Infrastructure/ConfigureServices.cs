using Core.Entities.Identity;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Identity;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructures(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>(o => o.UseSqlServer(configuration.GetConnectionString("Store")));
            services.AddDbContext<IdentityContext>(o => o.UseSqlServer(configuration.GetConnectionString("Identity")));

            services.AddIdentityCore<AppUser>() 
                .AddEntityFrameworkStores<IdentityContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var settings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidIssuer = settings!.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var options = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis")!);
                return ConnectionMultiplexer.Connect(options);
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<StoreContextInitialiser>();
            services.AddScoped<IdentityContextInitialiser>();
            services.AddScoped<ITokenService, JwtService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPaymentService, StripeService>();
            services.AddScoped<ICacheService, RedisService>();

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.Configure<StripeSettings>(configuration.GetSection(StripeSettings.SectionName));

            return services;
        }

        public static IServiceProvider InitialiseDB(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<StoreContextInitialiser>().Initialise();
            scope.ServiceProvider.GetRequiredService<IdentityContextInitialiser>().Initialise();

            return serviceProvider;
        }
    }
}
