using FlightBookingCaseStudy.Application.Common.Behaviors;
using FlightBookingCaseStudy.Application.Use_Cases.Commands.Search;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FlightBookingCaseStudy.Application
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(GetFlightsCommandValidator).Assembly);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetFlightsCommand).Assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(CachingBehavior<,>));
            });

            return services;
        }
    }
}
