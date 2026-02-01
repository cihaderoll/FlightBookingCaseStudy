using AutoMapper;
using FlightBookingCaseStudy.Application.Interfaces;
using MediatR;

namespace FlightBookingCaseStudy.Application.Use_Cases.Commands.Search
{
    public class GetFlightsCommandHandler : IRequestHandler<GetFlightsCommand, List<FlightDto>>
    {
        private readonly IFlightProviderClient _flightProviderClient;
        private readonly IMapper _mapper;

        public GetFlightsCommandHandler(
            IFlightProviderClient flightProviderClient, 
            IMapper mapper)
        {
            _flightProviderClient = flightProviderClient;
            _mapper = mapper;
        }

        public async Task<List<FlightDto>> Handle(GetFlightsCommand request, CancellationToken cancellationToken)
        {
            return await _flightProviderClient
                .SearchFlight(request.Origin, request.Destination, request.DepartDate, request.ReturnDate);
        }
    }
}