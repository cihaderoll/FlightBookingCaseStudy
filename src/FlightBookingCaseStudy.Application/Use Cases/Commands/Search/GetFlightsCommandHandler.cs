using FlightBookingCaseStudy.Application.Interfaces;
using MediatR;

namespace FlightBookingCaseStudy.Application.Use_Cases.Commands.Search
{
    public class GetFlightsCommandHandler : IRequestHandler<GetFlightsCommand, List<FlightDto>>
    {
        private readonly IFlightProviderClient _flightProviderClient;

        public GetFlightsCommandHandler(IFlightProviderClient flightProviderClient)
        {
            _flightProviderClient = flightProviderClient;
        }

        public async Task<List<FlightDto>> Handle(GetFlightsCommand request, CancellationToken cancellationToken)
        {
            var flights = await _flightProviderClient.SearchFlight("", "", DateOnly.MinValue);

            return new List<FlightDto>();
        }
    }
}