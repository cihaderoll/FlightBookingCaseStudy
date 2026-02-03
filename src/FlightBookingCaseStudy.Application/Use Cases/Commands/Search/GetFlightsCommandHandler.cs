using AutoMapper;
using FlightBookingCaseStudy.Application.Common.Settings;
using FlightBookingCaseStudy.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;

namespace FlightBookingCaseStudy.Application.Use_Cases.Commands.Search
{
    public class GetFlightsCommandHandler : IRequestHandler<GetFlightsCommand, List<FlightDto>>
    {
        private readonly IFlightProviderClient _flightProviderClient;
        private readonly IMapper _mapper;
        private readonly CacheSettings _cacheSettings;

        public GetFlightsCommandHandler(
            IFlightProviderClient flightProviderClient,
            IMapper mapper,
            IOptions<CacheSettings> cacheSettings)
        {
            _flightProviderClient = flightProviderClient;
            _mapper = mapper;
            _cacheSettings = cacheSettings.Value;
        }

        public async Task<List<FlightDto>> Handle(GetFlightsCommand request, CancellationToken cancellationToken)
        {
            return await _flightProviderClient
                .SearchFlight(request.Origin, request.Destination, request.DepartDate, request.ReturnDate);
        }
    }
}