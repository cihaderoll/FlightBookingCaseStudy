using FlightBookingCaseStudy.Application.Interfaces;
using MediatR;

namespace FlightBookingCaseStudy.Application.Use_Cases.Commands.Search
{
    public class GetFlightsCommand : IRequest<List<FlightDto>>, ICacheable
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public DateOnly DepartDate { get; set; }
        public DateOnly? ReturnDate { get; set; }

        string ICacheable.CacheKey => "Flights:"; //Explicit Interface Implementation

        TimeSpan? ICacheable.Expiration => TimeSpan.FromMinutes(5); //Explicit Interface Implementation
    }
}
