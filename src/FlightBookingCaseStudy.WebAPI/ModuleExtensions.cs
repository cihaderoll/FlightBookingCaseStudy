using FlightBookingCaseStudy.Application.Interfaces;
using FlightBookingCaseStudy.Infrastructure.Client;

namespace FlightBookingCaseStudy.WebAPI
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddClients(this IServiceCollection services)
        {
            services.AddHttpClient<IFlightProviderClient, FlightProviderClient>();

            return services;
        }
    }
}
