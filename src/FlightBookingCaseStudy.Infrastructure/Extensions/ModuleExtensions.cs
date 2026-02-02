using AutoMapper;
using FlightBookingCaseStudy.Application.Interfaces;
using FlightBookingCaseStudy.Infrastructure.Services.Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingCaseStudy.Infrastructure.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(cfg => { }, typeof(Mapping.MappingProfile).Assembly);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis") ?? "localhost:6379";
                options.InstanceName = "FlightBooking_";
            });

            services.AddSingleton<ICachingService, RedisCacheService>();

            return services;
        }
    }
}
