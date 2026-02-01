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
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(Mapping.MappingProfile).Assembly);

            return services;
        }
    }
}
