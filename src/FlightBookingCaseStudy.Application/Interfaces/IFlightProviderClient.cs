using FlightBookingCaseStudy.Application.Use_Cases.Commands.Search;

namespace FlightBookingCaseStudy.Application.Interfaces
{
    public interface IFlightProviderClient
    {
        public Task<List<FlightDto>> SearchFlight(string origin, string destination, DateOnly departDate, DateOnly? returNDate);
    }
}
