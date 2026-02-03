using AutoMapper;
using FlightBookingCaseStudy.Application.Common.Settings;
using FlightBookingCaseStudy.Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;

namespace FlightBookingCaseStudy.Application.Use_Cases.Commands.Search
{
    public class GetFlightsCommandHandler : IRequestHandler<GetFlightsCommand, List<FlightDto>>
    {
        private readonly IFlightProviderClient _flightProviderClient;
        private readonly IAirportService _airportService;

        public GetFlightsCommandHandler(
            IFlightProviderClient flightProviderClient,
            IAirportService airportService)
        {
            _flightProviderClient = flightProviderClient;
            _airportService = airportService;
        }

        public async Task<List<FlightDto>> Handle(GetFlightsCommand request, CancellationToken cancellationToken)
        {
            if(!await _airportService.ValidateAirport(request.Origin, request.Destination, cancellationToken))
                throw new ValidationException("Please search for valid airports!");

            return await _flightProviderClient
                .SearchFlight(request.Origin, request.Destination, request.DepartDate, request.ReturnDate);
        }
    }
}