using FlightBookingCaseStudy.Application.Common.Settings;
using FlightBookingCaseStudy.Application.Interfaces;
using FlightBookingCaseStudy.Infrastructure.Persistence;
using FlightBookingCaseStudy.Infrastructure.Services.Caching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlightBookingCaseStudy.Infrastructure.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddAutoMapper(cfg => { }, typeof(Mapping.MappingProfile).Assembly);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis") ?? "localhost:6379";
                options.InstanceName = "FlightBooking_";
            });

            services.AddSingleton<ICachingService, RedisCacheService>();

            services.Configure<CacheSettings>(configuration.GetSection("Caching"));

            return services;
        }
    }
}
